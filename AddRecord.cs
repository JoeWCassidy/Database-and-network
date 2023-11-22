using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _Database
{
    class AddRecord
    {
        public static void Execute(MySqlConnection conn)
        {
            Console.WriteLine("Enter Surname:");
            string surname = Console.ReadLine();

            Console.WriteLine("Enter Forenames:");
            string forenames = Console.ReadLine();

            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();

            string phone = "";
            while (!IsValidPhoneNumber(phone))
            {
                Console.WriteLine("Enter Phone Number (format +123456789):");
                phone = Console.ReadLine();
            }

            string email = "";
            while (!IsValidEmail(email))
            {
                Console.WriteLine("Enter Email:");
                email = Console.ReadLine();
            }

            Console.WriteLine("Enter Location:");
            string location = Console.ReadLine();

            try
            {
                string userQuery = "INSERT INTO Users (Surname, Forenames, Title) VALUES (@Surname, @Forenames, @Title); SELECT LAST_INSERT_ID();";
                MySqlCommand userCmd = new MySqlCommand(userQuery, conn);
                userCmd.Parameters.AddWithValue("@Surname", surname);
                userCmd.Parameters.AddWithValue("@Forenames", forenames);
                userCmd.Parameters.AddWithValue("@Title", title);
                int userID = Convert.ToInt32(userCmd.ExecuteScalar());

                string phoneQuery = "INSERT INTO Phones (Phone) VALUES (@Phone); SELECT LAST_INSERT_ID();";
                MySqlCommand phoneCmd = new MySqlCommand(phoneQuery, conn);
                phoneCmd.Parameters.AddWithValue("@Phone", phone);
                int phoneID = Convert.ToInt32(phoneCmd.ExecuteScalar());

                string userPhoneQuery = "INSERT INTO UserPhones (UserID, PhoneID) VALUES (@UserID, @PhoneID);";
                MySqlCommand userPhoneCmd = new MySqlCommand(userPhoneQuery, conn);
                userPhoneCmd.Parameters.AddWithValue("@UserID", userID);
                userPhoneCmd.Parameters.AddWithValue("@PhoneID", phoneID);
                userPhoneCmd.ExecuteNonQuery();

                string emailQuery = "INSERT INTO Emails (Email) VALUES (@Email); SELECT LAST_INSERT_ID();";
                MySqlCommand emailCmd = new MySqlCommand(emailQuery, conn);
                emailCmd.Parameters.AddWithValue("@Email", email);
                int emailID = Convert.ToInt32(emailCmd.ExecuteScalar());

                string userEmailQuery = "INSERT INTO UserEmails (UserID, EmailID) VALUES (@UserID, @EmailID);";
                MySqlCommand userEmailCmd = new MySqlCommand(userEmailQuery, conn);
                userEmailCmd.Parameters.AddWithValue("@UserID", userID);
                userEmailCmd.Parameters.AddWithValue("@EmailID", emailID);
                userEmailCmd.ExecuteNonQuery();

                string locationQuery = "INSERT INTO Locations (UserID, Location) VALUES (@UserID, @Location);";
                MySqlCommand locationCmd = new MySqlCommand(locationQuery, conn);
                locationCmd.Parameters.AddWithValue("@UserID", userID);
                locationCmd.Parameters.AddWithValue("@Location", location);
                locationCmd.ExecuteNonQuery();

                Console.WriteLine("Record added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add record. Error: " + ex.Message);
            }
        }

        static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+\d{9,15}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
