using Advocacy_Software.Advocacy.Software.Concrete.Builders.BankAccountBuilder;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.Person;
using Advocacy_Software.Advocacy.Software.Director.Person;
using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared.SqlCommands;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Advocacy_Software.Pages
{
    /// <summary>
    /// Interaction logic for PageRegisterLawyer.xaml
    /// </summary>
    public partial class PageRegisterBankAccount : Page
    {        
        private Director directorLawyer = new();
        private Director directorBankAccount = new();

        private ConcreteBankAccountBuilder bankAccountBuilder = new();        
        private ConcreteLawyerBuilder lawyerBuilder = new();

        private List<Lawyer> lawyers = new();
        private List<BankAccount> bankAccountList = new();
        private BankAccount bank = new();
        private List<bool> validFields = new();
        private bool update = true;
        private int index = 0;
        public Signatures Signature { get; set; }

        public PageRegisterBankAccount(Signatures signature)
        {
            Signature = signature;
            InitializeComponent();
        }

        private string FormatPix()
        {
            string pixFormat;
            switch (bankAccountList[index].PixType)
            {
                case "Número de telefone celular":
                    pixFormat = bankAccountList[index].Pix?.Length == 11 ? Convert.ToInt64(bankAccountList[index].Pix).ToString(@"(00)00000-0000") : Convert.ToInt64(bankAccountList[index].Pix).ToString(@"(00)0000-0000");
                    break;
                case "CPF":
                    pixFormat = Convert.ToInt64(bankAccountList[index].Pix).ToString(@"000\.000\.000-00");
                    break;
                case "CNPJ":
                    pixFormat = Convert.ToInt64(bankAccountList[index].Pix).ToString(@"00\.000\.000/0000-00");
                    break;
                default:
                    pixFormat = bankAccountList[index].Pix;
                    break;
            }
            return pixFormat;
        }

        #region Private methods

        private void FillTextBoxUserAccount()
        {
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Read(LawyerSqlCommands.Read(Signature.IdSignature));
            lawyers = lawyerBuilder.Lawyers; 
            if(lawyers.Count > 0)
                TextBoxUserAccount.Text = lawyers[0].Name;
        }

        private void FillComboBoxAccountType()
        {
            ComboBoxAccountType.Items.Add("Conta corrente");
            ComboBoxAccountType.Items.Add("Conta poupança");
        }

        private void FillComboBoxBankName()
        {
            ComboBoxBankName.Items.Add("001 – Banco do Brasil S.A.");
            ComboBoxBankName.Items.Add("033 – Banco Santander (Brasil) S.A.");
            ComboBoxBankName.Items.Add("104 – Caixa Econômica Federal");
            ComboBoxBankName.Items.Add("237 – Banco Bradesco S.A.");
            ComboBoxBankName.Items.Add("341 – Banco Itaú S.A.");
            ComboBoxBankName.Items.Add("356 – Banco Real S.A. (antigo)");
            ComboBoxBankName.Items.Add("389 – Banco Mercantil do Brasil S.A.");
            ComboBoxBankName.Items.Add("399 – HSBC Bank Brasil S.A. – Banco Múltiplo");
            ComboBoxBankName.Items.Add("422 – Banco Safra S.A.");
            ComboBoxBankName.Items.Add("453 – Banco Rural S.A.");
            ComboBoxBankName.Items.Add("633 – Banco Rendimento S.A.");
            ComboBoxBankName.Items.Add("652 – Itaú Unibanco Holding S.A.");
            ComboBoxBankName.Items.Add("745 – Banco Citibank S.A.");
            ComboBoxBankName.Items.Add("246 – Banco ABC Brasil S.A.");
            ComboBoxBankName.Items.Add("025 – Banco Alfa S.A.");
            ComboBoxBankName.Items.Add("641 – Banco Alvorada S.A.");
            ComboBoxBankName.Items.Add("029 – Banco Banerj S.A.");
            ComboBoxBankName.Items.Add("038 – Banco Banestado S.A.");
            ComboBoxBankName.Items.Add("000 – Banco Bankpar S.A.");
            ComboBoxBankName.Items.Add("740 – Banco Barclays S.A.");
            ComboBoxBankName.Items.Add("107 – Banco BBM S.A.");
            ComboBoxBankName.Items.Add("031 – Banco Beg S.A.");
            ComboBoxBankName.Items.Add("096 – Banco BM&F de Serviços de Liquidação e Custódia S.A");
            ComboBoxBankName.Items.Add("318 – Banco BMG S.A.");
            ComboBoxBankName.Items.Add("752 – Banco BNP Paribas Brasil S.A.");
            ComboBoxBankName.Items.Add("248 – Banco Boavista Interatlântico S.A.");
            ComboBoxBankName.Items.Add("036 – Banco Bradesco BBI S.A.");
            ComboBoxBankName.Items.Add("204 – Banco Bradesco Cartões S.A.");
            ComboBoxBankName.Items.Add("225 – Banco Brascan S.A.");
            ComboBoxBankName.Items.Add("044 – Banco BVA S.A.");
            ComboBoxBankName.Items.Add("263 – Banco Cacique S.A.");
            ComboBoxBankName.Items.Add("473 – Banco Caixa Geral – Brasil S.A.");
            ComboBoxBankName.Items.Add("222 – Banco Calyon Brasil S.A.");
            ComboBoxBankName.Items.Add("040 – Banco Cargill S.A.");
            ComboBoxBankName.Items.Add("M08 – Banco Citicard S.A.");
            ComboBoxBankName.Items.Add("M19 – Banco CNH Capital S.A.");
            ComboBoxBankName.Items.Add("215 – Banco Comercial e de Investimento Sudameris S.A.");
            ComboBoxBankName.Items.Add("756 – Banco Cooperativo do Brasil S.A. – BANCOOB");
            ComboBoxBankName.Items.Add("748 – Banco Cooperativo Sicredi S.A.");
            ComboBoxBankName.Items.Add("505 – Banco Credit Suisse (Brasil) S.A.");
            ComboBoxBankName.Items.Add("229 – Banco Cruzeiro do Sul S.A.");
            ComboBoxBankName.Items.Add("003 – Banco da Amazônia S.A.");
            ComboBoxBankName.Items.Add("083-3 – Banco da China Brasil S.A.");
            ComboBoxBankName.Items.Add("707 – Banco Daycoval S.A.");
            ComboBoxBankName.Items.Add("M06 – Banco de Lage Landen Brasil S.A.");
            ComboBoxBankName.Items.Add("024 – Banco de Pernambuco S.A. – BANDEPE");
            ComboBoxBankName.Items.Add("456 – Banco de Tokyo-Mitsubishi UFJ Brasil S.A.");
            ComboBoxBankName.Items.Add("214 – Banco Dibens S.A.");
            ComboBoxBankName.Items.Add("047 – Banco do Estado de Sergipe S.A.");
            ComboBoxBankName.Items.Add("037 – Banco do Estado do Pará S.A.");
            ComboBoxBankName.Items.Add("041 – Banco do Estado do Rio Grande do Sul S.A.");
            ComboBoxBankName.Items.Add("004 – Banco do Nordeste do Brasil S.A.");
            ComboBoxBankName.Items.Add("265 – Banco Fator S.A.");
            ComboBoxBankName.Items.Add("M03 – Banco Fiat S.A.");
            ComboBoxBankName.Items.Add("224 – Banco Fibra S.A.");
            ComboBoxBankName.Items.Add("626 – Banco Ficsa S.A.");
            ComboBoxBankName.Items.Add("394 – Banco Finasa BMC S.A.");
            ComboBoxBankName.Items.Add("M18 – Banco Ford S.A.");
            ComboBoxBankName.Items.Add("233 – Banco GE Capital S.A.");
            ComboBoxBankName.Items.Add("734 – Banco Gerdau S.A.");
            ComboBoxBankName.Items.Add("M07 – Banco GMAC S.A.");
            ComboBoxBankName.Items.Add("612 – Banco Guanabara S.A.");
            ComboBoxBankName.Items.Add("M22 – Banco Honda S.A.");
            ComboBoxBankName.Items.Add("063 – Banco Ibi S.A. Banco Múltiplo");
            ComboBoxBankName.Items.Add("M11 – Banco IBM S.A.");
            ComboBoxBankName.Items.Add("604 – Banco Industrial do Brasil S.A.");
            ComboBoxBankName.Items.Add("320 – Banco Industrial e Comercial S.A.");
            ComboBoxBankName.Items.Add("653 – Banco Indusval S.A.");
            ComboBoxBankName.Items.Add("630 – Banco Intercap S.A.");
            ComboBoxBankName.Items.Add("249 – Banco Investcred Unibanco S.A.");
            ComboBoxBankName.Items.Add("184 – Banco Itaú BBA S.A.");
            ComboBoxBankName.Items.Add("479 – Banco ItaúBank S.A");
            ComboBoxBankName.Items.Add("M09 – Banco Itaucred Financiamentos S.A.");
            ComboBoxBankName.Items.Add("376 – Banco J. P. Morgan S.A.");
            ComboBoxBankName.Items.Add("074 – Banco J. Safra S.A.");
            ComboBoxBankName.Items.Add("217 – Banco John Deere S.A.");
            ComboBoxBankName.Items.Add("065 – Banco Lemon S.A.");
            ComboBoxBankName.Items.Add("600 – Banco Luso Brasileiro S.A.");
            ComboBoxBankName.Items.Add("755 – Banco Merrill Lynch de Investimentos S.A.");
            ComboBoxBankName.Items.Add("746 – Banco Modal S.A.");
            ComboBoxBankName.Items.Add("151 – Banco Nossa Caixa S.A.");
            ComboBoxBankName.Items.Add("045 – Banco Opportunity S.A.");
            ComboBoxBankName.Items.Add("623 – Banco Panamericano S.A.");
            ComboBoxBankName.Items.Add("611 – Banco Paulista S.A.");
            ComboBoxBankName.Items.Add("643 – Banco Pine S.A.");
            ComboBoxBankName.Items.Add("638 – Banco Prosper S.A.");
            ComboBoxBankName.Items.Add("747 – Banco Rabobank International Brasil S.A.");
            ComboBoxBankName.Items.Add("M16 – Banco Rodobens S.A.");
            ComboBoxBankName.Items.Add("072 – Banco Rural Mais S.A.");
            ComboBoxBankName.Items.Add("250 – Banco Schahin S.A.");
            ComboBoxBankName.Items.Add("749 – Banco Simples S.A.");
            ComboBoxBankName.Items.Add("366 – Banco Société Générale Brasil S.A.");
            ComboBoxBankName.Items.Add("637 – Banco Sofisa S.A.");
            ComboBoxBankName.Items.Add("464 – Banco Sumitomo Mitsui Brasileiro S.A.");
            ComboBoxBankName.Items.Add("082-5 – Banco Topázio S.A.");
            ComboBoxBankName.Items.Add("M20 – Banco Toyota do Brasil S.A.");
            ComboBoxBankName.Items.Add("634 – Banco Triângulo S.A.");
            ComboBoxBankName.Items.Add("208 – Banco UBS Pactual S.A.");
            ComboBoxBankName.Items.Add("M14 – Banco Volkswagen S.A.");
            ComboBoxBankName.Items.Add("655 – Banco Votorantim S.A.");
            ComboBoxBankName.Items.Add("610 – Banco VR S.A.");
            ComboBoxBankName.Items.Add("370 – Banco WestLB do Brasil S.A.");
            ComboBoxBankName.Items.Add("021 – BANESTES S.A. Banco do Estado do Espírito Santo");
            ComboBoxBankName.Items.Add("719 – Banif-Banco Internacional do Funchal (Brasil)S.A.");
            ComboBoxBankName.Items.Add("073 – BB Banco Popular do Brasil S.A.");
            ComboBoxBankName.Items.Add("078 – BES Investimento do Brasil S.A.-Banco de Investimento");
            ComboBoxBankName.Items.Add("069 – BPN Brasil Banco Múltiplo S.A.");
            ComboBoxBankName.Items.Add("070 – BRB – Banco de Brasília S.A.");
            ComboBoxBankName.Items.Add("477 – Citibank N.A.");
            ComboBoxBankName.Items.Add("081-7 – Concórdia Banco S.A.");
            ComboBoxBankName.Items.Add("487 – Deutsche Bank S.A. – Banco Alemão");
            ComboBoxBankName.Items.Add("751 – Dresdner Bank Brasil S.A. – Banco Múltiplo");
            ComboBoxBankName.Items.Add("062 – Hipercard Banco Múltiplo S.A.");
            ComboBoxBankName.Items.Add("492 – ING Bank N.V.");
            ComboBoxBankName.Items.Add("488 – JPMorgan Chase Bank");
            ComboBoxBankName.Items.Add("409 – UNIBANCO – União de Bancos Brasileiros S.A.");
            ComboBoxBankName.Items.Add("230 – Unicard Banco Múltiplo S.A.");
        }

        private void FillComboBoxPixType()
        {
            ComboBoxPixType.Items.Add("Aleatória");
            ComboBoxPixType.Items.Add("Email");
            ComboBoxPixType.Items.Add("CPF");
            ComboBoxPixType.Items.Add("CNPJ");
            ComboBoxPixType.Items.Add("Número de telefone celular"); 
        }
        
        private void PrepareFieldsForNewInput()
        {
            if (lawyers.Count > 0)
            {
                TextBoxUserAccount.Clear();
                TextBoxAccountType.Clear();
                TextBoxBankName.Clear();
                TextBoxAgencyNumber.Clear();
                TextBoxAccountNumber.Clear();
                TextBoxPix.Clear();
                TextBoxPixType.Clear();
                TextBoxBankAccount.Clear();
                TextBoxUserAccount.Text = lawyers[0].Name;
                ComboBoxBankAccount.Items.Clear();
            }
            else
            {
                MessageBox.Show("Você ainda não cadastrou os dados do(a) advogado(a) no sistema, por favor, cadastre os dados completos na aba \"Cadastrar advogado\" para depois adicionar os dados bancários.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                
        }

        private void FillBankAccountFields()
        {
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Read(LawyerSqlCommands.Read(lawyers[0]));
            var lawyer = lawyerBuilder.Lawyers;
            
            bank.AccountType = ValidateFields("Tipo de conta", TextBoxAccountType.Text) == false ? null : TextBoxAccountType.Text;
            bank.AgencyNumber = ValidateFields("Agência", TextBoxAgencyNumber.Text) == false ? null : TextBoxAgencyNumber.Text;
            bank.BankName = ValidateFields("Banco", TextBoxBankName.Text) == false ? null : TextBoxBankName.Text;
            bank.AccountNumber = ValidateFields("Conta", TextBoxAccountNumber.Text) == false ? null : TextBoxAccountNumber.Text;
            bank.Pix = TextBoxPix.Text == "" ? " " : TextBoxPix.Text;
            bank.IdLawyer = lawyer[0].IdLawyer;
            bank.UserBankAccount = ValidateFields("Titular", TextBoxUserAccount.Text) == false ? null : TextBoxUserAccount.Text;
            bank.Id = TextBoxIdGuid.Text;
            bank.PixType = TextBoxPixType.Text == "" ? " " : TextBoxPixType.Text;
            bank.IdSignature = lawyer[0].IdSignature;
        }

        private bool ValidateFields(string field, string control)
        {
            if (control == "")
            {
                MessageBox.Show($"Campo \"{field}\" é obrigatório!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                validFields.Add(false);
            }
            else
            {
                validFields.Add(true);
            }
            return true;
        }

        private void Create()
        {
            directorBankAccount.Builder = bankAccountBuilder;
            directorBankAccount.Create(bank);
            update = true;            
            Read();
            FillTextBoxAccount();
            FillComboBoxBankAccounts();
        }

        private void Read()
        {
            directorBankAccount.Builder = bankAccountBuilder;
            directorBankAccount.Read(BankAccountSqlCommands.Read(Signature.IdSignature));
            bankAccountList = bankAccountBuilder.BankAccountList;
            SyncFieldsWithDatabase();
        }

        private void Update()
        {            
            directorBankAccount.Builder = bankAccountBuilder;
            directorBankAccount.Update(bank);
            update = true;
            ComboBoxBankAccount.Items.Clear();
            Read();
            FillTextBoxAccount();
            FillComboBoxBankAccounts();
        }

        private void Delete()
        {
            var indexAux = ComboBoxBankAccount.SelectedIndex;
            ComboBoxBankAccount.Items.Clear();
            var result = MessageBox.Show($"Tem certeza que deseja remover a conta {bankAccountList[index].BankName} do sistema de cadastro?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                directorBankAccount.Builder = bankAccountBuilder;
                directorBankAccount.Delete(BankAccountSqlCommands.Delete(bankAccountList[index]));
                ComboBoxBankAccount.Items.Clear();
                bankAccountList.Remove(bankAccountList[indexAux]);
                FillComboBoxBankAccounts();

                MessageBox.Show("Conta removida com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                index = 0;
                Read();
                FillTextBoxAccount();
            }
        }

        private void SyncFieldsWithDatabase()
        {
            if(bankAccountList.Count > 0)
            {
                TextBoxUserAccount.Text = lawyers[0].Name;
                TextBoxAccountType.Text = bankAccountList[index].AccountType;
                TextBoxBankName.Text = bankAccountList[index].BankName;
                TextBoxAgencyNumber.Text = bankAccountList[index].AgencyNumber;
                TextBoxAccountNumber.Text = bankAccountList[index].AccountNumber;
                TextBoxPix.Text = FormatPix();
                TextBoxPixType.Text = bankAccountList[index].PixType;
                TextBoxIdGuid.Text = bankAccountList[index].Id;
            }
            else
            {
                ButtonDelete.IsEnabled = false;
                ButtonSave.IsEnabled = false;
            }
        }

        private void FillTextBoxAccount()
        {
            if (bankAccountList.Count > 0)
            {
                TextBoxBankAccount.Text = bankAccountList[index].BankName;
            }                
        }

        private void FillComboBoxBankAccounts()
        {
            foreach (var item in bankAccountList)
            {
                ComboBoxBankAccount.Items.Add(item.BankName);
            }
        }

        #endregion

        private void PageBankAccount_Loaded(object sender, RoutedEventArgs e)
        {
            FillTextBoxUserAccount();
            FillComboBoxAccountType();
            FillComboBoxBankName();
            FillComboBoxPixType();
            Read();
            FillTextBoxAccount();
            FillComboBoxBankAccounts();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            TextBoxIdGuid.Text = Guid.NewGuid().ToString(); ;
            update = false;
            PrepareFieldsForNewInput();
            ButtonDelete.IsEnabled = true;
            ButtonSave.IsEnabled = true;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            bool isValidFields = true;
            FillBankAccountFields();

            foreach (var field in validFields)
            {
                if (field == false)
                {
                    isValidFields = false;
                    break;
                }
            }

            if (isValidFields)
            {
                if (update == false)
                    Create();
                else
                    Update();
            }
            validFields.Clear();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void ComboBoxBankAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = ComboBoxBankAccount.SelectedIndex < 0 ? 0 : ComboBoxBankAccount.SelectedIndex;
            if(ComboBoxBankAccount.SelectedIndex >= 0)
            {
                TextBoxBankAccount.Text = ComboBoxBankAccount.SelectedItem.ToString();
                SyncFieldsWithDatabase();
            }            
        }

        private void ComboBoxPixType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxPixType.SelectedIndex >= 0)
                TextBoxPixType.Text = ComboBoxPixType.SelectedItem.ToString();
        }

        private void ComboBoxBankName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxBankName.SelectedIndex >= 0)
                TextBoxBankName.Text = ComboBoxBankName.SelectedItem.ToString();
        }

        private void ComboBoxAccountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxAccountType.SelectedIndex >= 0)
                TextBoxAccountType.Text = ComboBoxAccountType.SelectedItem.ToString();
        }                
    }
}
