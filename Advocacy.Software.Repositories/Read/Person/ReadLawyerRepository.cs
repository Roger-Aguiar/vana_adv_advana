using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using System;
using System.Collections.Generic;
using System.Windows;
using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Repositories.Read.Person
{
    public static class ReadLawyerRepository
    {
        public static List<Lawyer> Select(string sql)
        {
            var lawyers = new List<Lawyer>();

            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();
                using (MySqlCommand command = new(sql, connection))
                {
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lawyers.Add(new Lawyer()
                        {
                            IdLawyer = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Nationality = reader.GetString(2),
                            CivilStatus = reader.GetString(3),
                            Profession = reader.GetString(4),
                            Phone = reader.GetString(5),
                            Email = reader.GetString(6),
                            RegisterDate = reader.GetString(7).ToString(),
                            LastUpdate = reader.GetString(8).ToString(),
                            OabNumber = reader.GetString(9),
                            UfOab = reader.GetString(10),
                            Id = reader.GetString(11),
                            IdSignature = reader.GetInt32(12),
                            IdAddress = reader.GetInt32(13),                           
                            AppPassword = reader.GetString(14)
                        });
                    }
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lawyers;
        }
    }
}
