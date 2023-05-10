using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using System;
using System.Windows;
using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.Person
{
    public static class CreateCustomerRepository
    {
        public static void Save(Customers customer, string sql)
        {
            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                MySqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.Add(new MySqlParameter("@Name", customer.Name));
                sqlCommand.Parameters.Add(new MySqlParameter("@Nationality", customer.Nationality));
                sqlCommand.Parameters.Add(new MySqlParameter("@CpfOrCnpj", customer.CpfOrCnpj));
                sqlCommand.Parameters.Add(new MySqlParameter("@CivilStatus", customer.CivilStatus));
                sqlCommand.Parameters.Add(new MySqlParameter("@Profession", customer.Profession));
                sqlCommand.Parameters.Add(new MySqlParameter("@Email", customer.Email));
                sqlCommand.Parameters.Add(new MySqlParameter("@Phone", customer.Phone));
                sqlCommand.Parameters.Add(new MySqlParameter("@RegisterDate", customer.RegisterDate));
                sqlCommand.Parameters.Add(new MySqlParameter("@LastUpdate", customer.LastUpdate));
                sqlCommand.Parameters.Add(new MySqlParameter("@Id", customer.Id));
                sqlCommand.Parameters.Add(new MySqlParameter("@IdAddress", customer.IdAddress));
                sqlCommand.Parameters.Add(new MySqlParameter("@IdSignature", customer.IdSignature));
                sqlCommand.Parameters.Add(new MySqlParameter("@IdentityLawyer", customer.IdentityCustomer));
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Operação realizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
