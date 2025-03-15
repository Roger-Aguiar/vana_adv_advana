namespace Advocacy_Software.Advocacy.Software.Concrete.Builders.State
{
    public class ConcreteStateBuilder : IBuilder
    {        
        public List<States>? State { get; set; }

        public void Create<Things>(Things thing)
        {
            throw new NotImplementedException();
        }
                
        public void Read<Things>(Things sql)
        {            
            State = ReadStateRepository.Select(sql as string);
        }

        public void Update<Thing>(Thing thing)
        {
            throw new NotImplementedException();
        }

        public void Delete(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
