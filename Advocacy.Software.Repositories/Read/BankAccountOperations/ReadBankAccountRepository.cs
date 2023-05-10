using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using System;
using System.Collections.Generic;
using System.Windows;
using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Repositories.Read.BankAccountOperations
{
    public static class ReadBankAccountRepository
    {
        public static List<BankAccount> Read(string sql)
        {
            var accounts = new List<BankAccount>();

            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();
                using (MySqlCommand command = new(sql, connection))
                {
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        accounts.Add(new BankAccount()
                        {
                            IdBankAccount = reader.GetInt32(0),
                            AccountType = reader.GetString(1),
                            AgencyNumber = reader.GetString(2),
                            BankName = reader.GetString(3),
                            IdLawyer = reader.GetInt32(4),
                            AccountNumber = reader.GetString(5),
                            Pix = reader.GetString(6),
                            Id = reader.GetString(7),
                            PixType = reader.GetString(8),
                            IdSignature = reader.GetInt32(9)
                        });
                    }
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return accounts;
        }
    }
}
