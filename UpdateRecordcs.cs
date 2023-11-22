using MySql.Data.MySqlClient;
using System;

namespace _Database
{
    class UpdateRecord
    {
        public static void Execute(MySqlConnection conn)
        {
            Console.WriteLine("Enter UserID of the user to update:");
            string userID = Console.ReadLine();

            Console.WriteLine("Enter the field name to update (e.g., Surname, Forenames, Title, Phone, Email, Location):");
            string field = Console.ReadLine();

            Console.WriteLine($"Enter the new value for {field}:");
            string newValue = Console.ReadLine();

            try
            {
                string query = GetUpdateQuery(field, userID, newValue);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NewValue", newValue);
                cmd.Parameters.AddWithValue("@UserID", userID);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("Record updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update record. Make sure the UserID and field name are correct.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during update: " + ex.Message);
            }
        }

        private static string GetUpdateQuery(string field, string userID, string newValue)
        {
            switch (field.ToLower())
            {
                case "phone":
                    return @"
                    UPDATE Phones p 
                    JOIN UserPhones up ON p.PhoneID = up.PhoneID 
                    SET p.Phone = @NewValue 
                    WHERE up.UserID = @UserID";
                case "email":
                    return @"
                    UPDATE Emails e 
                    JOIN UserEmails ue ON e.EmailID = ue.EmailID 
                    SET e.Email = @NewValue 
                    WHERE ue.UserID = @UserID";
                case "location":
                    return "UPDATE Locations SET Location = @NewValue WHERE UserID = @UserID";
                default:
                    return $"UPDATE Users SET {field} = @NewValue WHERE UserID = @UserID";
            }
        }
    }
}
