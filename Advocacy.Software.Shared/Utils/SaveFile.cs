using Microsoft.Win32;

namespace Advocacy_Software.Advocacy.Software.Shared.Utils
{
    public class SaveFile
    {
        public static string Save(string documentType)
        {
            string file = "";

            SaveFileDialog saveFile = new()
            {
                Title = documentType
            };
            if (saveFile.ShowDialog() == true)
                file = $@"{saveFile.FileName}.pdf";
            return file;
        }
    }
}
