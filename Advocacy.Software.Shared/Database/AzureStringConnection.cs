using MySqlConnector;

namespace Advocacy_Software.Advocacy.Software.Shared
{
    public static class AzureStringConnection
    {           
        public static MySqlConnectionStringBuilder GetStringConnection()
        {
            MySqlConnectionStringBuilder builder = new()
            {
                Server = "35.224.1.120",
                Database = "advocacy_database",
                UserID = "root",
                Password = "983453069",
                SslMode = MySqlSslMode.Required,
            };
            return builder;
        }
    }
}
