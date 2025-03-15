namespace Advocacy_Software.Advocacy.Software.Concrete.Builders.Person
{
    public class ConcreteCustomerBuilder : IBuilder
    {
        public Customers Customer { get; set; }
        public List<Customers> CustomersList { get; set; }

        void IBuilder.Create<Things>(Things thing)
        {
            CreateCustomerRepository.Save(thing as Customers, CustomerSqlCommands.Create(thing as Customers));
        }

        void IBuilder.Delete(string sql)
        {
            DeleteRowRepository.Delete(sql);
        }

        void IBuilder.Read<Things>(Things sql)
        {
            CustomersList = ReadCustomerRepository.Select(sql as string);
        }

        void IBuilder.Update<Thing>(Thing thing)
        {
            CreateCustomerRepository.Save(thing as Customers, CustomerSqlCommands.Update(thing as Customers));
        }
    }
}
