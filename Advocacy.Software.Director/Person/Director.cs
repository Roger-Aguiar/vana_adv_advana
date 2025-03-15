namespace Advocacy_Software.Advocacy.Software.Director.Person
{
    public class Director
    {
        private IBuilder? _builder;

        public IBuilder Builder
        {
            set { _builder = value; }
        }

        public void Create<Things>(Things thing)
        {
            _builder?.Create(thing);
        }

        public void Delete(string sql)
        {
            _builder?.Delete(sql);
        }

        public void Read<Thing>(Thing sql)
        {
            _builder?.Read(sql);
        }

        public void Update<Thing>(Thing thing)
        {
            _builder?.Update(thing);
        }
    }
}
