namespace Advocacy_Software.Advocacy.Software.Concrete.Builders.BankAccountBuilder
{
    public class ConcreteBankAccountBuilder : IBuilder
    {
        public BankAccount BankAccount { get; set; }
        public List<BankAccount> BankAccountList { get; set; }

        public void Create<Things>(Things thing) => CreateBankAccountRepository.Save(BankAccountSqlCommands.Create(thing as BankAccount), thing as BankAccount);

        public void Delete(string sql)
        {
            DeleteRowRepository.Delete(sql);
        }

        public void Read<Things>(Things sql) => BankAccountList = ReadBankAccountRepository.Read(sql as string);

        public void Update<Thing>(Thing thing)
        {
            CreateBankAccountRepository.Save(BankAccountSqlCommands.Update(thing as BankAccount), thing as BankAccount);
        }
    }
}
