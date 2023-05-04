using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Globalization;
using iText.Layout;
using iText.Kernel.Events;
using iText.IO.Image;
using Advocacy_Software.Advocacy.Software.Entities;

namespace Advocacy_Software.Advocacy.Software.Shared.Utils
{
    public class HipossuficiencyContract
    {
        public void GenerateContract(HipossuficiencyEntity contract)
        {
            using (PdfWriter pdfWriter = new(contract.PdfFile, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)))
            {
                var format = new DocumentFormat();
                var pdfDocument = new PdfDocument(pdfWriter);
                var document = new Document(pdfDocument, PageSize.A4);

                if (contract.Signature.LogoHeader != null || contract.Signature.LogoFooter != null)
                {
                    var documentHeader = new Document(pdfDocument, PageSize.A4);
                    var documentFooter = new Document(pdfDocument, PageSize.A4);

                    Image? imageHeader = contract.Signature.LogoHeader != null ? new Image(ImageDataFactory.Create(contract.Signature.LogoHeader)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER) : null;
                    Image? imageFooter = contract.Signature.LogoFooter != null ? new Image(ImageDataFactory.Create(contract.Signature.LogoFooter)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER) : null;
                    documentHeader.SetMargins(0, 0, 0, 0);
                    document.SetMargins(130, 50, 130, 50);
                    documentFooter.SetMargins(700, 0, 0, 0);
                    documentHeader.Add(imageHeader);
                    documentFooter.Add(imageFooter);
                }
                else
                {
                    document.SetMargins(85, 50, 50, 50);
                    pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new EndPageEventHandler(document, pdfDocument, contract.Lawyer[0]));
                }

                document.Add(format.SetTitle("\n\nDECLARAÇÃO DE HIPOSSUFICIÊNCIA DE RENDA"));

                document.Add(format.SetBodyAsJustified(SetCustomerData(contract)));

                document.Add(new Paragraph("\n\n\n\n\n"));

                document.Add(format.SetBody($"{contract.CityLawyer[0].City}, {DateTime.Now.ToString("D", new CultureInfo("pt-BR"))}"));

                document.Add(format.SetTitle("\n\n\n\n\n___________________________________________________________"));

                document.Add(format.SetTitle("(Assinatura)"));

                document.Add(format.SetBody("Declarante"));

                document.Close();
                pdfDocument.Close();
            };
        }

        private string SetCustomerData(HipossuficiencyEntity contract)
        {
            string CpfOrCnpj = contract.Customer[0].CpfOrCnpj.Length == 11 ? "CPF: " + Convert.ToInt64(contract.Customer[0].CpfOrCnpj).ToString(@"000\.000\.000-00") : "CNPJ" + Convert.ToInt64(contract.Customer[0].CpfOrCnpj).ToString(@"00\.000\.000/0000-00");
            string phone = contract.Customer[0].Phone.Length == 11 ? Convert.ToInt64(contract.Customer[0].Phone).ToString(@"(00)00000-0000") : Convert.ToInt64(contract.Customer[0].Phone).ToString(@"(00)0000-0000");
            string zipCode = Convert.ToInt64(contract.AddressCustomer.ZipCode).ToString(@"00000-000");
            string complement = contract.AddressCustomer.Complement != " " ? contract.AddressCustomer.Complement + "," : "";
            string body = $"{contract.Customer[0].Name.ToUpper()}, {contract.Customer[0].Nationality}, {contract.Customer[0].CivilStatus}, {contract.Customer[0].Profession}, portador(a) do RG: {contract.Customer[0].IdentityCustomer}, {CpfOrCnpj}, {contract.AddressCustomer.Street}, {contract.AddressCustomer.Number}, {contract.AddressCustomer.Neighbourhood}, {contract.City[0].City}-{contract.UfCustomer}, {complement} CEP: {zipCode}, Telefone: {phone}, email: {contract.Customer[0].Email}, declara, com a finalidade de obter a gratuidade da Justiça (Lei n.º 1.060/50) que não possui condições financeiras e econômicas para arcar com as custas processuais e honorários advocatícios sem prejuízo do sustento próprio ou de sua família, nos termos do artigo 5º, LXXIV da Constituição Federal e pela Lei 13.105/2015, c/c o art. 98 e seguintes, do CPC/2015.\r\n";

            return body;
        }
    }
}
