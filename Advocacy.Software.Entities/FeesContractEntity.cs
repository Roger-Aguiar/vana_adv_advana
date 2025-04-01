namespace Advocacy_Software.Advocacy.Software.Entities
{
    public class FeesContractEntity
    {
        public List<Customers> Customer { get; set; }
        public List<Lawyer> Lawyer { get; set; }
        public List<Lawyer> LawyerInFee { get; set; } = new();
        public List<Cities> CityLawyer { get; set; }
        public List<Cities> CityCustomer { get; set; }
        public Signatures Signature { get; set; }
        public AddressEntityModel AddressLawyer { get; set; }
        public AddressEntityModel AddressCustomer { get; set; }
        public List<BankAccount> BankAccount { get; set; }
        public string? ActionName { get; set; }
        public decimal TotalServiceValue { get; set; }
        public string? SuccessFees { get; set; }
        public int InstallmentsNumber { get; set; }
        public decimal InitialValue { get; set; }
        public string? City { get; set; }
        public string? HeaderPath { get; set; }
        public string? FooterPath { get; set; }
        public string? PdfPath { get; set; }
        public string? LawyerCity { get; set; }        
        public string? PaymentType { get; set; }
        public string? Subject { get; set; }
        public int IdSignature { get; set; }
        public string? UfCustomer { get; set; }
        public string? UfLawyer { get; set; }
        public string? Uf { get; set; }
        public string? CpfOrCnpj { get; set; }
        public string? CityOffice { get; set; }
    }
}
