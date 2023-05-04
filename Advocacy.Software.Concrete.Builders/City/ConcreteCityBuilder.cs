using Advocacy_Software.Advocacy.Software.Builders;
using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Repositories.Read.Address;
using System;
using System.Collections.Generic;

namespace Advocacy_Software.Advocacy.Software.Concrete.Builders.City
{
    public class ConcreteCityBuilder : IBuilder
    {
        public List<Cities>? CitiesList { get; set; }
        
        public void Create<Things>(Things thing)
        {
            throw new NotImplementedException();
        }

        public void Delete(string sql)
        {
            throw new NotImplementedException();
        }

        public void Read<Things>(Things sql)
        {
            CitiesList = ReadCityRepository.Select(sql as string);
        }

        public void Update<Thing>(Thing thing)
        {
            throw new NotImplementedException();
        }
    }
}
