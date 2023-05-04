using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.Person
{
    public static class CreateCustomerRepository
    {
        public static void Save(Customers customer, string sql)
        {
            try
            {
                using SqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                SqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.Add(new SqlParameter("@Name", customer.Name));
                sqlCommand.Parameters.Add(new SqlParameter("@Nationality", customer.Nationality));
                sqlCommand.Parameters.Add(new SqlParameter("@CpfOrCnpj", customer.CpfOrCnpj));
                sqlCommand.Parameters.Add(new SqlParameter("@CivilStatus", customer.CivilStatus));
                sqlCommand.Parameters.Add(new SqlParameter("@Profession", customer.Profession));
                sqlCommand.Parameters.Add(new SqlParameter("@Email", customer.Email));
                sqlCommand.Parameters.Add(new SqlParameter("@Phone", customer.Phone));
                sqlCommand.Parameters.Add(new SqlParameter("@RegisterDate", customer.RegisterDate));
                sqlCommand.Parameters.Add(new SqlParameter("@LastUpdate", customer.LastUpdate));
                sqlCommand.Parameters.Add(new SqlParameter("@Id", customer.Id));
                sqlCommand.Parameters.Add(new SqlParameter("@IdAddress", customer.IdAddress));
                sqlCommand.Parameters.Add(new SqlParameter("@IdSignature", customer.IdSignature));
                sqlCommand.Parameters.Add(new SqlParameter("@IdentityLawyer", customer.IdentityCustomer));
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
