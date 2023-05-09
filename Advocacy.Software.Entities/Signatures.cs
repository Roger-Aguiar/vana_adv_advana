namespace Advocacy_Software.Advocacy.Software.Entities
{
    public class Signatures
    {
        public int IdSignature { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? GuidSignature { get; set; }
        public string? RegisterDate { get; set; }
        public decimal SignaturePrice { get; set; }
        public string? SignatureType { get; set; }
        public string? ImageProfile { get; set; }
        public string? Genre { get; set; }
        public string? UserType { get; set; }
        public string? DeadlineSignatureDate { get; set; }
        public string? VerificationCode { get; set; }
        public string? LogoHeader { get; set; }
        public string? LogoFooter { get; set; }
        public bool Update { get; set; }
    }
}
