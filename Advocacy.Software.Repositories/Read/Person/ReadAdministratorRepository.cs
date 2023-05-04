using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using System;
using System.Windows;
using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Repositories.Read.Person
{
    public static class ReadAdministratorRepository
    {
        public static Signatures SelectUser(string sql)
        {
            var user = new Signatures();

            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();

                using (MySqlCommand command = new(sql, connection))
                {
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user.IdSignature = reader.GetInt32(0);
                        user.FullName = reader.GetString(1);
                        user.Username = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.Password = reader.GetString(4);
                        user.GuidSignature = reader.GetString(5);
                        user.RegisterDate = reader.GetString(6);
                        user.SignaturePrice = reader.GetDecimal(7);
                        user.SignatureType = reader.GetString(8);
                        user.ImageProfile = (!Convert.IsDBNull(reader["ImageProfile"])) ? (byte[])reader["ImageProfile"] : null;
                        user.Genre = reader.GetString(10);
                        user.LogoHeader = (!Convert.IsDBNull(reader["LogoHeader"])) ? (byte[])reader["LogoHeader"] : null;
                        user.LogoFooter = (!Convert.IsDBNull(reader["LogoFooter"])) ? (byte[])reader["LogoFooter"] : null;
                        user.DeadlineSignatureDate = reader.GetString(11);
                    }
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return user;
        }
    }
}
