using Advocacy_Software.Advocacy.Software.Builders;
using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Repositories.Create.Person;
using Advocacy_Software.Advocacy.Software.Repositories.Delete;
using Advocacy_Software.Advocacy.Software.Repositories.Read.Person;
using Advocacy_Software.Advocacy.Software.Shared.SqlCommands;
using System.Collections.Generic;

namespace Advocacy_Software.Advocacy.Software.Concrete.Builders.Person
{
    public class ConcreteLawyerBuilder : IBuilder
    {        
        public Lawyer? Lawyer { get; set; }
        public List<Lawyer> Lawyers { get; set; }

        public void Create<Things>(Things thing)
        {            
            CreateLawyerRepository.Save(thing as Lawyer, LawyerSqlCommands.Create(thing as Lawyer));
        }

        public void Delete(string sql)
        {
            DeleteRowRepository.Delete(sql);
        }

        public void Read<Things>(Things sql)
        {
            Lawyers = ReadLawyerRepository.Select(sql as string);
        }
               
        public void Update<Thing>(Thing thing)
        {
            CreateLawyerRepository.Save(thing as Lawyer, LawyerSqlCommands.Update(thing as Lawyer));
        }
    }
}
