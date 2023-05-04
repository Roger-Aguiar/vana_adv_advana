using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.BankAccountOperations
{
    public static class CreateBankAccountRepository
    {        
        public static void Save(string sql, BankAccount bank)
        {
            try
            {
                using SqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());

                SqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.Add(new SqlParameter("@BankName", bank.BankName));
                sqlCommand.Parameters.Add(new SqlParameter("@AccountType", bank.AccountType));
                sqlCommand.Parameters.Add(new SqlParameter("@AgencyNumber", bank.AgencyNumber));
                sqlCommand.Parameters.Add(new SqlParameter("@AccountNumber", bank.AccountNumber));
                sqlCommand.Parameters.Add(new SqlParameter("@Pix", bank.Pix));
                sqlCommand.Parameters.Add(new SqlParameter("@IdLawyer", bank.IdLawyer));
                sqlCommand.Parameters.Add(new SqlParameter("@Id", bank.Id));
                sqlCommand.Parameters.Add(new SqlParameter("@PixType", bank.PixType));
                sqlCommand.Parameters.Add(new SqlParameter("@IdSignature", bank.IdSignature));

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
