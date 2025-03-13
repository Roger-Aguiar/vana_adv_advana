using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Shared.Database
{
    public static class AzureStringConnection
    {           
        public static MySqlConnectionStringBuilder GetStringConnection()
        {
            MySqlConnectionStringBuilder builder = new()
            {
                Server = "localhost",
                Database = "advocacy_database",
                UserID = "root",
                Password = "983453069",
                SslMode = MySqlSslMode.Required,
            };
            return builder;
        }
    }
}
