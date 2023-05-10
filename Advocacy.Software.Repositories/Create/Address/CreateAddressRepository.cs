using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using System;
using System.Windows;
using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.Address
{
    public static class CreateAddressRepository
    {       
        public static void Save(AddressEntityModel address, string sql)
        {
            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());

                MySqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.Add(new MySqlParameter("@Street", address.Street));
                sqlCommand.Parameters.Add(new MySqlParameter("@Number", address.Number));
                sqlCommand.Parameters.Add(new MySqlParameter("@Neighbourhood", address.Neighbourhood));
                sqlCommand.Parameters.Add(new MySqlParameter("@ZipCode", address.ZipCode));
                sqlCommand.Parameters.Add(new MySqlParameter("@IdCity", address.IdCity));
                sqlCommand.Parameters.Add(new MySqlParameter("@Id", address.Id));
                sqlCommand.Parameters.Add(new MySqlParameter("@IdSignature", address.IdSignature));
                sqlCommand.Parameters.Add(new MySqlParameter("@Complement", address.Complement));

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
