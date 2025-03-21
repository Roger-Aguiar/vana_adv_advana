﻿namespace Advocacy_Software.Pages
{
    /// <summary>
    /// Interaction logic for PageGenerateHipossuficiencyContract.xaml
    /// </summary>
    public partial class PageGenerateHipossuficiencyContract : Page
    {
        #region Variables

        private Director directorLawyer = new();
        private Director directorCustomer = new();
        private Director directorCity = new();
        private Director directorAddress = new();
        private Director directorState = new();

        private ConcreteLawyerBuilder lawyerBuilder = new();
        private ConcreteCustomerBuilder customerBuilder = new();
        private ConcreteCityBuilder cityBuilder = new();
        private ConcreteAddressBuilder addressBuilder = new();
        private ConcreteStateBuilder stateBuilder = new();

        private Signatures signature = new();
        private HipossuficiencyEntity contract = new();

        #endregion

        public PageGenerateHipossuficiencyContract(Signatures signature)
        {
            this.signature = signature;
            InitializeComponent();
        }

        #region Private methods

        private void FillTextBoxLawyer()
        {
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Read(LawyerSqlCommands.Read(signature.IdSignature));
            contract.Lawyer = lawyerBuilder.Lawyers;
            if(contract.Lawyer.Count > 0)
                TextBoxLawyer.Text = contract.Lawyer[0].Name;
        }

        #endregion

        #region Events

        private void ButtonGenerateHipossuficiencyContract_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxCpfOrCnpj.Text == null || TextBoxCpfOrCnpj.Text == "")
            {
                MessageBox.Show("Campo CPF ou CNPJ é de preenchimento obrigatório!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                TextBoxCpfOrCnpj.Focus();
            }
            else 
            {
                contract.Signature = signature;

                directorCity.Builder = cityBuilder;
                directorAddress.Builder = addressBuilder;
                directorState.Builder = stateBuilder;
                directorCustomer.Builder = customerBuilder;

                directorCustomer.Read(CustomerSqlCommands.Read(TextBoxCpfOrCnpj.Text, signature.IdSignature));
                contract.Customer = customerBuilder.CustomersList;
                
                if(contract.Customer.Count > 0)
                {
                    directorAddress.Read(AddressSqlCommands.Read(contract.Customer[0].Id));
                    contract.AddressCustomer = addressBuilder.Address;
                    directorCity.Read(CitySqlCommands.Read(contract.AddressCustomer.IdCity));
                    contract.City = cityBuilder.CitiesList;
                    directorState.Read(StateSqlCommands.Select(contract.City[0].IdState));
                    contract.UfCustomer = stateBuilder.State[0].State;

                    directorAddress.Read(AddressSqlCommands.Read(contract.Lawyer[0].Id));
                    contract.AddressLawyer = addressBuilder.Address;
                    directorCity.Read(CitySqlCommands.Read(contract.AddressLawyer.IdCity));
                    contract.CityLawyer = cityBuilder.CitiesList;
                    directorState.Read(StateSqlCommands.Select(contract.CityLawyer[0].IdState));
                    contract.UfLawyer = stateBuilder.State[0].State;

                    contract.PdfFile = SaveFile.Save("Salvar contrato de hipossuficiência");
                    if (contract.PdfFile != null)
                    {
                        HipossuficiencyContract contractGenerator = new();
                        contractGenerator.GenerateContract(contract);

                        MessageBox.Show("Contrato de hipossuficiência gerado com sucesso!", "Contrato de hipossuficiência", MessageBoxButton.OK, MessageBoxImage.Information);                        
                    }
                }
                else
                {
                    MessageBox.Show("CPF ou CNPJ inválido! Verifique os dados e tente novamente.", "Cliente não encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }            
        }

        private void PageAttorneyHipossuficiencyContract_Loaded(object sender, RoutedEventArgs e)
        {
            FillTextBoxLawyer();
        }

        #endregion
    }
}
