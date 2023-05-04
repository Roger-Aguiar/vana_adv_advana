namespace Advocacy_Software.Advocacy.Software.Entities
{
    public class AddressEntityModel
    {
        public int IdAddress { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighbourhood { get; set; }
        public string? ZipCode { get; set; }
        public int IdCity { get; set; }
        public string? Id { get; set; }
        public string? Complement { get; set; }
        public int IdSignature { get; set; }
    }
}
