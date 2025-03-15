namespace Advocacy_Software.Advocacy.Software.Entities
{
    public class BankAccount
    {
        public int IdBankAccount { get; set; }
        public string? AccountType { get; set; }
        public string? AgencyNumber { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public int IdLawyer { get; set; }
        public string? Pix { get; set; }
        public string? UserBankAccount { get; set; }
        public string? Id { get; set; }
        public string? PixType { get; set; }
        public int IdSignature { get; set; }
        public List<Lawyer> Lawyers { get; set; }
    }
}
