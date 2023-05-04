using Advocacy_Software.Advocacy.Software.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace Advocacy_Software.Advocacy.Software.Repositories.Delete
{
    public static class DeleteRowRepository
    {
        public static void Delete(string sql)
        {
            try
            {
                using SqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();
                using (SqlCommand command = new(sql, connection))
                {
                    command.ExecuteNonQuery();
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
