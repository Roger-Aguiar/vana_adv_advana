using System;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Globalization;
using Advocacy_Software.Advocacy.Software.Entities;

namespace Advocacy_Software.Advocacy.Software.Shared
{
    public class EmailSent
    {
        private readonly string userName = "rogervisualstudio@gmail.com";
        private readonly string password = "nqqtywrupjpkgpew";
        private readonly string host = "smtp.gmail.com";
        private readonly int port = 587;
        private readonly Signatures signature = new();
        private AttorneyEntity attorney = new();
        private FeesContractEntity contract;

        public HipossuficiencyEntity Hipossuficiency { get; }

        public EmailSent(AttorneyEntity attorney)
        {
            this.attorney = attorney;
        }
        
        public EmailSent(FeesContractEntity contract)
        {
            this.contract = contract;
        }

        public EmailSent(HipossuficiencyEntity hipossuficiency)
        {
            Hipossuficiency = hipossuficiency;
        }

        public EmailSent(Signatures signature)
        {
            this.signature = signature;
        }

        public void SendAttorneyByEmail()
        {
            try
            {
                using SmtpClient smtp = new();
                using MailMessage email = new();
                smtp.Host = host;
                smtp.Credentials = new NetworkCredential(attorney.Lawyer.Email, attorney.Lawyer.AppPassword);
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                email.From = new MailAddress(userName);
                email.To.Add(attorney.Customer.Email);
                email.Subject = $"Documento para assinatura - {attorney.Subject}";
                email.Body = SetBodyAttorney();
                email.IsBodyHtml = true;
                email.Attachments.Add(new Attachment(attorney.PdfPath));
                smtp.Send(email);

                MessageBox.Show("Email enviado com sucesso!", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SendFeesContractByEmail()
        {
            try
            {
                using SmtpClient smtp = new();
                using MailMessage email = new();
                smtp.Host = host;
                smtp.Credentials = new NetworkCredential(contract.Lawyer[0].Email, contract.Lawyer[0].AppPassword);
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                email.From = new MailAddress(userName);
                email.To.Add(contract.Customer[0].Email);
                email.Subject = $"Documento para assinatura - {contract.Subject}";
                email.Body = SetBody();
                email.IsBodyHtml = true;
                email.Attachments.Add(new Attachment(contract.PdfPath));
                smtp.Send(email);

                MessageBox.Show("Email enviado com sucesso!", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SendFeesHippossuficiencyContractByEmail()
        {
            try
            {
                using SmtpClient smtp = new();
                using MailMessage email = new();
                smtp.Host = host;
                smtp.Credentials = new NetworkCredential(Hipossuficiency.Lawyer[0].Email, Hipossuficiency.Lawyer[0].AppPassword);
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                email.From = new MailAddress(userName);
                email.To.Add(Hipossuficiency.Customer[0].Email);
                email.Subject = $"Documento para assinatura - {Hipossuficiency.Subject}";
                email.Body = SetBodyHippossuficiency();
                email.IsBodyHtml = true;
                email.Attachments.Add(new Attachment(Hipossuficiency.PdfFile));
                smtp.Send(email);

                MessageBox.Show("Email enviado com sucesso!", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SendVerificationCodeByEmail()
        {
            try
            {
                using SmtpClient smtp = new();
                using MailMessage email = new();
                smtp.Host = host;
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                email.From = new MailAddress(userName);
                email.To.Add(signature.Email);
                email.Subject = $"Código de verificação - Advoga Fácil";
                email.Body = SetBodyVerificationCode();
                email.IsBodyHtml = true;
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SendSignConfirmationByEmail()
        {
            try
            {
                using SmtpClient smtp = new();
                using MailMessage email = new();
                smtp.Host = host;
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                email.From = new MailAddress(userName);
                email.To.Add(signature.Email);
                email.Subject = "Confirmação de assinatura - Advoga Fácil";
                email.Body = SetBodySignConfirmation();
                email.IsBodyHtml = true;
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SendUpdateConfirmationByEmail(bool isNewUser)
        {
            try
            {
                using SmtpClient smtp = new();
                using MailMessage email = new();
                smtp.Host = host;
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                email.From = new MailAddress(userName);
                email.To.Add(signature.Email);

                email.Subject = isNewUser == false ? "Confirmação de mudança de plano - Advoga Fácil" : "Dados de usuário - Advoga Fácil";
                email.Body = SetBodySignConfirmation();
                email.IsBodyHtml = true;
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SendDeleteAccountConfirmationByEmail()
        {
            try
            {
                using SmtpClient smtp = new();
                using MailMessage email = new();
                smtp.Host = host;
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                email.From = new MailAddress(userName);
                email.To.Add(signature.Email);

                email.Subject = "Cancelamento de assinatura - Advoga Fácil";
                email.Body = SetBodyForDeleteAccount();
                email.IsBodyHtml = true;
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string SetBody() => @$"<p>Prezado(a), segue em anexo {contract.Subject} para fins de análise e assinatura.
        <p>Qualquer dúvida me coloco à disposição!</p> <p><b>Atenciosamente</b>,</p> <p><i>Dr(a). {contract.Lawyer[0].Name}.<i>";

        private string SetBodyAttorney() => @$"<p>Prezado(a), segue em anexo {attorney.Subject} para fins de análise e assinatura.
        <p>Qualquer dúvida me coloco à disposição!</p> <p><b>Atenciosamente</b>,</p> <p><i>Dr(a). {attorney.Lawyer.Name}.<i>";

        private string SetBodyHippossuficiency() => @$"<p>Prezado(a), segue em anexo {Hipossuficiency.Subject} para fins de análise e assinatura.
        <p>Qualquer dúvida me coloco à disposição!</p> <p><b>Atenciosamente</b>,</p> <p><i>Dr(a). {Hipossuficiency.Lawyer[0].Name}.<i>";


        private string SetBodyVerificationCode() => @$"<p>Prezado(a) {signature.FullName}, digite o código de verificação na janela de confirmação de 
        assinatura para confirmar seu cadastro. <h3>Código de verificação: {signature.VerificationCode} </h3> <p><b>Atenciosamente</b>,</p> 
        <p><i>Equipe de desenvolvimento Advoga Fácil<i>";

        private string SetBodySignConfirmation() => @$"<p>Prezado(a) {signature.FullName}, obrigado por contratar nossos serviços! 
        Sua assinatura foi confirmada com sucesso e agora você tem total acesso ao sistema Advoga Fácil! Segue abaixo os detalhes completos de sua 
        assinatura:</p> 
        <ul> <li>Nome completo: {signature.FullName}</li> <li>Usuário: {signature.Username}</li> <li>Email: {signature.Email}</li> 
        <li>Senha: {signature.Password}</li> 
        <li>Tipo de assinatura: {signature.SignatureType}</li> 
        <li>Valor da sua assinatura: {String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", signature.SignaturePrice)}</li> 
        <li>Data de início da assinatura: {signature.RegisterDate}</li><li>Data de vencimento da assinatura: {signature.DeadlineSignatureDate}</li> 
        <li>Numero da matrícula: {signature.GuidSignature}</li></ul> 
        <p>Recomendamos que você guarde este email, pois nele contém todos os dados de sua assinatura.</p>
        <p>Caso tenha qualquer dúvida ou necessite de algum suporte para a plataforma, basta entrar entrar em contato por 
        este email e um de nossos atendentes irá entrar em contato com você imediatamente para solucionar suas dúvidas!</p><p><b>Atenciosamente</b>,
        </p> <p><i>Equipe de desenvolvimento Advoga Fácil<i></p>";
               
        private string SetBodyForDeleteAccount() => @$"<p>Prezado(a) {signature.FullName}, lamentamos por não utilizar mais os nossos serviços! 
        Sua assinatura foi cancelada e agora você não tem mais acesso à plataforma!</p>
        <p>Caso queira voltar a usar nosso sistema, basta instalar o programa no seu computador e fazer uma nova assinatura!</p>
        <p><b>Atenciosamente</b>,
        </p> <p><i>Equipe de desenvolvimento Advoga Fácil<i></p>";
    }
}
