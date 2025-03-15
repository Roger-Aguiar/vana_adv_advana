namespace Advocacy_Software.Advocacy.Software.Repositories.Read.Person
{
    public class ReadCustomerRepository
    {
        public static List<Customers> Select(string sql)
        {
            var customers = new List<Customers>();

            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();
                using (MySqlCommand command = new(sql, connection))
                {
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        customers.Add(new Customers()
                        {
                            IdCustomer = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Nationality = reader.GetString(2),
                            CivilStatus = reader.GetString(3),
                            Profession = reader.GetString(4),
                            IdentityCustomer = reader.GetString(5),
                            CpfOrCnpj = reader.GetString(6),
                            Phone = reader.GetString(7),
                            Email = reader.GetString(8),
                            RegisterDate = reader.GetString(9).ToString(),
                            LastUpdate = reader.GetString(10).ToString(),
                            Id = reader.GetString(11),                                                        
                            IdSignature = reader.GetInt32(12),
                            IdAddress = reader.GetInt32(13)
                        });
                    }
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return customers;
        }
    }
}
