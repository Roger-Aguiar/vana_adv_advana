using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using iText.Layout;
using iText.IO.Image;
using iText.Kernel.Events;
using Advocacy_Software.Advocacy.Software.Entities;
using System.Globalization;

namespace Advocacy_Software.Advocacy.Software.Shared.Utils
{
    public class AttorneyGenerator
    {        
        public void GenerateAttorney(AttorneyEntity attorney)
        {
            using (PdfWriter pdfWriter = new(attorney.PdfPath, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)))
            {
                var format = new DocumentFormat();
                var pdfDocument = new PdfDocument(pdfWriter);
                var document = new Document(pdfDocument, PageSize.A4);

                if (attorney.Signature.LogoHeader != null || attorney.Signature.LogoFooter != null)
                {
                    var documentHeader = new Document(pdfDocument, PageSize.A4);
                    var documentFooter = new Document(pdfDocument, PageSize.A4);

                    Image? imageHeader = attorney.Signature.LogoHeader != null ? new Image(ImageDataFactory.Create(attorney.Signature.LogoHeader)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER) : null;
                    Image? imageFooter = attorney.Signature.LogoFooter != null ? new Image(ImageDataFactory.Create(attorney.Signature.LogoFooter)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER) : null;
                    documentHeader.SetMargins(0, 0, 0, 0);
                    document.SetMargins(130, 50, 130, 50);
                    documentFooter.SetMargins(765, 0, 0, 0);
                    documentHeader.Add(imageHeader);
                    documentFooter.Add(imageFooter);
                }
                else
                {
                    document.SetMargins(85, 50, 50, 50);
                    pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new EndPageEventHandler(document, pdfDocument, attorney.Lawyer));
                }

                document.Add(format.SetTitle("PROCURAÇÃO"));

                document.Add(format.SetTitle("OUTORGANTE"));

                document.Add(format.SetBodyAsJustified(SetCustomerData(attorney)));

                document.Add(format.SetTitle("OUTORGADO"));

                document.Add(format.SetBodyAsJustified(SetLawyerData(attorney)));

                document.Add(format.SetTitle("PODERES"));

                document.Add(format.SetBodyAsJustified("Amplos, para o foro em geral, com a cláusula ad judicia para, em qualquer Juízo, Tribunal ou Repartição Pública, propor contra quem de direito as ações competentes e defendê-lo(s) nas contrárias, seguindo umas e outras, até final decisão, usando os recursos legais e acompanhando-os, conferindo-lhe, ainda, os poderes especiais para dar e receber quitação, confessar, transigir, desistir, renunciar ao direito, retirar alvarás em Cartório, podendo, ainda substabelecer, no todo ou em parte, com ou sem reservas de poderes."));

                document.Add(format.SetTitle("PODERES ESPECÍFICOS"));

                document.Add(format.SetBody($"Atuar junto ao processo {attorney.SpecificPowers?.ToUpper()}."));

                document.Add(format.SetTitle("RESSALVA"));

                document.Add(format.SetBody("Nos poderes ora conferidos não está o de confessar em juízo ou fora dele."));

                document.Add(format.SetTitle("VALIDADE"));

                document.Add(format.SetBody("A presente procuração tem duração até o fim da ação.\n"));

                document.Add(format.SetBody($"{attorney.CityLawyer[0].City}, {DateTime.Now.ToString("D", (new CultureInfo("pt-BR")))}"));

                document.Add(new Paragraph("\n\n"));

                document.Add(format.SetTitle("___________________________________________________________"));

                document.Add(format.SetTitle("(Assinatura)"));

                document.Add(format.SetBody($"{attorney.Customer.Name?.ToUpper()}"));

                document.Close();
                pdfDocument.Close();
            };
        }

        private string SetCustomerData(AttorneyEntity attorney)
        {
            string body;

            var CpfOrCnpj = attorney.Customer.CpfOrCnpj?.Length == 11 ? "CPF: " + Convert.ToInt64(attorney.Customer.CpfOrCnpj).ToString(@"000\.000\.000-00") : "CNPJ" + Convert.ToInt64(attorney.Customer.CpfOrCnpj).ToString(@"00\.000\.000/0000-00");
            var phone = attorney.Customer.Phone?.Length == 11 ? Convert.ToInt64(attorney.Customer.Phone).ToString(@"(00)00000-0000") : Convert.ToInt64(attorney.Customer.Phone).ToString(@"(00)0000-0000");
            var zipCode = Convert.ToInt64(attorney.AddressCustomer.ZipCode).ToString(@"00000-000");
            var complement = attorney.AddressCustomer.Complement != " " ? attorney.AddressCustomer.Complement + ", " : ""; 
            body = $"{attorney.Customer.Name.ToUpper()}, {attorney.Customer.Nationality}, {attorney.Customer.CivilStatus}, {attorney.Customer.Profession}, portador(a) do RG: {attorney.Customer.IdentityCustomer}, {CpfOrCnpj}, {attorney.AddressCustomer.Street}, {attorney.AddressCustomer.Number}, {attorney.AddressCustomer.Neighbourhood}, {attorney.CityCustomer[0].City}, {attorney.UfCustomer}, {complement} CEP: {zipCode}, Telefone: {phone}, email: {attorney.Customer.Email}.";

            return body;
        }

        private string SetLawyerData(AttorneyEntity attorney)
        {
            string body;
            var zipCode = Convert.ToInt64(attorney.AddressLawyer.ZipCode).ToString(@"00000-000");
            var complement = attorney.AddressLawyer.Complement != " " ? attorney.AddressLawyer.Complement + ", " : "";

            body = $"{attorney.Lawyer.Name.ToUpper()}, {attorney.Lawyer.Nationality}, {attorney.Lawyer.CivilStatus}, {attorney.Lawyer.Profession}, inscrito(a) na OAB - {attorney.Lawyer.UfOab} sob nº {attorney.Lawyer.OabNumber}, email: {attorney.Lawyer.Email}, com endereço profissional na {attorney.AddressLawyer.Street}, {attorney.AddressLawyer.Number}, {complement} {attorney.AddressLawyer.Neighbourhood}, {attorney.CityLawyer[0].City}, {attorney.UfLawyer}, {attorney.AddressLawyer.Complement}, CEP: {zipCode}, onde recebe intimação.";
            
            return body;
        }
    }
}
