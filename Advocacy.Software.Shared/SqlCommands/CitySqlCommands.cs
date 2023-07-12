namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class CitySqlCommands
    {
        public static string GetIdCity(string city) => $"SELECT * FROM cities WHERE City = '{city}'";

        public static string GetCitiesById(int idState) => $"SELECT * FROM cities WHERE IdState = {idState}";

        public static string Read(int IdCity) => $"SELECT * FROM cities WHERE IdCity = {IdCity}";
    }
}
