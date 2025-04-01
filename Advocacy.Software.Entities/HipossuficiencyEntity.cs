namespace Advocacy_Software.Advocacy.Software.Entities
{
    public class HipossuficiencyEntity
    {
        public List<Customers> Customer { get; set; }
        public List<Lawyer> Lawyer { get; set; }
        public Signatures Signature { get; set; }
        public AddressEntityModel AddressCustomer { get; set; }
        public AddressEntityModel AddressLawyer { get; set; }
        public List<Cities> City { get; set; }
        public List<Cities> CityLawyer{ get; set; }
        public string? UfCustomer { get; set; }
        public string? UfLawyer { get; set; }
        public string? LawyerCity { get; set; }
        public string? PdfFile { get; set; }
        public string? Subject { get; set; }
        public List<Lawyer> LawyerInFee { get; set; } = new();
        public string CityOffice { get; set; }
    }
}
