using Advocacy_Software.Advocacy.Software.Concrete.Builders.Address;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.City;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.Person;
using Advocacy_Software.Advocacy.Software.Concrete.Builders.State;
using Advocacy_Software.Advocacy.Software.Director.Person;
using Advocacy_Software.Advocacy.Software.Entities;
using Advocacy_Software.Advocacy.Software.Shared.SqlCommands;
using Advocacy_Software.Advocacy.Software.Shared.Utils;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Advocacy_Software.Pages
{
    /// <summary>
    /// Interaction logic for PageGenerateAttorney.xaml
    /// </summary>
    public partial class PageGenerateAttorney : Page
    {        
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
        private Lawyer lawyer = new();
        private AttorneyEntity attorney = new();

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
            if(lawyerList.Count > 0)
                TextBoxLawyer.Text = lawyerList[0].Name;        
        }

        #endregion

        private void ButtonGenerateAttorney_Click(object sender, RoutedEventArgs e)
        {
            attorney.Signature = signature;
            attorney.SpecificPowers = TextBoxSpecificPowers.Text;
            lawyer.Name = TextBoxLawyer.Text;
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
                attorney.Lawyer = lawyerBuilder.Lawyers[0];
                directorAddress.Read(AddressSqlCommands.Read(attorney.Lawyer.Id));
                attorney.AddressLawyer = addressBuilder.Address;
                directorAddress.Read(AddressSqlCommands.Read(attorney.Customer.Id));
                attorney.AddressCustomer = addressBuilder.Address;
                directorCity.Read(CitySqlCommands.Read(attorney.AddressLawyer.IdCity));
                attorney.CityLawyer = cityBuilder.CitiesList;
                directorCity.Read(CitySqlCommands.Read(attorney.AddressCustomer.IdCity));
                attorney.CityCustomer = cityBuilder.CitiesList;

                directorState.Read(StateSqlCommands.Select(attorney.CityLawyer[0].IdState));
                attorney.UfLawyer = stateBuilder.State[0].State;
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
    }
}
