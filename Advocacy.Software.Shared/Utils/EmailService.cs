public class EmailService
{
    private const string SmtpServer = "smtp.gmail.com";
    private const int SmtpPort = 587;
    private const string SubjectEmail = "rogervisualstudio@gmail.com";
    private const string Senha = "jdzz jicj ixkn bzyg";

    public async Task<bool> SendRecoveryEmailAsync(string name, string destinationEmail, string password)
    {
        try
        {
            using SmtpClient smtpClient = new(SmtpServer, SmtpPort);
            smtpClient.Credentials = new NetworkCredential(SubjectEmail, Senha);
            smtpClient.EnableSsl = true;

            MailMessage mail = new()
            {
                From = new MailAddress(SubjectEmail, "Suporte AdvFlow"),
                Subject = "Recuperação de Senha",
                Body = $"Olá {name}!<br>Você solicitou a recuperação da sua senha. " +
                $"Segue a sua senha que foi cadastrada no momento da assinatura:<br><br>Senha: {password}" +
                $"<br><br>Se não foi você, ignore este e-mail.<br><br>Atenciosamente, <br>Equipe de suporte AdvFlow!",
                IsBodyHtml = true
            };

            mail.To.Add(destinationEmail);

            await smtpClient.SendMailAsync(mail);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao enviar email: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            return false;
        }
    }
}
