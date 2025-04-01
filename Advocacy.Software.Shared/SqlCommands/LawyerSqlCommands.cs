namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class LawyerSqlCommands
    {
        public static string Create(Lawyer lawyer) => $@"INSERT INTO lawyers 
        (Name, Profession, RegisterDate, LastUpdate, Id, OabNumber, OabUf, IdSignature) 
        VALUES ('{lawyer.Name}', '{lawyer.Profession}', '{lawyer.RegisterDate}', 
       '{lawyer.LastUpdate}', '{lawyer.Id}', '{lawyer.OabNumber}', '{lawyer.UfOab}', 
        {lawyer.IdSignature});";

        public static string Read(int IdSignature) => $"SELECT * FROM lawyers WHERE IdSignature = {IdSignature}";

        public static string Update(Lawyer lawyer) => $@"UPDATE lawyers 
        SET Name = '{lawyer.Name}', 
        Profession = '{lawyer.Profession}', RegisterDate = '{lawyer.RegisterDate}', 
        LastUpdate = '{lawyer.LastUpdate}', Id = '{lawyer.Id}', OabNumber = '{lawyer.OabNumber}', 
        OabUf = '{lawyer.UfOab}', IdSignature = {lawyer.IdSignature} 
        WHERE Id = '{lawyer.Id}' AND IdSignature = {lawyer.IdSignature}";

        public static string Delete(Lawyer lawyer) => $"DELETE FROM lawyers WHERE IdSignature = {lawyer.IdSignature} AND Id = '{lawyer.Id}'";

        public static string Delete(int IdSignature) => $"DELETE FROM lawyers WHERE IdSignature = {IdSignature}";

        public static string Read(Lawyer lawyer) => $"SELECT * FROM lawyers WHERE Name = '{lawyer.Name}' AND IdSignature = {lawyer.IdSignature};";
    }
}
