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
            FillComboBoxLawyers();

            if (lawyers.Count > 0) 
            {
                LabelNumberOfLawyers.Content = $"{index + 1} de {lawyers.Count}";
            }            
        }

        
        private void Create()
        {
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
        }

        private void Update()
        {
            
            lawyer.LastUpdate = DateTime.Now.ToString("dd/MM/yyyy");

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
                lawyers.Remove(lawyers[index]);

                MessageBox.Show("Advogado removido do sistema de cadastro com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                index = 0;
                PrepareControlsForNewInput();
                TextBoxGuidId.Clear();
                TextBoxRegisterDate.Clear();
                TextBoxLastUpdate.Clear();
            }
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
                ComboBoxLawyers.Text = lawyers[index].Name;

            }
            else
            {
                ButtonDelete.IsEnabled = false;
                ButtonSave.IsEnabled = false;
            }
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
                        
            ComboBoxUfOab.SelectedItem = null;
            ComboBoxUfOab.SelectedValue = null;
            
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

            ComboBoxUfOab.Items.Clear();

            foreach (var state in states)
            {                
                ComboBoxUfOab.Items.Add(state.State);
            }
        }
                
        private void FillComboBoxLawyers()
        {
            if (ComboBoxLawyers.Items.Count > 0)
                ComboBoxLawyers.Items.Clear();

            foreach (Lawyer item in lawyers)
                ComboBoxLawyers.Items.Add(item.Name);
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
               
        private void PageRegisterLawyers_Loaded(object sender, RoutedEventArgs e)
        {
            LoadResources();
        }

        #endregion

        private void ComboBoxLawyers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxLawyers.SelectedIndex >= 0)
            {
                index = ComboBoxLawyers.SelectedIndex;
                ComboBoxLawyers.Text = ComboBoxLawyers.Items[index].ToString();                
                LoadResources();
            }
        }
    }
}
