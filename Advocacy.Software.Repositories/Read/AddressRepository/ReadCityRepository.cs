using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Advocacy_Software.Advocacy.Software.Repositories.Read.Address
{
    public static class ReadCityRepository
    {        
        public static List<Cities> Select(string sql)
        {
            var cityList = new List<Cities>();
            try
            {
                using SqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    cityList.Add(new Cities() { IdCity = reader.GetInt32(0), City = reader.GetString(1), IdState = reader.GetInt32(2) });

                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return cityList;
        }
    }
}
