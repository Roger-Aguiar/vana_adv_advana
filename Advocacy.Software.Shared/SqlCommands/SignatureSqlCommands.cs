namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class SignatureSqlCommands
    {
        public static string Insert() => $@"INSERT INTO signatures
            (FullName, Username, Email, Password, Id, RegisterDate, SignaturePrice, SignatureType, ImageProfile, Genre, DeadlineSignature, ImageHeader, ImageFooter)
            VALUES (@FullName, @Username, @Email, @Password, @Id, @RegisterDate, @SignaturePrice, @SignatureType, @ImageProfile, @Genre, @DeadlineSignature, @ImageHeader, @ImageFooter);";


        public static string Select() => "SELECT * FROM signatures";

        public static string DeleteUser(int id) => $"DELETE FROM signatures WHERE IdSignature = {id};";

        public static string SelectByEmail(string email) => $"SELECT * FROM signatures WHERE Email = '{email}'";

        public static string Update(int id) => $@"UPDATE signatures
            SET Username = @Username, Password = @Password, RegisterDate = @RegisterDate, 
            SignaturePrice = @SignaturePrice, SignatureType = @SignatureType, ImageProfile = @ImageProfile, 
            DeadlineSignature = @DeadlineSignature, ImageHeader = @ImageHeader, ImageFooter = @ImageFooter
            WHERE IdSignature = {id};";
    }
}
