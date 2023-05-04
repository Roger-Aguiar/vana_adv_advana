using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace Advocacy_Software.Advocacy.Software.Repositories.Create.Person
{
    public static class CreateAdministratorRepository
    {       
        public static void Save(Signatures signature, string sql)
        {
            try
            {
                using SqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                SqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.AddWithValue("@FullName", signature.FullName);
                sqlCommand.Parameters.AddWithValue("@Username", signature.Username);
                sqlCommand.Parameters.AddWithValue("@Email", signature.Email);
                sqlCommand.Parameters.AddWithValue("@Password", signature.Password);
                sqlCommand.Parameters.AddWithValue("@GuidSignature", signature.GuidSignature);
                sqlCommand.Parameters.AddWithValue("@RegisterDate", signature.RegisterDate);
                sqlCommand.Parameters.AddWithValue("@SignaturePrice", signature.SignaturePrice);
                sqlCommand.Parameters.AddWithValue("@SignatureType", signature.SignatureType);
                sqlCommand.Parameters.Add("@ImageProfile", System.Data.SqlDbType.Image, signature.ImageProfile.Length).Value = signature.ImageProfile;
                sqlCommand.Parameters.AddWithValue("@Genre", signature.Genre);
                sqlCommand.Parameters.AddWithValue("@DeadlineSignatureDate", signature.DeadlineSignatureDate);
                sqlCommand.Parameters.Add("@LogoHeader", System.Data.SqlDbType.Image, signature.LogoHeader.Length).Value = signature.LogoHeader;
                sqlCommand.Parameters.Add("@LogoFooter", System.Data.SqlDbType.Image, signature.LogoFooter.Length).Value = signature.LogoFooter;
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
