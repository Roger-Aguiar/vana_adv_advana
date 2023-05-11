using Advocacy_Software.Advocacy.Software.Entities;

namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class CustomerSqlCommands
    {
        public static string Create(Customers customer) => $@"INSERT INTO Customers 
        (Name, Nationality, CivilStatus, Profession, RG, CpfOrCnpj, Phone, Email, 
        RegisterDate, LastUpdate, Id, IdAddress, IdSignature) 
        VALUES ('{customer.Name}', '{customer.Nationality}', '{customer.CivilStatus}', '{customer.Profession}', 
        '{customer.IdentityCustomer}', '{customer.CpfOrCnpj}', '{customer.Phone}', '{customer.Email}', 
        '{customer.RegisterDate}', '{customer.LastUpdate}', '{customer.Id}', {customer.IdAddress}, 
        {customer.IdSignature});";

        public static string Read(int IdSignature) => $"SELECT * FROM Customers WHERE IdSignature = {IdSignature}";

        public static string Update(Customers customer) => $@"UPDATE Customers 
        SET Name = '{customer.Name}', Nationality = '{customer.Nationality}', CivilStatus = '{customer.CivilStatus}', 
        Profession = '{customer.Profession}', RG = '{customer.IdentityCustomer}', CpfOrCnpj = '{customer.CpfOrCnpj}', 
        Phone = '{customer.Phone}', Email = '{customer.Email}', RegisterDate = '{customer.RegisterDate}', LastUpdate = '{customer.LastUpdate}', 
        Id = '{customer.Id}', IdAddress = {customer.IdAddress}, IdSignature = {customer.IdSignature} 
        WHERE Id = '{customer.Id}' AND IdSignature = {customer.IdSignature}";

        public static string Delete(Customers customer) => $"DELETE FROM Customers WHERE IdSignature = {customer.IdSignature} AND Id = '{customer.Id}'";

        public static string Delete(int IdSignature) => $"DELETE FROM Customers WHERE IdSignature = {IdSignature};";

        public static string Read(string CpfOrCnpj, int IdSignature) => $"SELECT * FROM Customers WHERE CpfOrCnpj = '{CpfOrCnpj}' AND IdSignature = {IdSignature}";
    }
}
