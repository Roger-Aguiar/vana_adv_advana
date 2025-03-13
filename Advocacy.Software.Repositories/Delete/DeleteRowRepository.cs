using System;
using System.Windows;
using MySqlConnector;
using Advocacy_Software.Advocacy.Software.Shared.Database;

namespace Advocacy_Software.Advocacy.Software.Repositories.Delete
{
    public static class DeleteRowRepository
    {
        public static void Delete(string sql)
        {
            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();
                using (MySqlCommand command = new(sql, connection))
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
