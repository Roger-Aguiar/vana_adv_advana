namespace Advocacy_Software.Advocacy.Software.Entities
{
    public class Lawyer
    {
        public int IdLawyer { get; set; }
        public string? Name { get; set; }
        public string? Profession { get; set; }
        public string? RegisterDate { get; set; }
        public string? LastUpdate { get; set; }
        public string? Id { get; set; }
        public string? OabNumber { get; set; }
        public string? UfOab { get; set; }
        public int IdSignature { get; set; }
        public Signatures Signature { get; }
    }
}
