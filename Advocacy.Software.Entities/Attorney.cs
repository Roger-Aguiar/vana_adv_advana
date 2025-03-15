namespace Advocacy_Software.Advocacy.Software.Entities
{
    public class AttorneyEntity
    {
        public int IdAttorney { get; set; }
        public string? SpecificPowers { get; set; }
        public DateTime RegisterDate { get; set; }
        public int IdCustomer { get; set; }
        public string? Id { get; set; }
        public Customers Customer { get; set; }
        public Lawyer Lawyer { get; set; }
        public List<Cities> CityLawyer { get; set; }
        public List<Cities> CityCustomer { get; set; }
        public int IdSignature { get; set; }
        public Signatures Signature { get; set; }
        public AddressEntityModel AddressLawyer { get; set; }
        public AddressEntityModel AddressCustomer { get; set; }
        public string? PdfPath { get; set; }
        public string? UfCustomer { get; set;}
        public string? UfLawyer { get; set; }
        public string? Subject { get; set; }
    }
}
