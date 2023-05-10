using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using System;
using System.Windows;
using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.BankAccountOperations
{
    public static class CreateBankAccountRepository
    {        
        public static void Save(string sql, BankAccount bank)
        {
            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());

                MySqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.Add(new MySqlParameter("@BankName", bank.BankName));
                sqlCommand.Parameters.Add(new MySqlParameter("@AccountType", bank.AccountType));
                sqlCommand.Parameters.Add(new MySqlParameter("@AgencyNumber", bank.AgencyNumber));
                sqlCommand.Parameters.Add(new MySqlParameter("@AccountNumber", bank.AccountNumber));
                sqlCommand.Parameters.Add(new MySqlParameter("@Pix", bank.Pix));
                sqlCommand.Parameters.Add(new MySqlParameter("@IdLawyer", bank.IdLawyer));
                sqlCommand.Parameters.Add(new MySqlParameter("@Id", bank.Id));
                sqlCommand.Parameters.Add(new MySqlParameter("@PixType", bank.PixType));
                sqlCommand.Parameters.Add(new MySqlParameter("@IdSignature", bank.IdSignature));

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Operação realizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
