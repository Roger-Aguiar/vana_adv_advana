using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using System;
using System.Windows;
using MySqlConnector;
using System.IO;
using iText.IO.Image;

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
                sqlCommand.Parameters.AddWithValue("@ImageProfile", Convert.ToBase64String(signature.ImageProfile));
                sqlCommand.Parameters.AddWithValue("@Genre", signature.Genre);
                sqlCommand.Parameters.AddWithValue("@DeadlineSignature", signature.DeadlineSignatureDate);
                sqlCommand.Parameters.AddWithValue("@ImageHeader", Convert.ToBase64String(signature.LogoHeader));
                sqlCommand.Parameters.AddWithValue("@ImageFooter", Convert.ToBase64String(signature.LogoFooter));

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
