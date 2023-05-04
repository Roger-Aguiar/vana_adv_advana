using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.Person
{
    public static class CreateLawyerRepository
    {   
        public static void Save(Lawyer lawyer, string sql)
        {
            try
            {
                using SqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                SqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.Add(new SqlParameter("@Name", lawyer.Name));
                sqlCommand.Parameters.Add(new SqlParameter("@Nationality", lawyer.Nationality));
                sqlCommand.Parameters.Add(new SqlParameter("@CpfOrCnpj", lawyer.CpfOrCnpj));
                sqlCommand.Parameters.Add(new SqlParameter("@CivilStatus", lawyer.CivilStatus));
                sqlCommand.Parameters.Add(new SqlParameter("@Profession", lawyer.Profession));
                sqlCommand.Parameters.Add(new SqlParameter("@Email", lawyer.Email));
                sqlCommand.Parameters.Add(new SqlParameter("@Phone", lawyer.Phone));
                sqlCommand.Parameters.Add(new SqlParameter("@RegisterDate", lawyer.RegisterDate));
                sqlCommand.Parameters.Add(new SqlParameter("@LastUpdate", lawyer.LastUpdate));
                sqlCommand.Parameters.Add(new SqlParameter("@Id", lawyer.Id));
                sqlCommand.Parameters.Add(new SqlParameter("@IdAddress", lawyer.IdAddress));
                sqlCommand.Parameters.Add(new SqlParameter("@IdSignature", lawyer.IdSignature));
                sqlCommand.Parameters.Add(new SqlParameter("@OabNumber", lawyer.OabNumber));
                sqlCommand.Parameters.Add(new SqlParameter("@UfOab", lawyer.UfOab));
                sqlCommand.Parameters.Add(new SqlParameter("@IdentityLawyer", lawyer.IdentityLawyer));
                sqlCommand.Parameters.Add(new SqlParameter("@AppPassword", lawyer.AppPassword));
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
