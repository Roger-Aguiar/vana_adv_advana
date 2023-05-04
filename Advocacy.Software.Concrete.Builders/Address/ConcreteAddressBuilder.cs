using Advocacy_Software.Advocacy.Software.Builders;
using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Repositories.Create.Address;
using Advocacy_Software.Advocacy.Software.Repositories.Delete;
using Advocacy_Software.Advocacy.Software.Repositories.Read.AddressRepository;
using Advocacy_Software.Advocacy.Software.Shared.SqlCommands;

namespace Advocacy_Software.Advocacy.Software.Concrete.Builders.Address
{
    public class ConcreteAddressBuilder : IBuilder
    {
        public AddressEntityModel Address { get; set; }

        public void Create<Things>(Things thing)
        {
            CreateAddressRepository.Save(thing as AddressEntityModel, AddressSqlCommands.Create(thing as AddressEntityModel));
        }

        public void Delete(string sql)
        {
            DeleteRowRepository.Delete(sql);
        }

        public void Read<Things>(Things sql)
        {
            Address = ReadAddressRepository.Select(sql as string);
        }

        public void Update<Thing>(Thing thing)
        {
            CreateAddressRepository.Save(thing as AddressEntityModel, AddressSqlCommands.Update(thing as AddressEntityModel));
        }
    }
}
