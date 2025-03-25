#nullable disable

namespace Advocacy_Software.Advocacy.Software.Forms
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        public InputBox()
        {
            InitializeComponent();
        }

        #region Private methods
        
        private async void ReadUser()
        {
            Director director = new();
            ConcreteSignatureBuilder builder = new();
            director.Builder = builder;
            director.Read(SignatureSqlCommands.SelectByEmail(TextEmail.Text));

            if (builder.User.Email == TextEmail.Text)
            {
                EmailService emailService = new();
                bool success = await emailService.SendRecoveryEmailAsync(builder.User.FullName, builder.User.Email, builder.User.Password);
                if (success)
                {
                    MessageBox.Show($"Um e-mail com seus dados de acesso foi enviado para {TextEmail.Text}, caso este email tenha sido o email cadastrado no momento que você criou sua assinatura, você receberá com sucesso a sua senha! Verifique sua caixa de spam caso não tenha recebido na caixa de entrada!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Erro ao enviar e-mail", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Email não encontrado", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            ReadUser();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
