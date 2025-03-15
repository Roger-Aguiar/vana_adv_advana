namespace Advocacy_Software.Advocacy.Software.Concrete.Builders.Person
{
    public class ConcreteSignatureBuilder : IBuilder
    {
        public Signatures User { get; set; }        

        public void Create<Things>(Things thing)
        {
            CreateAdministratorRepository.Save(thing as Signatures, SignatureSqlCommands.Insert());
        }

        public void Read<Things>(Things sql)
        {
            User = ReadAdministratorRepository.SelectUser(sql as string);
        }

        public void Update<Thing>(Thing thing)
        {
            var signature = thing as Signatures;
            CreateAdministratorRepository.Save(signature, SignatureSqlCommands.Update(signature.IdSignature));
        }

        public void Delete(string sql)
        {
            DeleteAdministratorRepository.Delete(sql);
        }
    }
}
