namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class CitySqlCommands
    {
        public static string GetIdCity(string city) => $"SELECT * FROM Cities WHERE City = '{city}'";

        public static string GetCitiesById(int idState) => $"SELECT * FROM Cities WHERE IdState = {idState}";

        public static string Read(int IdCity) => $"SELECT * FROM Cities WHERE IdCity = {IdCity}";
    }
}
