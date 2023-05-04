namespace Advocacy_Software.Advocacy.Software.Builders
{
    public interface IBuilder
    {
        void Create<Things>(Things thing);
        void Read<Things>(Things sql);
        void Update<Thing>(Thing thing);
        void Delete(string sql);
    }
}
