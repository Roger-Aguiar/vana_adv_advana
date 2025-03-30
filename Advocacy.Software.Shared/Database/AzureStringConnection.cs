namespace Advocacy_Software.Advocacy.Software.Shared.Database
{
    public static class AzureStringConnection
    {           
        public static MySqlConnectionStringBuilder GetStringConnection()
        {
            //var builder = GetStringConnectionWeb();
            var builder = GetStringConnectionLocalhost();
            return builder;
        }

        private static MySqlConnectionStringBuilder GetStringConnectionLocalhost()
        {
            return new()
            {
                Server = "localhost",
                Database = "advocacy_database",
                UserID = "root",
                Password = "983453069",
                SslMode = MySqlSslMode.Required,
            };
        }

        private static MySqlConnectionStringBuilder GetStringConnectionWeb()
        {
            return new()
            {
                Server = "bjthjgepvhxsr8sdyoxt-mysql.services.clever-cloud.com",
                Database = "bjthjgepvhxsr8sdyoxt",
                UserID = "ui514qgguvqlvged",
                Password = "jakXPMWnOZIgmNWQnRvf",
                SslMode = MySqlSslMode.Required,
            };
        }
    }
}
