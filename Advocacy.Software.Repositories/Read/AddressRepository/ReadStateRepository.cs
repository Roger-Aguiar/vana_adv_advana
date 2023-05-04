using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Advocacy_Software.Advocacy.Software.Repositories.Read.Address
{
    public static class ReadStateRepository
    {           
        public static List<States> Select(string sql)
        {
            List<States> states = new();

            try
            {
                using SqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();
                using (SqlCommand command = new(sql, connection))
                {
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        states.Add(new States() { IdState = reader.GetInt32(0), State = reader.GetString(1)});
                    }
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return states;
        }
    }
}
