namespace Advocacy_Software.Pages
{
    /// <summary>
    /// Interaction logic for PageRegisterLawyer.xaml
    /// </summary>
    public partial class PageRegisterLawyer : Page
    {
        #region Variables

        private Signatures signature = new();        
        
        private List<Cities> cities = new();
        private List<States> states = new();
        private List<Lawyer> lawyers = new();
        private List<bool> validFields = new();
        private Lawyer lawyer = new();
        private States state = new();
        private AddressEntityModel address = new();
        private Cities city = new();
        private bool update = true;
        private int index = 0;

        private Director directorCity = new();
        private Director directorState = new();
        private Director directorLawyer = new();
        private Director directorAddress = new();
        private Director directorBankAccount = new();
        private ConcreteCityBuilder cityBuilder = new();
        private ConcreteStateBuilder stateBuilder = new();
        private ConcreteLawyerBuilder lawyerBuilder = new();
        private ConcreteAddressBuilder addressBuilder = new();
        private ConcreteBankAccountBuilder bankAccountBuilder = new();

        #endregion

        public PageRegisterLawyer(Signatures signature)
        {
            this.signature = signature;
            InitializeComponent();
        }
                             
        
        #region Private methods
                
        private void LoadResources()
        {
            FillComboBoxStates();
            Read();
            SyncFieldsWithDatabase();
            if(lawyers.Count > 0) 
            {
                LabelNumberOfLawyers.Content = $"{index + 1} de {lawyers.Count}";
            }            
        }

        private void InsertAddress()
        {
            foreach (var item in cities)
            {
                if (item.City == ComboBoxCities.SelectedItem.ToString())
                {
                    address.IdCity = item.IdCity;
                    break;
                }
            }

            directorAddress.Builder = addressBuilder;

            address.Id = TextBoxGuidId.Text;
            address.IdSignature = signature.IdSignature;

            if (update == false)
                directorAddress.Create(address);

            directorAddress.Read(AddressSqlCommands.Read(TextBoxGuidId.Text));
            lawyer.IdAddress = addressBuilder.Address.IdAddress;                        
        }

        private void Create()
        {
            InsertAddress();
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Create(lawyer);
            LoadResources();
            update = true;
        }

        private void Read()
        {
            directorLawyer.Builder = lawyerBuilder;
            directorAddress.Builder = addressBuilder;

            directorLawyer.Read(LawyerSqlCommands.Read(signature.IdSignature));
            lawyers = lawyerBuilder.Lawyers;
            if(lawyers.Count > 0)
            {
                directorAddress.Read(AddressSqlCommands.Read(lawyers[index].Id));
                address = addressBuilder.Address;
            }            
        }

        private void Update()
        {
            
            lawyer.LastUpdate = DateTime.Now.ToString("dd/MM/yyyy");

            UpdateAddress();
            directorLawyer.Builder = lawyerBuilder;
            directorLawyer.Read(LawyerSqlCommands.Read(lawyer.IdSignature));
            lawyer.IdLawyer = lawyerBuilder.Lawyers[0].IdLawyer;
            directorLawyer.Update(lawyer);
            LoadResources();
        }

        private void Delete()
        {
            var result = MessageBox.Show($"Tem certeza que deseja remover {lawyers[index].Name} do sistema de cadastro?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                directorBankAccount.Builder = bankAccountBuilder;
                directorBankAccount.Delete(BankAccountSqlCommands.Delete(lawyers[index]));
                directorLawyer.Builder = lawyerBuilder;
                directorLawyer.Delete(LawyerSqlCommands.Delete(lawyers[index]));
                directorAddress.Builder = addressBuilder;
                directorAddress.Delete(AddressSqlCommands.Delete(lawyers[index].IdAddress, lawyers[index].IdSignature));
                lawyers.Remove(lawyers[index]);

                MessageBox.Show("Advogado removido do sistema de cadastro com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                index = 0;
                PrepareControlsForNewInput();
                TextBoxGuidId.Clear();
                TextBoxRegisterDate.Clear();
                TextBoxLastUpdate.Clear();
            }
        }

        private void UpdateAddress()
        {
            foreach (var item in cities)
            {
                if (item.City == ComboBoxCities.SelectedItem.ToString())
                {
                    address.IdCity = item.IdCity;
                    break;
                }
            }

            directorAddress.Builder = addressBuilder;

            address.Id = TextBoxGuidId.Text;
            address.IdSignature = signature.IdSignature;

            if (update == true)
                directorAddress.Update(address);

            directorAddress.Read(AddressSqlCommands.Read(TextBoxGuidId.Text));
            lawyer.IdAddress = addressBuilder.Address.IdAddress;
        }

        private void SyncFieldsWithDatabase()
        {
            if (lawyers.Count > 0)
            {
                TextBoxName.Text = lawyers[index].Name;
                TextBoxProfession.Text = lawyers[index].Profession;
                TextBoxOabNumber.Text = lawyers[index].OabNumber;                
                TextBoxRegisterDate.Text = lawyers[index].RegisterDate;
                TextBoxLastUpdate.Text = lawyers[index].LastUpdate;
                TextBoxGuidId.Text = lawyers[index].Id;

                ComboBoxUfOab.Items.Insert(0, lawyers[index].UfOab);
                ComboBoxUfOab.Text = lawyers[index].UfOab.ToString();                 

                SyncStateAndCityWithDatabase();
            }
            else
            {
                ButtonDelete.IsEnabled = false;
                ButtonSave.IsEnabled = false;
            }
        }

        private List<Cities> SelectCity()
        {            
            directorCity.Builder = cityBuilder;
            directorCity.Read(CitySqlCommands.Read(address.IdCity));
            return cityBuilder.CitiesList;
        }

        private void SyncStateAndCityWithDatabase()
        {
            var city = SelectCity();
            directorState.Builder = stateBuilder;
            directorState.Read(StateSqlCommands.Select(city[0].IdState));
            states = stateBuilder.State;

            ComboBoxUf.Items.Insert(0, states[0].State);
            ComboBoxUf.Text = states[0].State;
            ComboBoxCities.Items.Insert(0, city[0].City);
            ComboBoxCities.Text = city[0].City;
        }

        private void FillFields()
        {
            lawyer.Name = ValidateFields("Nome", TextBoxName.Text) == false ? null : TextBoxName.Text;
            lawyer.Profession = ValidateFields("Profissão", TextBoxProfession.Text) == false ? null : TextBoxProfession.Text;
            lawyer.OabNumber = ValidateFields("Número da OAB", TextBoxOabNumber.Text) == false ? null : TextBoxOabNumber.Text;
            lawyer.UfOab = ValidateFields("UF da OAB", ComboBoxUfOab.SelectedItem.ToString()) == false ? null : ComboBoxUfOab.SelectedItem.ToString();
            lawyer.RegisterDate = TextBoxRegisterDate.Text;
            lawyer.LastUpdate = TextBoxLastUpdate.Text;
            lawyer.Id = TextBoxGuidId.Text;
            lawyer.IdSignature = this.signature.IdSignature;

            state.State = ValidateFields("UF", ComboBoxUf.SelectedItem.ToString()) == false ? null : ComboBoxUf.SelectedItem.ToString();

            city.City = ValidateFields("Cidade", ComboBoxCities.SelectedItem.ToString()) == false ? null : ComboBoxCities.SelectedItem.ToString();
                     
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

        private void PrepareControlsForNewInput()
        {
            TextBoxName.Clear();
            TextBoxProfession.Clear();
            TextBoxGuidId.Clear();
            TextBoxName.Focus();
            TextBoxGuidId.Text = Guid.NewGuid().ToString();
            TextBoxOabNumber.Clear();

            ComboBoxCities.Items.Clear();            
            ComboBoxUf.SelectedItem = null;
            ComboBoxCities.SelectedItem = null;
            ComboBoxUfOab.SelectedItem = null;
            ComboBoxUf.SelectedValue = null;
            ComboBoxUfOab.SelectedValue = null;
            ComboBoxCities.SelectedValue = null;
            
            TextBoxRegisterDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TextBoxLastUpdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void FillComboBoxStates()
        {
            var states = new List<States>
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

            ComboBoxUf.Items.Clear();
            ComboBoxUfOab.Items.Clear();

            foreach (var state in states)
            {
                ComboBoxUf.Items.Add(state.State);
                ComboBoxUfOab.Items.Add(state.State);
            }
        }

        private void FillComboBoxCities()
        {
            this.states.Clear();
            this.cities.Clear();
            ComboBoxCities.Items.Clear();

            directorCity.Builder = cityBuilder;
            directorState.Builder = stateBuilder;            

            if(ComboBoxUf.SelectedValue != null)
            {
                directorState.Read(StateSqlCommands.GetIdState(ComboBoxUf.SelectedItem.ToString()));
                this.states = stateBuilder.State;
            }
            
            if(states.Count > 0)
            {
                directorCity.Read(CitySqlCommands.GetCitiesById(states[0].IdState));
                this.cities = cityBuilder.CitiesList;
            }
            
                       
            if (cities.Count > 0)
            {
                foreach (var city in cities)
                {
                    ComboBoxCities.Items.Add(city.City);
                }
            }
        }

        #endregion

        #region Events

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            update = false;
            PrepareControlsForNewInput();
            ButtonDelete.IsEnabled = true;
            ButtonSave.IsEnabled = true;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            bool isValidFields = true;
            FillFields();
                        
            foreach (var field in validFields) 
            {
                if(field == false)
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

        private void ComboBoxUf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBoxCities();
        }

        private void PageRegisterLawyers_Loaded(object sender, RoutedEventArgs e)
        {
            LoadResources();
        }

        #endregion
    }
}
