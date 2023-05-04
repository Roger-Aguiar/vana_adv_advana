using Advocacy_Software.Advocacy.Software.Entities;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Globalization;
using iText.Layout;
using iText.Kernel.Events;
using iText.IO.Image;

namespace Advocacy_Software.Advocacy.Software.Shared.Utils
{
    public static class FeesContractGenerator
    {        
        public static void GenerateContract(FeesContractEntity contract)
        {
            using (PdfWriter pdfWriter = new(contract.PdfPath, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)))
            {
                DocumentFormat format = new();
                PdfDocument pdfDocument = new(pdfWriter);
                Document document = new(pdfDocument, PageSize.A4);

                if (contract.Signature.LogoHeader != null || contract.Signature.LogoFooter != null)
                {
                    Document documentHeader = new(pdfDocument, PageSize.A4);
                    Document documentFooter = new(pdfDocument, PageSize.A4);

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

                document.Add(format.SetTitle("CONTRATO DE HONORÁRIOS ADVOCATÍCIOS"));
                
                document.Add(format.SetBodyAsJustified($"Pelo presente instrumento particular de contrato de prestação de serviços advocatícios e na melhor forma de direito, {SetCustomerBody(contract)}, "));

                document.Add(format.SetBodyAsJustified($"Convenciona e contrata com Dr(a). {SetLawyerBody(contract)} o que mutuamente aceitam e outorgam, mediante as cláusulas e condições seguintes:"));

                document.Add(format.SetTitle("DO OBJETO DO CONTRATO"));

                document.Add(format.SetBodyAsJustified("CLAÚSULA I – O presente instrumento tem como objetivo a prestação de serviços advocatícios a serem realizados em primeira instância e em fase de recurso, se conveniente for e mediante contrato aditivo."));

                document.Add(format.SetBodyAsJustified($"1.1 A contratada obriga-se, face ao mandato judicial que lhe foi outorgado, a prestar seus serviços profissionais norteando-se pelo Código de Ética e Disciplina da profissão em defesa dos direitos do contratante em ação denominada {contract.ActionName?.ToUpper()}, se comprometendo a despachar com juiz quando julgar necessário, acompanhar o contratante em audiência e todos os atos necessários para o bom andamento da causa."));

                document.Add(format.SetBodyAsJustified("1.2 A partir da data da assinatura deste contrato tem a advogada 30 (trinta) dias úteis para distribuir a ação, exceto se outro prazo for convencionado com o contratante, o qual deverá constar no corpo deste contrato.\r\n"));

                document.Add(format.SetTitle("DAS OBRIGAÇÕES"));

                document.Add(format.SetBodyAsJustified("CLÁUSULA II – Fica responsabilizado o contratante a contribuir para a melhor execução dos atos processuais, se comprometendo a disponibilizar à advogada todos e quaisquer documentos e/ou informações necessárias, tanto ao ajuizamento da ação, quanto aos impulsionamentos necessários no decorrer desta."));

                document.Add(format.SetBodyAsJustified("2.1 A advogada poderá solicitar ao contratante os documentos mencionados na cláusula anterior por meio telefônico, por e-mail (o qual deverá ser previamente informado pelo cliente. Ficando, ainda, sob sua responsabilidade a verificação de sua caixa eletrônica), ou por whatsapp - devendo o cliente fornecer a documentação ou informações solicitadas em um prazo de até 48h levando-se em consideração o bom andamento da causa, exceto se um prazo menor for convencionado."));

                document.SetMargins(70, 50, 130, 50);

                document.Add(format.SetBodyAsJustified("2.2 O contratante poderá esclarecer todas as suas dúvidas referentes ao presente contrato, sobre a ação mencionada no item 1.1 da Cláusula I pessoalmente (mediante agendamento de horário) ou, por meio eletrônico (por meio de e-mail – tendo a advogada um prazo de até 48h para resposta). Não serão prestadas quaisquer informações e/ou esclarecimentos por meio de whatsapp."));

                document.Add(format.SetBodyAsJustified("2.3 Caso o cliente tenha qualquer dúvida referente a outras ações as quais tenha interesse ingressar, deverá agendar horário e comparecer ao escritório pagando a quantia referente a consultoria, exceto se de outra forma for convencionado."));

                document.Add(format.SetBodyAsJustified("2.4 Caso o cliente tenha qualquer dúvida referente a processo o qual já tenha outro advogado constituído, esta subscritora NÃO prestará qualquer informação referente ao advogado ou, se os atos que o patrono está tomando são os corretos. Deverá o cliente, entrar em contato com seu patrono e solicitar as informações necessárias. Exceto, se este tiver sido desconstituído anteriormente."));

                document.Add(format.SetBodyAsJustified("2.5 É de inteira responsabilidade do contratante a veracidade e procedência das informações e documentos prestados à advogada no que pertine à demanda."));

                document.Add(format.SetBodyAsJustified("CLÁUSULA III – Havendo necessidade de contratação de outros profissionais, no decurso do processo, a advogada elaborará substabelecimento com reserva de poderes à pessoa/entidade de seu conhecimento e deste evento será notificado o contratante por meio telefônico, e-mail e/ou por AR."));

                document.Add(format.SetBodyAsJustified("CLÁUSULA IV – Ao contratante, caberá o pagamento das despesas efetuadas pela advogada direta ou indiretamente ligadas ao processo, incluindo-se todas as cópias, emolumentos, viagens, custas processuais entre outros que forem necessárias ao bom andamento da ação. Despesas estas que serão apresentadas ao contratante como prestação de contas acompanhadas de recibo em apartado dos de honorários advocatícios."));

                if (contract.PaymentType == "À vista")
                {
                    document.Add(format.SetBodyAsJustified($"CLÁUSULA V – Pelos serviços prestados e especificados neste contrato, o(a) advogado(a) receberá a título de honorários, o valor de {String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", contract.TotalServiceValue)} que serão pagos na assinatura deste."));
                }
                else
                {
                    document.Add(format.SetBodyAsJustified($"CLÁUSULA V - Pelos serviços prestados e especificados neste contrato, o(a) advogado(a) receberá a título de honorários, o valor de {String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", contract.TotalServiceValue)}, devendo estes serem pagos da seguinte forma:"));
                    document.Add(format.SetBodyAsJustified($"a) Entrada no valor {String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", contract.InitialValue)}"));
                    document.Add(format.SetBodyAsJustified($"b) {contract.InstallmentsNumber} parcelas de {String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", ((contract.TotalServiceValue - contract.InitialValue) / contract.InstallmentsNumber))}, a serem {SetBankAccountData(contract)}"));
                }

                document.Add(format.SetBodyAsJustified($"Além disso, o(a) contratado(a) receberá, no êxito, a título de honorários, {contract.SuccessFees}% ({contract.SuccessFees} por cento) sobre o proveito final da ação."));

                document.Add(format.SetBodyAsJustified("5.1 Sendo a atividade do(a) advogado(a) atividade meio e não fim fica estabelecido que os honorários avençados a título de entrada nesta cláusula são sempre devidos, independente do resultado da ação."));

                document.Add(format.SetBodyAsJustified("5.2 Agindo o contratante de forma dolosa ou culposa em face da advogada, restará facultado a esta, retirar-se da causa e se exonerar de todas as obrigações, cabendo a execução total dos honorários neste pactuados."));

                document.Add(format.SetBodyAsJustifiedInline("5.3 Fica estabelecido que iniciados os serviços especificados na CLAÚSULA I, §1º (mediante prestação de informações, vista de petição, protocolo de distribuição de peça ou, de qualquer outra forma admitida em direito), são devidos os honorários contratados e sucumbências por completo, ainda que em caso de desistência por parte do contratante ou se for cassado o mandado da advogada sem sua culpa, podendo esta exigir os honorários de imediato. "));

                document.Add(format.SetBodyAsJustified("5.4 Caso haja morte ou incapacidade civil da contratada, seus sucessores ou representantes legais receberão os honorários e sucumbências na proporção do trabalho realizado."));

                document.Add(format.SetBodyAsJustified("5.5 Havendo acordo entre o contratante e a parte contrária, não prejudicará o recebimento dos honorários contratados e, eventualmente, os de sucumbência. Caso em que os horários iniciais e finais serão pagos à advogada."));

                document.Add(format.SetTitle("DA COBRANÇA E DA EXECUÇÃO"));

                document.Add(format.SetBodyAsJustified("CLÁUSULA VI – Fica estabelecido que em caso de serviços de cobrança ou de execução ou ainda de qualquer outra natureza, em que a advogada receba verba ou importância em nome do contratante, este desde já, autoriza àquela a descontar os honorários advocatícios da verba ou importância recebida, ficando obrigada a advogada a reembolsar o contratante no valor correspondente ao saldo remanescente."));

                document.Add(format.SetBodyAsJustified("6.1 Em caso de inadimplência do contratante, por mais de 30 (trinta) dias, fica facultado à contratada, o direito de realizar a cobrança dos honorários, por todos os meios admitidos em direito. Mencione-se que os valores deverão ser atualizados monetariamente e calculados com juros mensais de 1% a partir do vencimento."));

                document.Add(format.SetTitle("DO FORO"));

                document.Add(format.SetBodyAsJustified($"CLÁUSULA VII – As partes elegem o foro de {contract.City.ToUpper()} - {contract.Uf.ToUpper()} para dirimir quaisquer pendências relativas ao presente contrato, podendo ainda a contratada em caso de execução do contrato, optar pelo foro de domicílio do contratante."));

                document.Add(format.SetBodyAsJustified("E para firmeza e como prova de assim haverem contratado, fizeram este instrumento particular em duas vias impressas de igual teor e forma, assinado pelas partes contratantes e entregues suas respectivas."));

                document.Add(new Paragraph("\n"));

                document.Add(format.SetBody($"{contract.CityLawyer[0].City}, {DateTime.Now.ToString("D", (new CultureInfo("pt-BR")))}"));

                document.Add(format.SetTitle("\n\n_________________________________________________________"));

                document.Add(format.SetBody($"{contract.Lawyer[0].Name}\nOAB/{contract.Lawyer[0].UfOab} nº {contract.Lawyer[0].OabNumber}"));

                document.Add(format.SetTitle("\n\n_________________________________________________________"));

                var CpfOrCnpj = contract.Customer[0].CpfOrCnpj?.Length == 11 ? "CPF: " + Convert.ToInt64(contract.Customer[0].CpfOrCnpj).ToString(@"000\.000\.000-00") : "CNPJ" + Convert.ToInt64(contract.Customer[0].CpfOrCnpj).ToString(@"00\.000\.000/0000-00");

                document.Add(format.SetBody($"{contract.Customer[0].Name?.ToUpper()}\nRG: {contract.Customer[0].IdentityCustomer}\n{CpfOrCnpj}"));

                document.Close();
                pdfDocument.Close();
            };
        }

        private static string SetCustomerBody(FeesContractEntity contract)
        {
            string CpfOrCnpj = contract.Customer[0].CpfOrCnpj.Length == 11 ? "CPF: " + Convert.ToInt64(contract.Customer[0].CpfOrCnpj).ToString(@"000\.000\.000-00") : "CNPF" + Convert.ToInt64(contract.Customer[0].CpfOrCnpj).ToString(@"00\.000\.000/0000-00");
            string phone = contract.Customer[0].Phone.Length == 11 ? Convert.ToInt64(contract.Customer[0].Phone).ToString(@"(00)00000-0000") : Convert.ToInt64(contract.Customer[0].Phone).ToString(@"(00)0000-0000");
            string zipCode = Convert.ToInt64(contract.AddressCustomer.ZipCode).ToString(@"00000-000");
            string complement = contract.AddressCustomer.Complement != " " ? contract.AddressCustomer.Complement + "," : "";
            return $"{contract.Customer[0].Name.ToUpper()}, {contract.Customer[0].Nationality}, {contract.Customer[0].CivilStatus}, {contract.Customer[0].Profession}, portador(a) do RG: {contract.Customer[0].IdentityCustomer}, {CpfOrCnpj}, residente na {contract.AddressCustomer.Street}, {contract.AddressCustomer.Number}, {contract.AddressCustomer.Neighbourhood}, {contract.CityCustomer[0].City} - {contract.UfCustomer}, {complement} CEP: {zipCode}, Telefone: {phone}, email: {contract.Customer[0].Email}";
        }

        private static string SetLawyerBody(FeesContractEntity contract)
        { 
            string phone = contract.Lawyer[0].Phone?.Length == 11 ? Convert.ToInt64(contract.Lawyer[0].Phone).ToString(@"(00)00000-0000") : Convert.ToInt64(contract.Lawyer[0].Phone).ToString(@"(00)0000-0000");
            string zipCode = Convert.ToInt64(contract.AddressLawyer.ZipCode).ToString(@"00000-000");

            return $"{contract.Lawyer[0].Name.ToUpper()}, {contract.Lawyer[0].Nationality}, {contract.Lawyer[0].CivilStatus}, {contract.Lawyer[0].Profession}, inscrito(a) na Ordem dos Advogados do Brasil – SEÇÃO - {contract.Lawyer[0].UfOab} sob nº {contract.Lawyer[0].OabNumber}, email: {contract.Lawyer[0].Email}, com endereço profissional na {contract.AddressLawyer.Street}, {contract.AddressLawyer.Number}, {contract.AddressLawyer.Complement}, {contract.AddressLawyer.Neighbourhood}, {contract.CityLawyer[0].City}, {contract.UfLawyer}, CEP: {zipCode}, Telefone: {phone}, email: {contract.Lawyer[0].Email}, ";  
        }
                
        private static string SetBankAccountData(FeesContractEntity contract)
        {
            string paymentDetails = "";

            switch (contract.PaymentType)
            {
                case "Depósito bancário":
                    paymentDetails = $@" depositados no 5º dia útil de todo mês na {contract.BankAccount[0].AccountType?.ToLower()} {contract.BankAccount[0].AccountNumber}, agência {contract.BankAccount[0].AgencyNumber}, {contract.BankAccount[0].BankName}";
                    break;
                case "Pix":
                    if (contract.BankAccount[0].PixType == "CPF")
                    {
                        paymentDetails = $" pagos no 5º dia útil de todo mês via PIX: {Convert.ToInt64(contract.BankAccount[0].Pix).ToString(@"000\.000\.000-00")} - Tipo de chave: {contract.BankAccount[0].PixType}";
                    }
                    else if (contract.BankAccount[0].PixType == "CNPJ")
                    {
                        paymentDetails = $" pagos no 5º dia útil de todo mês via PIX: {Convert.ToInt64(contract.BankAccount[0].Pix).ToString(@"00\.000\.000/0000-00")} - Tipo de chave: {contract.BankAccount[0].PixType}";
                    }
                    else if (contract.BankAccount[0].PixType == "Telefone")
                    {
                        paymentDetails = contract.BankAccount[0].Pix?.Length == 11 ? $" pagos no 5º dia útil de todo mês via PIX: {Convert.ToInt64(contract.BankAccount[0].Pix):(00)00000-0000} - Tipo de chave: {contract.BankAccount[0].PixType}" : $" pagos no 5º dia útil de todo mês via PIX: {Convert.ToInt64(contract.BankAccount[0].Pix):(00)0000-0000} - Tipo de chave: {contract.BankAccount[0].PixType}";
                    }
                    break;
                default:
                    paymentDetails = " pagos no 5º dia útil de todo mês via boleto bancário, que será enviado por email ou WhatsApp.";
                    break;
            }

            return paymentDetails;
        }
    }
}
