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
                            BankName = reader.GetString(1),
                            AccountType = reader.GetString(2),
                            AgencyNumber = reader.GetString(3),                          
                            AccountNumber = reader.GetString(4),
                            Pix = reader.GetString(5),
                            PixType = reader.GetString(6),                                                                             
                            Id = reader.GetString(7),
                            IdLawyer = reader.GetInt32(8),     
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
