using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Pages;
using Advocacy_Software.Pages;
using System.Windows;

namespace Advocacy_Software.Forms
{
    /// <summary>
    /// Interaction logic for WindowMenu.xaml
    /// </summary>
    public partial class WindowMenu : Window
    {
        private readonly Signatures signature = new();

        public WindowMenu(Signatures signature)
        {
            this.signature = signature;
            InitializeComponent();
        }

        private void ListBoxItemExit_Selected(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListBoxItemGenerateAttorney_Selected(object sender, RoutedEventArgs e)
        {
            PageGenerateAttorney page = new(signature);
            FrameForms.Content = page;
        }

        private void ListBoxItemRegisterLawyer_Selected(object sender, RoutedEventArgs e)
        {
            PageRegisterLawyer page = new(signature);
            FrameForms.Content = page;
        }
                
        private void ListBoxItemRegisterCustomer_Selected(object sender, RoutedEventArgs e)
        {
            PageRegisterCustomer customer = new(signature);
            FrameForms.Content = customer;
        }

        private void ListBoxItemRegisterAccountBank_Selected(object sender, RoutedEventArgs e)
        {
            PageRegisterBankAccount bank = new(signature);
            FrameForms.Content = bank;
        }

        private void ListBoxItemGenerateHipposuficiencyContract_Selected(object sender, RoutedEventArgs e)
        {
            PageGenerateHipossuficiencyContract page = new(signature);
            FrameForms.Content = page;
        }

        private void ListBoxItemGenerateFeesContract_Selected(object sender, RoutedEventArgs e)
        {
            PageGenerateFeesContract page = new(signature);
            FrameForms.Content = page;
        }

        private void ListBoxItemAddUser_Selected(object sender, RoutedEventArgs e)
        {
            PageRegisterNewUser page = new();
            FrameForms.Content = page;
        }

        private void ListBoxItemWindowNewAccount_Selected(object sender, RoutedEventArgs e)
        {
            signature.Update = true;
            this.Hide();
            WindowNewAccount form = new(signature);
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void WindowMainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            PageImageLogo page = new(signature);
            FrameForms.Content = page;
            this.Title = this.Title + " - " + signature.Username;
        }

        private void ListBoxMainMenu_Selected(object sender, RoutedEventArgs e)
        {
            PageImageLogo page = new(signature);
            FrameForms.Content = page;
        }
    }
}
