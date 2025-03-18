namespace Advocacy_Software.Pages
{
    /// <summary>
    /// Interaction logic for PageRegisterCustomer.xaml
    /// </summary>
    public partial class PageGenerateFeesContract : Page
    {
        #region Variables

        private readonly Signatures Signature = new();
        private Director directorLawyer = new();
        private Director directorCustomer = new();
        private Director directorCity = new();
        private Director directorAddress = new();
        private Director directorState = new();
        private Director directorBankAccount = new();

        private ConcreteLawyerBuilder lawyerBuilder = new();
        private ConcreteCustomerBuilder customerBuilder = new();
        private ConcreteCityBuilder cityBuilder = new();
        private ConcreteAddressBuilder addressBuilder = new();
        private ConcreteStateBuilder stateBuilder = new();
        private ConcreteBankAccountBuilder bankAccountBuilder = new();
               
        private Lawyer lawyer = new();
        private FeesContractEntity contract = new();
        private List<Cities> cities = new();
        private List<States> states = new();
        private List<bool> validFields = new();

        #endregion

        public PageGenerateFeesContract(Signatures signature) 
        {
            Signature = signature;
            InitializeComponent();
        }

        #region Private methods

        private void SelectLawyer()
        {
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Read(LawyerSqlCommands.Read(Signature.IdSignature));
            contract.Lawyer = lawyerBuilder.Lawyers;
            if(contract.Lawyer.Count > 0)
                TextBoxLawyer.Text = contract.Lawyer[0].Name;
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

        private void FillFeesContractFields()
        {
            contract.Signature = Signature;
            contract.CpfOrCnpj = ValidateFields("CPF ou CNPJ", TextBoxCpfOrCnpj.Text) == false ? null : TextBoxCpfOrCnpj.Text;
            contract.ActionName = ValidateFields("Nome da ação", TextBoxActionName.Text) == false ? null : TextBoxActionName.Text;
            contract.TotalServiceValue = ValidateFields("Valor total do serviço", TextBoxTotalValue.Text) == false ? 0 : Convert.ToDecimal(TextBoxTotalValue.Text);
            contract.SuccessFees = ValidateFields("Honorários de êxito", TextBoxSuccessFees.Text) == false ? null : TextBoxSuccessFees.Text;
            contract.InstallmentsNumber = TextBoxInstallmentsNumber.Text == "" ? 1 : Convert.ToInt32(TextBoxInstallmentsNumber.Text);
            contract.InitialValue = TextBoxInitialValue.Text == "" ? 0 : Convert.ToDecimal(TextBoxInitialValue.Text);
            contract.Uf = ValidateFields("Selecione um estado", ComboBoxStates.Text) == false ? null : ComboBoxStates.Text;
            contract.City = ValidateFields("Selecione uma cidade", ComboBoxCities.Text) == false ? null : ComboBoxCities.Text;
            contract.PaymentType = ValidateFields("Selecione uma forma de pagamento", ComboBoxPaymentType.SelectedItem.ToString()) == false ? null : ComboBoxPaymentType.SelectedItem.ToString();

            lawyer.Name = TextBoxLawyer.Text;
            lawyer.IdSignature = Signature.IdSignature;
            directorCity.Builder = cityBuilder;
            directorAddress.Builder = addressBuilder;
            directorState.Builder = stateBuilder;

            directorCustomer.Builder = customerBuilder;
            directorCustomer.Read(CustomerSqlCommands.Read(TextBoxCpfOrCnpj.Text, Signature.IdSignature));
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Read(LawyerSqlCommands.Read(lawyer));
            contract.Customer = customerBuilder.CustomersList;
            if(contract.Customer.Count > 0)
            {
                contract.Lawyer = lawyerBuilder.Lawyers;
                directorAddress.Read(AddressSqlCommands.Read(contract.Lawyer[0].Id));
                contract.AddressLawyer = addressBuilder.Address;
                directorAddress.Read(AddressSqlCommands.Read(contract.Customer[0].Id));
                contract.AddressCustomer = addressBuilder.Address;
                directorCity.Read(CitySqlCommands.Read(contract.AddressLawyer.IdCity));
                contract.CityLawyer = cityBuilder.CitiesList;
                directorCity.Read(CitySqlCommands.Read(contract.AddressCustomer.IdCity));
                contract.CityCustomer = cityBuilder.CitiesList;

                directorState.Read(StateSqlCommands.Select(contract.CityLawyer[0].IdState));
                contract.UfLawyer = stateBuilder.State[0].State;
                directorState.Read(StateSqlCommands.Select(contract.CityCustomer[0].IdState));
                contract.UfCustomer = stateBuilder.State[0].State;
            }
            else 
            {
                MessageBox.Show("CPF ou CNPJ inválido! Verifique os dados e tente novamente.", "Cliente não encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }
        
        private void FillComboBoxStates()
        {
            ComboBoxStates.Items.Clear();

            List<States> states = new()
            {
                new States() { State = "AC" },
                new States() { State = "AL" },
                new States() { State = "AP" },
                new States() { State = "AM" },
                new States() { State = "BA" },
                new States() { State = "CE" },
                new States() { State = "DF" },
                new States() { State = "ES" },
                new States() { State = "GO" },
                new States() { State = "MA" },
                new States() { State = "MT" },
                new States() { State = "MS" },
                new States() { State = "MG" },
                new States() { State = "PA" },
                new States() { State = "PB" },
                new States() { State = "PR" },
                new States() { State = "PE" },
                new States() { State = "PI" },
                new States() { State = "RJ" },
                new States() { State = "RN" },
                new States() { State = "RS" },
                new States() { State = "RO" },
                new States() { State = "RR" },
                new States() { State = "SC" },
                new States() { State = "SE" },
                new States() { State = "SP" },
                new States() { State = "SO" },
                new States() { State = "TO" }
            };

            foreach (var state in states)
            {
                ComboBoxStates.Items.Add(state.State);
            }
        }
                
        private void LoadResources()
        {
            FillComboBoxStates();
            SelectLawyer();
            FillComboBoxPaymentType();
            FillComboBoxBankAccount();
        }

        private void FillComboBoxPaymentType()
        {
            ComboBoxPaymentType.Items.Add("À vista");
            ComboBoxPaymentType.Items.Add("Boleto");
            ComboBoxPaymentType.Items.Add("Depósito bancário");
            ComboBoxPaymentType.Items.Add("Pix");
        }
           
        private void FillComboBoxBankAccount()
        {
            directorBankAccount.Builder = bankAccountBuilder;
            directorBankAccount.Read(BankAccountSqlCommands.Read(Signature.IdSignature));
            contract.BankAccount = bankAccountBuilder.BankAccountList;

            if(contract.BankAccount.Count > 0) 
                foreach (var bankAccount in contract.BankAccount)
                    ComboBoxBankAccount.Items.Add(bankAccount.BankName);
        }

        private void FillComboBoxCities()
        {            
            ComboBoxCities.Items.Clear();

            directorCity.Builder = cityBuilder;
            directorState.Builder = stateBuilder;
            directorState.Read(StateSqlCommands.GetIdState(ComboBoxStates.SelectedItem.ToString()));
            states = stateBuilder.State;
            directorCity.Read(CitySqlCommands.GetCitiesById(states[0].IdState));
            this.cities = cityBuilder.CitiesList;
                        
            if (cities.Count > 0)
                foreach (var city in cities)
                    ComboBoxCities.Items.Add(city.City);
        }

        #endregion

        #region Events
        private void ButtonGenerateFeesContract_Click(object sender, RoutedEventArgs e)
        {      
            bool isValidFields = true;
            FillFeesContractFields();

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
                if(contract.Customer.Count > 0)
                {
                    FeesContractGenerator generate = new();
                    contract.PdfPath = SaveFile.Save("Salvar contrato de honorários");
                    generate.GenerateContract(contract);
                    MessageBox.Show("Contrato de honorários gerado com sucesso!", "Contrato de honorários", MessageBoxButton.OK, MessageBoxImage.Information);                                        
                }
            }
        }

        private void PageFeesContractGenerator_Loaded(object sender, RoutedEventArgs e)
        {
            LoadResources();
        }

        private void ComboBoxStates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBoxCities();
        }
        #endregion
    }
}
