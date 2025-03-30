namespace Advocacy_Software.Advocacy.Software.Shared.Utils
{
    public class CharacterOperations
    {
        public static string RemoveEpecialCharacters(string input) => Regex.Replace(input, @"\D", "");
    }
}
