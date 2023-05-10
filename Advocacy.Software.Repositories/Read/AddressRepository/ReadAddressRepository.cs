using Advocacy_Software.Advocacy.Software.Shared;
using System.Windows;
using System;
using Advocacy_Software.Advocacy.Software.Entities;
using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Repositories.Read.AddressRepository
{
    public static class ReadAddressRepository
    {
        public static AddressEntityModel Select(string sql)
        {
            AddressEntityModel address = new();

            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();

                using (MySqlCommand command = new(sql, connection))
                {
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        address.IdAddress = Convert.ToInt32(reader["IdAddress"]);
                        address.Street = (string?)reader["Street"];
                        address.Number = (string?)reader["Number"];
                        address.Neighbourhood = (string?)reader["Neighbourhood"];
                        address.ZipCode = (string?)reader["ZipCode"];
                        address.IdCity = (int)reader["IdCity"];
                        address.Id = (string?)reader["Id"];
                        address.Complement = (string?)reader["Complement"];
                        address.IdSignature = (int)reader["IdSignature"];                        
                    }
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return address;
        }
    }
}
