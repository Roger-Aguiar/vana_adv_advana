namespace Advocacy_Software.Forms
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private Signatures signature = new();
        
        public WindowLogin()
        {
            WindowSplashScreen splashScreen = new();
            splashScreen.Show();
            System.Threading.Thread.Sleep(5000);
            splashScreen.Close();
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var resetPassword = Interaction.InputBox("Digite o email que você usa para acessar o sistema:", 
                "Recuperação de senha", "", 
                Convert.ToInt32(this.Left + (this.Width / 2) - 200), 
                Convert.ToInt32(this.Top + (this.Height / 2) - 100));
            if(resetPassword != "")
            {
                signature.Password = Guid.NewGuid().ToString().Substring(0, 12);
                MessageBox.Show($"Enviamos uma nova senha com 12 caracteres para você para o email " +
                    $"\"{resetPassword}\" para que você possa recuperar seu acesso, a qual será usada a " +
                    $"partir de agora para você acessar o sistema! Caso não consiga visualizar seu email na " +
                    $"caixa de entrada, confira sua caixa de spam!", "Recuperação de acesso", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Você não digitou seu email, favor tentar novamente!", "Recuperação de acesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void HyperlinkNewUser_Click(object sender, RoutedEventArgs e)
        {           
            this.Hide();
            WindowNewAccount form = new();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void ButtonAccess_Click(object sender, RoutedEventArgs e)
        {
            
            Director director = new();
            ConcreteSignatureBuilder builder = new();
            director.Builder = builder;
            director.Read(SignatureSqlCommands.SelectByEmail(TextBoxUserName.Text));

            if (builder.User.Email == TextBoxUserName.Text && builder.User.Password == UserPassword.Password)
            {
                builder.User.Update = true;
                this.Hide();
                WindowMenu menu = new(builder.User);
                menu.Closed += (s, args) => this.Close();
                menu.Show();                
            }
            else
            {
                MessageBox.Show("Usuário e/ou senha inválidos!\nEsse erro pode ter ocorrido por você ter digitado os dados incorretamente ou porque você ainda não tem cadastro no sistema.", "Credenciais inválidas", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WindowLogin1_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxUserName.Focus();
        }
    }
}
