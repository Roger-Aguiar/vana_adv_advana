using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using System.Globalization;
using Advocacy_Software.Advocacy.Software.Director.Person;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.Person;
using Microsoft.Win32;
using Advocacy_Software.Advocacy.Software.Shared;
using Advocacy_Software.Advocacy.Software.Shared.SqlCommands;
using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.BankAccountBuilder;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.Address;

namespace Advocacy_Software.Forms
{
    /// <summary>
    /// Interaction logic for WindowMenu.xaml
    /// </summary>
    public partial class WindowNewAccount : Window
    {
        private bool isValidField = true;
        private Signatures signature = new();
        private string signatureType;
        string? signatureUpdate = null;

        Director director = new();
        ConcreteSignatureBuilder builder = new();
        
        public WindowNewAccount()
        {
            InitializeComponent();
        }

        public WindowNewAccount(Signatures signature)
        {
            this.signature = signature;            
            InitializeComponent();
        }

        #region Private methods
        
        private void GoToLogin()
        {
            this.Hide();
            WindowLogin form = new();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private bool ValidateFields(string field, string control)
        {
            if (control == "")
            {
                MessageBox.Show($"Campo \"{field}\" é obrigatório!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                isValidField = false;
            }
            else
            {
                isValidField = true;
            }
            return isValidField;
        }

        private void FillFields()
        {
            signature.FullName = ValidateFields("Nome completo", TextBoxFullName.Text) == false ? null : TextBoxFullName.Text;
            signature.Username = ValidateFields("Usuário", TextBoxUserName.Text) == false ? null : TextBoxUserName.Text;
            signature.Email = ValidateFields("Email", TextBoxEmail.Text) == false ? null : TextBoxEmail.Text;
            signature.Password = ValidateFields("Senha", TextBoxPassword.Text) == false ? null : TextBoxPassword.Text;
            signature.Genre = ValidateFields("Gênero", TextBoxGenre.Text) == false ? null : TextBoxGenre.Text;
            signature.SignatureType = ValidateFields("Tipo de assinatura", TextBoxSignatureType.Text) == false ? null : TextBoxSignatureType.Text;
            signature.RegisterDate = TextBoxRegisterDate.Text;            
            signature.GuidSignature = TextBoxGuidSignature.Text;
        }

        private void FillComboBoxSignatureType()
        {
            ComboBoxSignatureType.Items.Add("Anual");
            ComboBoxSignatureType.Items.Add("Mensal");
            ComboBoxSignatureType.Items.Add("Semestral");
        }

        private void FillComboBoxGenre()
        {
            ComboBoxGenre.Items.Add("Feminino");
            ComboBoxGenre.Items.Add("Masculino");
        }

        private void ReadTable()
        {
            if (this.signature.Email != null)
            {
                signatureType = this.signature.SignatureType;

                TextBoxFullName.Text = this.signature.FullName;
                TextBoxUserName.Text = this.signature.Username;
                TextBoxEmail.Text = this.signature.Email;
                TextBoxPassword.Text = this.signature.Password;
                TextBoxGenre.Text = this.signature.Genre;
                TextBoxSignatureType.Text = this.signature.SignatureType;
                TextBoxSignaturePrice.Text = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", signature.SignaturePrice);
                TextBoxRegisterDate.Text = this.signature.RegisterDate;
                TextBoxDeadline.Text = this.signature.DeadlineSignatureDate;
                TextBoxGuidSignature.Text = this.signature.GuidSignature;

                TextBoxEmail.IsEnabled = false;
                TextBoxFullName.IsEnabled = false;
                TextBoxRegisterDate.IsEnabled = false;
                TextBoxDeadline.IsEnabled = false;
                TextBoxGuidSignature.IsEnabled = false;
                TextBoxGenre.IsEnabled = false;                
            }
            else
            {
                TextBoxRegisterDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                TextBoxGuidSignature.Text = Guid.NewGuid().ToString();
            }
            signatureUpdate = TextBoxSignatureType.Text;
            FillComboBoxSignatureType();
            FillComboBoxGenre();
        }

        private void FillFullComboBoxSignatureType()
        {
            switch (ComboBoxSignatureType.SelectedItem.ToString())
            {
                case "Anual":
                    signature.DeadlineSignatureDate = (DateTime.ParseExact(TextBoxRegisterDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1)).ToString("dd/MM/yyyy");
                    break;
                case "Mensal":
                    signature.DeadlineSignatureDate = (DateTime.ParseExact(TextBoxRegisterDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddMonths(1)).ToString("dd/MM/yyyy");
                    break;
                case "Semestral":
                    signature.DeadlineSignatureDate = (DateTime.ParseExact(TextBoxRegisterDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddMonths(6)).ToString("dd/MM/yyyy");
                    break;
                default:
                    break;
            }

            signature.SignaturePrice = 100;
            TextBoxSignaturePrice.Text = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", signature.SignaturePrice);
            TextBoxSignatureType.Text = ComboBoxSignatureType.SelectedItem.ToString();
            TextBoxDeadline.Text = signature.DeadlineSignatureDate;
        }

        private void UpdateComboBoxSignatureType()
        {
            switch (ComboBoxSignatureType.SelectedItem.ToString())
            {
                case "Anual":
                    TextBoxRegisterDate.Text = DateTime.UtcNow.ToString("dd/MM/yyyy");
                    TextBoxDeadline.Text = signature.DeadlineSignatureDate = (DateTime.ParseExact(TextBoxRegisterDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1)).ToString("dd/MM/yyyy");
                    signature.SignaturePrice = 1500;
                    break;
                case "Mensal":
                    TextBoxRegisterDate.Text = DateTime.UtcNow.ToString("dd/MM/yyyy");
                    TextBoxDeadline.Text = signature.DeadlineSignatureDate = (DateTime.ParseExact(TextBoxRegisterDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddMonths(1)).ToString("dd/MM/yyyy");
                    signature.SignaturePrice = 100;
                    break;
                case "Semestral":
                    TextBoxRegisterDate.Text = DateTime.UtcNow.ToString("dd/MM/yyyy");
                    TextBoxDeadline.Text = signature.DeadlineSignatureDate = (DateTime.ParseExact(TextBoxRegisterDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddMonths(6)).ToString("dd/MM/yyyy");
                    signature.SignaturePrice = 900;
                    break;
                default:
                    break;
            }            
            TextBoxSignaturePrice.Text = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", signature.SignaturePrice);
            TextBoxSignatureType.Text = ComboBoxSignatureType.SelectedItem.ToString();
        }

        private void Create()
        {
            director.Builder = builder;
            if (isValidField == true)
            {
                director.Read(SignatureSqlCommands.SelectByEmail(signature.Email));

                if (builder.User.Email == TextBoxEmail.Text)
                {
                    MessageBox.Show("Já existe uma conta associada a este email! Favor tentar com um novo email!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {                    
                    director.Create(signature);
                    MessageBox.Show("Assinatura confirmada! Agora você tem acesso completo ao sistema! Acabamos de enviar um email para você com os dados completos de sua assinatura!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);                  
                }
            }
        }

        private void Update()
        {
            director.Builder = builder;               
            if (signatureUpdate != TextBoxSignatureType.Text)
            {                    
                MessageBox.Show($"Alteração de plano realizada com sucesso! Enviamos um email para {signature.Email} com todos os detalhes de sua assinatura!", "Dados alterados", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Dados alterados com sucesso!", "Dados alterados", MessageBoxButton.OK, MessageBoxImage.Information);
                director.Update(signature);
            }  
        }

        private void DeleteCustomers()
        {
            Director directorCustomer = new();
            ConcreteCustomerBuilder builder = new();
            directorCustomer.Builder = builder;
            directorCustomer.Delete(CustomerSqlCommands.Delete(signature.IdSignature));
        }

        private void DeleteLawyer()
        {
            Director directorLawyer = new();
            ConcreteLawyerBuilder builder = new();
            directorLawyer.Builder = builder;
            directorLawyer.Delete(LawyerSqlCommands.Delete(signature.IdSignature));
        }

        private void DeleteBankAccount()
        {
            Director bankAccountDirector = new();
            ConcreteBankAccountBuilder builder = new();
            bankAccountDirector.Builder = builder;
            bankAccountDirector.Delete(BankAccountSqlCommands.Delete(signature.IdSignature));
        }

        private void DeleteAddress()
        {
            Director directorAddress = new();
            ConcreteAddressBuilder builder = new();
            directorAddress.Builder = builder;
            directorAddress.Delete(AddressSqlCommands.Delete(signature.IdSignature));
        }

        #endregion

        private void ListBoxItemExit_Selected(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListBoxItemRegisterAccount_Selected_1(object sender, RoutedEventArgs e)
        {
            FillFields();

            if (signatureType == null)
            {
                Create();                                
            }
            else
            {
                Update();                                
            }
            GoToLogin();
        }

        private void ComboBoxSignatureType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBoxSignatureType.Text = ComboBoxSignatureType.SelectedItem.ToString();
            if(signature.Update == false)
            {
                FillFullComboBoxSignatureType();
            }
            else
            {
                UpdateComboBoxSignatureType();
            }
        }

        private void ComboBoxGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBoxGenre.Text = ComboBoxGenre.SelectedItem.ToString();
        }

        private void ListBoxItemAddProfileImage_Selected(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new() { Title = "Selecionar imagem de perfil" };
            openFileDialog.ShowDialog();
            signature.ImageProfile = openFileDialog.FileName == "" ? null : openFileDialog.FileName;
            MessageBox.Show("Sua imagem foi adicionada com sucesso e será exibida como foto de perfil no menu principal!", "Imagem de perfil", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ListBoxItemAddHeaderImage_Selected(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new() { Title = "Selecionar logo para cabeçalho" };
            openFileDialog.ShowDialog();
            signature.LogoHeader = openFileDialog.FileName == "" ? null : openFileDialog.FileName;
            MessageBox.Show("Sua imagem foi adicionada com sucesso e será adicionada nos cabeçalhos de todos os contratos que você criar!", "Cabeçalho", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ListBoxItemAddFooterImage_Selected(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new() { Title = "Selecionar logo para rodapé" };
            openFileDialog.ShowDialog();
            signature.LogoFooter = openFileDialog.FileName == "" ? null : openFileDialog.FileName;
            MessageBox.Show("Sua imagem foi adicionada com sucesso e será adicionada nos rodapés de todos os contratos que você criar!", "Rodapé", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ListBoxItemDeleteAccount_Selected(object sender, RoutedEventArgs e)
        {
            director.Builder = builder;

            var question = MessageBox.Show("Tem certeza que deseja excluir sua conta? Caso continue, sua assinatura será automaticamente cancelada e você não terá mais acesso ao sistema.", "Excluir conta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                DeleteCustomers();
                DeleteLawyer();
                DeleteBankAccount();
                DeleteAddress();
                director.Delete(SignatureSqlCommands.DeleteUser(signature.IdSignature));
                MessageBox.Show("Sua assinatura foi cancelada com sucesso!", "Cancelamento", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void WindowNewAccount1_Loaded(object sender, RoutedEventArgs e)
        {
            ReadTable();
        }
    }
}
