using Advocacy_Software.Advocacy.Software.Concrete.Builders.Address;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.City;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.Person;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.State;
using Advocacy_Software.Advocacy.Software.Director.Person;
using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared.SqlCommands;
using Advocacy_Software.Advocacy.Software.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Advocacy_Software.Pages
{
    /// <summary>
    /// Interaction logic for PageRegisterCustomer.xaml
    /// </summary>
    public partial class PageRegisterCustomer : Page
    {
        private Signatures signature = new();

        private List<Cities> cities = new();
        private List<States> states = new();
        private List<Customers> customers = new();
        private List<bool> validFields = new();
        private Customers customer = new();
        private States state = new();
        private AddressEntityModel address = new();
        private Cities city = new();
        private bool update = true;
        private int index = 0;

        private Director directorCity = new();
        private Director directorState = new();
        private Director directorCustomer = new();
        private Director directorAddress = new();
        private ConcreteCityBuilder cityBuilder = new();
        private ConcreteStateBuilder stateBuilder = new();
        private ConcreteCustomerBuilder customerBuilder = new();
        private ConcreteAddressBuilder addressBuilder = new();

        public PageRegisterCustomer(Signatures signature) 
        {
            this.signature = signature;
            InitializeComponent();
        }

        #region Private methods

        private void FillComboBoxCustomers()
        {
            if (ComboBoxCustomers.Items.Count > 0)
                ComboBoxCustomers.Items.Clear();

            foreach (Customers item in customers)
                ComboBoxCustomers.Items.Add(item.Name);
        }

        private void LoadResources()
        {
            FillComboBoxStates();
            Read();
            SyncFieldsWithDatabase();
            if (customers.Count > 0)
            {
                LabelNumberOfRows.Content = $"{index + 1} de {customers.Count}";
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

            address.Street = TextBoxAddress.Text;
            address.Number = TextBoxNumber.Text;
            address.Neighbourhood = TextBoxNeighbourhood.Text;
            address.ZipCode = TextBoxZipCode.Text;
            address.Id = TextBoxGuidId.Text;
            address.Complement = TextBoxComplement.Text == "" ? " " : TextBoxComplement.Text;
            address.IdSignature = signature.IdSignature;

            if (update == false)
                directorAddress.Create(address);

            directorAddress.Read(AddressSqlCommands.Read(TextBoxGuidId.Text));
            customer.IdAddress = addressBuilder.Address.IdAddress;
        }

        private void Create()
        {
            InsertAddress();
            directorCustomer.Builder = customerBuilder;
            directorCustomer.Create(customer);           
            LoadResources();
            FillComboBoxCustomers();
            update = true;
        }

        private void Read()
        {
            directorCustomer.Builder = customerBuilder;
            directorAddress.Builder = addressBuilder;

            directorCustomer.Read(CustomerSqlCommands.Read(signature.IdSignature));
            customers = customerBuilder.CustomersList;
            if (customers.Count > 0)
            {
                directorAddress.Read(AddressSqlCommands.Read(customers[index].Id));
                address = addressBuilder.Address;
            }
        }

        private void Update()
        {
            customer.Phone = new string((from c in TextBoxPhoneNumber.Text where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) select c).ToArray());

            customer.LastUpdate = DateTime.Now.ToString("dd/MM/yyyy");

            UpdateAddress();
            directorCustomer.Builder = customerBuilder;
            directorCustomer.Read(CustomerSqlCommands.Read(customer.IdSignature));
            customer.IdCustomer = customerBuilder.CustomersList[0].IdCustomer;
            directorCustomer.Update(customer);
            LoadResources();
            FillComboBoxCustomers();
        }

        private void Delete()
        {
            ComboBoxCustomers.Items.Clear();
            var result = MessageBox.Show($"Tem certeza que deseja remover {customers[index].Name} do sistema de cadastro?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                directorCustomer.Builder = customerBuilder;
                directorCustomer.Delete(CustomerSqlCommands.Delete(customers[index]));
                directorAddress.Builder = addressBuilder;
                directorAddress.Delete(AddressSqlCommands.Delete(customers[index].IdAddress, customers[index].IdSignature));
                customers.Remove(customers[index]);

                MessageBox.Show("Cliente removido do sistema de cadastro com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                index = 0;
                LoadResources();
                FillComboBoxCustomers();
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

            address.Street = TextBoxAddress.Text;
            address.Number = TextBoxNumber.Text;
            address.Neighbourhood = TextBoxNeighbourhood.Text;
            address.ZipCode = new string((from c in TextBoxZipCode.Text where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) select c).ToArray());
            address.Id = TextBoxGuidId.Text;
            address.Complement = TextBoxComplement.Text == "" ? " " : TextBoxComplement.Text;
            address.IdSignature = signature.IdSignature;

            if (update == true)
                directorAddress.Update(address);

            directorAddress.Read(AddressSqlCommands.Read(TextBoxGuidId.Text));
            customer.IdAddress = addressBuilder.Address.IdAddress;
        }

        private void SyncFieldsWithDatabase()
        {
            index = ComboBoxCustomers.SelectedIndex < 0 ? 0 : ComboBoxCustomers.SelectedIndex;

            if (customers.Count > 0)
            {
                TextBoxName.Text = customers[index].Name;
                TextBoxNationality.Text = customers[index].Nationality;
                TextBoxIdentityCustomer.Text = customers[index].IdentityCustomer;
                TextBoxCpfOrCnpj.Text = customers[index].CpfOrCnpj?.Length == 11 ? Convert.ToInt64(customers[index].CpfOrCnpj).ToString(@"000\.000\.000-00") : Convert.ToInt64(customers[index].CpfOrCnpj).ToString(@"00\.000\.000/0000-00");
                TextBoxCivilStatus.Text = customers[index].CivilStatus;
                TextBoxProfession.Text = customers[index].Profession;
                TextBoxEmail.Text = customers[index].Email;
                TextBoxPhoneNumber.Text = customers[index].Phone?.Length == 11 ? Convert.ToInt64(customers[index].Phone).ToString(@"(00)00000-0000") : Convert.ToInt64(customers[index].Phone).ToString(@"(00)0000-0000");
                TextBoxRegisterDate.Text = customers[index].RegisterDate;
                TextBoxLastUpdate.Text = customers[index].LastUpdate;
                TextBoxGuidId.Text = customers[index].Id;
                TextBoxAddress.Text = address.Street;
                TextBoxNumber.Text = address.Number;
                TextBoxNeighbourhood.Text = address.Neighbourhood;
                TextBoxComplement.Text = address.Complement;
                TextBoxZipCode.Text = Convert.ToInt64(address.ZipCode).ToString(@"00000-000");

                TextBoxCustomers.Text = customers[index].Name;

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
            var cpfOrCnpj = new string((from c in TextBoxCpfOrCnpj.Text where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) select c).ToArray());

            customer.Name = ValidateFields("Nome", TextBoxName.Text) == false ? null : TextBoxName.Text;
            customer.Nationality = ValidateFields("Nacionalidade", TextBoxNationality.Text) == false ? null : TextBoxNationality.Text;
            customer.IdentityCustomer = ValidateFields("Identidade", TextBoxIdentityCustomer.Text) == false ? null : TextBoxIdentityCustomer.Text;
            customer.CpfOrCnpj = ValidateFields("CPF ou CNPJ", cpfOrCnpj) == false ? null : cpfOrCnpj;
            customer.CivilStatus = ValidateFields("Estado civil", TextBoxCivilStatus.Text) == false ? null : TextBoxCivilStatus.Text;
            customer.Profession = ValidateFields("Profissão", TextBoxProfession.Text) == false ? null : TextBoxProfession.Text;
            customer.Email = ValidateFields("Email", TextBoxEmail.Text) == false ? null : TextBoxEmail.Text;
            customer.Phone = ValidateFields("Telefone", TextBoxPhoneNumber.Text) == false ? null : TextBoxPhoneNumber.Text;
            customer.RegisterDate = TextBoxRegisterDate.Text;
            customer.LastUpdate = TextBoxLastUpdate.Text;
            customer.Id = TextBoxGuidId.Text;
            customer.IdSignature = this.signature.IdSignature;

            state.State = ValidateFields("UF", ComboBoxUf.SelectedItem.ToString()) == false ? null : ComboBoxUf.SelectedItem.ToString();

            city.City = ValidateFields("Cidade", ComboBoxCities.SelectedItem.ToString()) == false ? null : ComboBoxCities.SelectedItem.ToString();
            address.Street = ValidateFields("Endereço", TextBoxAddress.Text) == false ? null : TextBoxAddress.Text;
            address.Number = ValidateFields("Número", TextBoxNumber.Text) == false ? null : TextBoxNumber.Text;
            address.Neighbourhood = ValidateFields("Bairro", TextBoxNeighbourhood.Text) == false ? null : TextBoxNeighbourhood.Text;
            address.Complement = TextBoxComplement.Text == " " ? "" : TextBoxComplement.Text;
            address.ZipCode = ValidateFields("CEP", TextBoxZipCode.Text) == false ? null : TextBoxZipCode.Text;
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
            TextBoxNationality.Clear();
            TextBoxIdentityCustomer.Clear();
            TextBoxEmail.Clear();
            TextBoxCpfOrCnpj.Clear();
            TextBoxCivilStatus.Clear();
            TextBoxProfession.Clear();
            TextBoxAddress.Clear();
            TextBoxNumber.Clear();
            TextBoxNeighbourhood.Clear();
            TextBoxZipCode.Clear();
            TextBoxPhoneNumber.Clear();
            TextBoxGuidId.Clear();
            TextBoxName.Focus();
            TextBoxGuidId.Text = Guid.NewGuid().ToString();
            TextBoxComplement.Clear();
            TextBoxCustomers.Clear();

            ComboBoxCities.Items.Clear();
            ComboBoxUf.SelectedItem = null;
            ComboBoxCities.SelectedItem = null;
            ComboBoxUf.SelectedValue = null;
            ComboBoxCities.SelectedValue = null;
            ComboBoxCustomers.Items.Clear();
            ComboBoxCustomers.SelectedItem = null;
            ComboBoxCustomers.SelectedValue = null;

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

            foreach (var state in states)
            {
                ComboBoxUf.Items.Add(state.State);
            }
        }

        private void FillComboBoxCities()
        {
            this.states.Clear();
            this.cities.Clear();
            ComboBoxCities.Items.Clear();

            directorCity.Builder = cityBuilder;
            directorState.Builder = stateBuilder;

            if (ComboBoxUf.SelectedValue != null)
            {
                directorState.Read(StateSqlCommands.GetIdState(ComboBoxUf.SelectedItem.ToString()));
                this.states = stateBuilder.State;
            }

            if (states.Count > 0)
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

        private void ComboBoxUf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBoxCities();
        }

        private void PageRegisterCustomer1_Loaded(object sender, RoutedEventArgs e)
        {
            LoadResources();
            FillComboBoxCustomers();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void ComboBoxCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCustomers.SelectedIndex >= 0)
            {
                index = ComboBoxCustomers.SelectedIndex;
                TextBoxCustomers.Text = ComboBoxCustomers.Items[index].ToString();
                LabelNumberOfRows.Content = $"{index + 1} de {customers.Count}";
                LoadResources();
            }
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            update = false;
            PrepareControlsForNewInput();
            ButtonDelete.IsEnabled = true;
            ButtonSave.IsEnabled = true;
        }

        private void ButtonSave_Click_1(object sender, RoutedEventArgs e)
        {
            bool isValidFields = true;
            FillFields();

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
                PersonalDocumentValidator validator = new(customer.CpfOrCnpj);
                bool isValidDocument;
                isValidDocument = customer.CpfOrCnpj.Trim().Length == 11 ? validator.ValidateCPF() : validator.ValidateCNPJ();

                if (isValidDocument)
                {
                    if (update == false)
                        Create();
                    else
                        Update();
                }
                else
                {
                    MessageBox.Show("CPF ou CNPJ inválido! Tente novamente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    TextBoxCpfOrCnpj.Focus();
                }
            }
            validFields.Clear();
        }
    }
}
