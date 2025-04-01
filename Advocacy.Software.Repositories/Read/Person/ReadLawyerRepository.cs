namespace Advocacy_Software.Advocacy.Software.Repositories.Read.Person
{
    public static class ReadLawyerRepository
    {
        public static List<Lawyer> Select(string sql)
        {
            var lawyers = new List<Lawyer>();

            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                connection.Open();
                using (MySqlCommand command = new(sql, connection))
                {
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lawyers.Add(new Lawyer()
                        {
                            IdLawyer = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Profession = reader.GetString(2),
                            RegisterDate = reader.GetString(3).ToString(),
                            LastUpdate = reader.GetString(4).ToString(),
                            OabNumber = reader.GetString(5),
                            UfOab = reader.GetString(6),
                            Id = reader.GetString(7),
                            IdSignature = reader.GetInt32(8),
                        });
                    }
                };
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lawyers;
        }
    }
}
