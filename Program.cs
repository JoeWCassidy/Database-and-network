using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Text.RegularExpressions;

namespace _Database
{
    class Whois
    {
        static async Task Main(string[] args)
        {
            string connStr = "server=localhost;user=root;database=userdata;port=3306;password=L3tM31n";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    await conn.OpenAsync();
                    Console.WriteLine("Connected to the MySQL database.");

                    if (args.Length > 0)
                    {
                        await ProcessCommandLineArguments(args, conn);
                    }
                    else
                    {
                        ShowMenu(conn);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                    Console.WriteLine("Disconnected from the database.");
                }
            }
        }

        static async Task ProcessCommandLineArguments(string[] args, MySqlConnection conn)
        {
            foreach (var arg in args)
            {
                if (arg.Contains("?"))
                {
                    var parts = arg.Split('?');
                    var loginID = parts[0];
                    var commandParts = parts[1].Split('=');
                    var field = commandParts[0];

                    if (commandParts.Length == 1)
                    {
                        await RetrieveField(loginID, field, conn);
                    }
                    else
                    {
                        var value = commandParts[1];
                        await UpdateField(loginID, field, value, conn);
                    }
                }
                else
                {
                    DisplayFullRecord(arg, conn);
                }
            }
        }

        static void ShowMenu(MySqlConnection conn)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add a record");
                Console.WriteLine("2. View records");
                Console.WriteLine("3. Update a record");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                string option = GetUserInputWithTimeout("", 60).Result;

                switch (option)
                {
                    case "1":
                        AddRecord.Execute(conn);
                        break;
                    case "2":
                        ViewRecords.Execute(conn);
                        break;
                    case "3":
                        UpdateRecord.Execute(conn);
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static async Task<string> GetUserInputWithTimeout(string prompt, int timeoutInSeconds)
        {
            Console.Write(prompt);

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutInSeconds)))
            {
                Task<string> userInputTask = Console.In.ReadLineAsync();
                if (await Task.WhenAny(userInputTask, Task.Delay(-1, cts.Token)) == userInputTask)
                {
                    return await userInputTask;
                }
                else
                {
                    Console.WriteLine("\nTimeout reached. Exiting.");
                    Environment.Exit(0);
                    return null;
                }
            }
        }

        static async Task RetrieveField(string loginID, string field, MySqlConnection conn)
        {
            string query = $"SELECT {field} FROM Users WHERE LoginID = '{loginID}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var result = await cmd.ExecuteScalarAsync();
            if (result != null)
            {
                Console.WriteLine($"{field} for {loginID} is: {result}");
            }
            else
            {
                Console.WriteLine($"No value found for {field} and LoginID: {loginID}");
            }
        }

        static async Task UpdateField(string loginID, string field, string value, MySqlConnection conn)
        {
            string query = $"UPDATE Users SET {field} = '{value}' WHERE LoginID = '{loginID}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            int count = await cmd.ExecuteNonQueryAsync();
            if (count > 0)
            {
                Console.WriteLine("Update successful.");
            }
            else
            {
                Console.WriteLine($"Failed to update {field} for LoginID: {loginID}");
            }
        }

        static void DisplayFullRecord(string loginID, MySqlConnection conn)
        {
            string query = $"SELECT * FROM Users WHERE LoginID = '{loginID}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    Console.WriteLine("User Details:");
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Console.WriteLine($"{rdr.GetName(i)}: {rdr[i]}");
                    }
                }
                else
                {
                    Console.WriteLine($"No records found for LoginID: {loginID}");
                }
            }
        }
    }
}
