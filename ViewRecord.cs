using MySql.Data.MySqlClient;
using System;

namespace _Database
{
    class ViewRecords
    {
        public static void Execute(MySqlConnection conn)
        {
            string query = @"
            SELECT u.*, p.Phone, e.Email, l.Location 
            FROM Users u
            LEFT JOIN UserPhones up ON u.UserID = up.UserID
            LEFT JOIN Phones p ON up.PhoneID = p.PhoneID
            LEFT JOIN UserEmails ue ON u.UserID = ue.UserID
            LEFT JOIN Emails e ON ue.EmailID = e.EmailID
            LEFT JOIN Locations l ON u.UserID = l.UserID";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Console.WriteLine("User Details:");
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Console.WriteLine($"{rdr.GetName(i)}: {rdr[i]}");
                    }
                }
            }
        }
    }
}
