namespace Advocacy_Software.Advocacy.Software.Repositories.Read.Person
{
    public static class ReadAdministratorRepository
    {
        public static Signatures SelectUser(string sql)
        {
            var user = new Signatures();

            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();

                using (MySqlCommand command = new(sql, connection))
                {
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user.IdSignature = reader.GetInt32(0);
                        user.FullName = reader.GetString(1);
                        user.Username = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.Password = reader.GetString(4);
                        user.RegisterDate = reader.GetString(5);
                        user.SignaturePrice = reader.GetDecimal(6);
                        user.SignatureType = reader.GetString(7);
                        user.Genre = reader.GetString(8);
                        user.DeadlineSignatureDate = reader.GetString(9);
                        user.ImageProfile = reader["ImageProfile"] is DBNull ? null : reader.GetString(10);
                        user.LogoHeader = (string?)reader["ImageHeader"];
                        user.LogoFooter = (string?)reader["ImageFooter"];
                        user.GuidSignature = reader.GetString(13);    
                    }
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return user;
        }
    }
}
