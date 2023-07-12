using Advocacy_Software.Advocacy.Software.Entities;

namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class AddressSqlCommands
    {
        public static string Create(AddressEntityModel address) => $@"INSERT INTO address (Street, Number, Neighbourhood, 
        ZipCode, IdCity, Id, Complement, IdSignature) 
        VALUES ('{address.Street}', '{address.Number}', '{address.Neighbourhood}', '{address.ZipCode}', 
        '{address.IdCity}', '{address.Id}', '{address.Complement}', {address.IdSignature})";

        public static string Read(string id) => $"SELECT * FROM address WHERE Id = '{id}';";

        public static string Update(AddressEntityModel address) => $@"UPDATE address 
        SET Street = '{address.Street}', Number = '{address.Number}', Neighbourhood = '{address.Neighbourhood}', 
        ZipCode = '{address.ZipCode}', IdCity = '{address.IdCity}', Id = '{address.Id}', Complement = '{address.Complement}',
        IdSignature = {address.IdSignature}
        WHERE Id = '{address.Id}' AND IdAddress = {address.IdAddress}";

        public static string Delete(int IdAddress, int IdSignature) => $"DELETE FROM address WHERE IdAddress = {IdAddress} AND IdSignature = {IdSignature}";

        public static string Delete(int IdSignature) => $"DELETE FROM address WHERE IdSignature = {IdSignature}";
    }
}
