using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.Address
{
    public static class CreateAddressRepository
    {       
        public static void Save(AddressEntityModel address, string sql)
        {
            try
            {
                using SqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());

                SqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.Add(new SqlParameter("@Street", address.Street));
                sqlCommand.Parameters.Add(new SqlParameter("@Number", address.Number));
                sqlCommand.Parameters.Add(new SqlParameter("@Neighbourhood", address.Neighbourhood));
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", address.ZipCode));
                sqlCommand.Parameters.Add(new SqlParameter("@IdCity", address.IdCity));
                sqlCommand.Parameters.Add(new SqlParameter("@Id", address.Id));
                sqlCommand.Parameters.Add(new SqlParameter("@IdSignature", address.IdSignature));
                sqlCommand.Parameters.Add(new SqlParameter("@Complement", address.Complement));

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
