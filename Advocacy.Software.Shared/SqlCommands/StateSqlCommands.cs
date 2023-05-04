namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public class StateSqlCommands
    {
        public static string GetIdState(string state) => $"SELECT * FROM States WHERE State = '{state}'";
        
        public static string Select(int IdState) => $"SELECT * FROM States WHERE IdState = {IdState}";
    }
}
