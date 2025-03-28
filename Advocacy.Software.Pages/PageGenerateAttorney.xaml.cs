namespace Advocacy_Software.Pages
{
    /// <summary>
    /// Interaction logic for PageGenerateAttorney.xaml
    /// </summary>
    public partial class PageGenerateAttorney : Page
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
        private List<Lawyer> lawyerList = new();
        private List<Lawyer> lawyersInAttorney = new();
        private Lawyer lawyer = new();
        private AttorneyEntity attorney = new();

        #endregion

        public PageGenerateAttorney(Signatures signature)
        {
            this.signature = signature;
            InitializeComponent();
        }

        #region Private methods
        
        private void FillComboBoxLawyers()
        {
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Read(LawyerSqlCommands.Read(signature.IdSignature));
            lawyerList = lawyerBuilder.Lawyers;
            foreach (Lawyer lawyer in lawyerList)
                ComboBoxLawyers.Items.Add(lawyer.Name);
        }

        #endregion

        #region Events

        private void ButtonGenerateAttorney_Click(object sender, RoutedEventArgs e)
        {
            attorney.LawyerInAttorney = lawyersInAttorney;
            attorney.City = TextBoxcity.Text;
            attorney.Signature = signature;
            attorney.SpecificPowers = TextBoxSpecificPowers.Text;
            lawyer.Name = ComboBoxLawyers.SelectedItem.ToString();
            lawyer.IdSignature = signature.IdSignature;
            directorCity.Builder = cityBuilder;
            directorAddress.Builder = addressBuilder;
            directorState.Builder = stateBuilder;

            directorCustomer.Builder = customerBuilder;
            directorCustomer.Read(CustomerSqlCommands.Read(TextBoxCpfOrCnpj.Text, signature.IdSignature));
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Read(LawyerSqlCommands.Read(lawyer));

            if(customerBuilder.CustomersList.Count > 0)
            {
                attorney.Customer = customerBuilder.CustomersList[0];
                attorney.Lawyer = lawyerBuilder.Lawyers;
                attorney.AddressLawyer = addressBuilder.Address;
                directorAddress.Read(AddressSqlCommands.Read(attorney.Customer.Id));
                attorney.AddressCustomer = addressBuilder.Address;
                attorney.CityLawyer = cityBuilder.CitiesList;
                directorCity.Read(CitySqlCommands.Read(attorney.AddressCustomer.IdCity));
                attorney.CityCustomer = cityBuilder.CitiesList;
                directorState.Read(StateSqlCommands.Select(attorney.CityCustomer[0].IdState));
                attorney.UfCustomer = stateBuilder.State[0].State;

                attorney.PdfPath = SaveFile.Save("Salvar procuração");
                AttorneyGenerator attorneyGenerator = new();
                attorneyGenerator.GenerateAttorney(attorney);

                MessageBox.Show("Procuração gerada com sucesso!", "Procuração", MessageBoxButton.OK, MessageBoxImage.Information);            
            }
            else
            {
                MessageBox.Show("CPF ou CNPJ inválido! Verifique os dados e tente novamente.", "Cliente não encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PageAttorneyGenerator_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBoxLawyers();
        }

        #endregion
                
        private void ComboBoxLawyers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lawyersInAttorney.Add(lawyerList[ComboBoxLawyers.SelectedIndex]);
            MessageBox.Show("Você adicionou um(a) advogado(a) na procuração, " +
                "caso queira adicionar outro(a) advogado(a), basta selecionar na lista", 
                "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
