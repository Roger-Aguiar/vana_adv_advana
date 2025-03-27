namespace Advocacy_Software.Advocacy.Software.Repositories.Create.Person
{
    public static class CreateLawyerRepository
    {   
        public static void Save(Lawyer lawyer, string sql)
        {
            try
            {
                using MySqlConnection connection = new(AzureStringConnection.GetStringConnection().ToString());
                MySqlCommand sqlCommand = new(sql, connection);
                sqlCommand.Parameters.Add(new MySqlParameter("@Name", lawyer.Name));
                sqlCommand.Parameters.Add(new MySqlParameter("@Profession", lawyer.Profession));
                sqlCommand.Parameters.Add(new MySqlParameter("@RegisterDate", lawyer.RegisterDate));
                sqlCommand.Parameters.Add(new MySqlParameter("@LastUpdate", lawyer.LastUpdate));
                sqlCommand.Parameters.Add(new MySqlParameter("@Id", lawyer.Id));
                sqlCommand.Parameters.Add(new MySqlParameter("@IdSignature", lawyer.IdSignature));
                sqlCommand.Parameters.Add(new MySqlParameter("@OabNumber", lawyer.OabNumber));
                sqlCommand.Parameters.Add(new MySqlParameter("@OabUf", lawyer.UfOab));
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
