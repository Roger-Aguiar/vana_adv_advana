using Advocacy_Software.Advocacy.Software.Entities;

namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class BankAccountSqlCommands
    {
        public static string Create(BankAccount bank) => $@"INSERT INTO bankaccount (BankName, AccountType, AgencyNumber, 
        AccountNumber, Pix, IdLawyer, Id, PixType, IdSignature)
        VALUES('{bank.BankName}', '{bank.AccountType}', '{bank.AgencyNumber}', '{bank.AccountNumber}', '{bank.Pix}', {bank.IdLawyer}, 
        '{bank.Id}', '{bank.PixType}', {bank.IdSignature});";

        public static string Read(int IdSignature) => $"SELECT * FROM bankaccount WHERE IdSignature = {IdSignature};";

        public static string Update(BankAccount bank) => $@"UPDATE bankaccount 
        SET BankName = '{bank.BankName}', AccountType = '{bank.AccountType}', AgencyNumber = '{bank.AgencyNumber}', 
        AccountNumber = '{bank.AccountNumber}', Pix = '{bank.Pix}', PixType = '{bank.PixType}'
        WHERE Id = '{bank.Id}' AND IdSignature = {bank.IdSignature};";

        public static string Delete(BankAccount bank) => $"DELETE FROM bankaccount WHERE IdBankAccount = {bank.IdBankAccount} AND IdSignature = {bank.IdSignature};";

        public static string Delete(Lawyer lawyer) => $"DELETE FROM bankaccount WHERE IdLawyer = {lawyer.IdLawyer} AND IdSignature = {lawyer.IdSignature};";
        
        public static string Delete(int IdSignature) => $"DELETE FROM bankaccount WHERE IdSignature = {IdSignature};";

    }
}
