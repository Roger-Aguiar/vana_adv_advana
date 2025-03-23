namespace Advocacy_Software.Forms
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        
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
            this.Hide();
            InputBox form = new();
            form.Closed += (s, args) => this.Close();
            form.Show();

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
