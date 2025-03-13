using Advocacy_Software.Advocacy.Software.Entities;
using System;
using System.Windows;
using MySqlConnector;
using Advocacy_Software.Advocacy.Software.Shared.Database;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.Person
{
    public static class CreateAdministratorRepository
    {       
        public static void Save(Signatures signature, string sql)
        {
            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());

                MySqlCommand sqlCommand = new(sql, connection);

                sqlCommand.Parameters.AddWithValue("@FullName", signature.FullName);
                sqlCommand.Parameters.AddWithValue("@Username", signature.Username);
                sqlCommand.Parameters.AddWithValue("@Email", signature.Email);
                sqlCommand.Parameters.AddWithValue("@Password", signature.Password);
                sqlCommand.Parameters.AddWithValue("@Id", signature.GuidSignature);
                sqlCommand.Parameters.AddWithValue("@RegisterDate", signature.RegisterDate);
                sqlCommand.Parameters.AddWithValue("@SignaturePrice", signature.SignaturePrice);
                sqlCommand.Parameters.AddWithValue("@SignatureType", signature.SignatureType);
                sqlCommand.Parameters.AddWithValue("@ImageProfile", signature.ImageProfile);
                sqlCommand.Parameters.AddWithValue("@Genre", signature.Genre);
                sqlCommand.Parameters.AddWithValue("@DeadlineSignature", signature.DeadlineSignatureDate);
                sqlCommand.Parameters.AddWithValue("@ImageHeader", signature.LogoHeader);
                sqlCommand.Parameters.AddWithValue("@ImageFooter", signature.LogoFooter);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        
    }
}
