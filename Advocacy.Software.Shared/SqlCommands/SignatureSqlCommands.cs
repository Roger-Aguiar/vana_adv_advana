namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class SignatureSqlCommands
    {
        public static string Insert() => $@"INSERT INTO Signatures
            (FullName, Username, Email, Password, Id, RegisterDate, SignaturePrice, SignatureType, ImageProfile, Genre, DeadlineSignature, ImageHeader, ImageFooter)
            VALUES (@FullName, @Username, @Email, @Password, @Id, @RegisterDate, @SignaturePrice, @SignatureType, @ImageProfile, @Genre, @DeadlineSignature, @ImageHeader, @ImageFooter);";


        public static string Select() => "SELECT * FROM Signatures";

        public static string DeleteUser(int id) => $"DELETE FROM Signatures WHERE IdSignature = {id};";

        public static string SelectByEmail(string email) => $"SELECT * FROM Signatures WHERE Email = '{email}'";

        public static string Update(int id) => $@"UPDATE Signatures
            SET Username = @Username, Password = @Password, RegisterDate = @RegisterDate, 
            SignaturePrice = @SignaturePrice, SignatureType = @SignatureType, ImageProfile = @ImageProfile, 
            DeadlineSignatureDate = @DeadlineSignatureDate, LogoHeader = @LogoHeader, LogoFooter = @LogoFooter
            WHERE IdSignature = {id};";
    }
}
