using Microsoft.Data.SqlClient;

namespace Advocacy_Software.Advocacy.Software.Shared
{
    public static class AzureStringConnection
    {        
        public static SqlConnectionStringBuilder GetStringConnection()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "advocacy.database.windows.net",
                UserID = "roger",
                Password = "kK!P7A3USaEB\"B8$",
                InitialCatalog = "Advocacy_Database",
                ConnectTimeout = 240
            };
            return builder;
        }
    }
}
