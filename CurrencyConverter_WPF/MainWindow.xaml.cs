using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverter_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //CurrencyConverter currencyConverter;
        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
            //currencyConverter = new CurrencyConverter();
        }

        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            // Add rows in the Datatable with text and value
            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("SAR", 20);
            dtCurrency.Rows.Add("POUND", 5);
            dtCurrency.Rows.Add("DEM", 43);

            // The data to currency Combobox assigned from datatable
            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            // DisplayMemberPath is used to display data in Combobox
            cmbFromCurrency.DisplayMemberPath = "Text";
            // Selected Value is used to set the value in the Combobox
            cmbFromCurrency.SelectedValue = "Value";
            // SelectedIndex is used to bind last selected value. Default is Select (@ index 0)
            cmbFromCurrency.SelectedIndex = 0;

            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValue = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            //Variable ConvertedValue with double data type to store currency converted value
            double convertedValue;

            //Check amount textbox is Null or Blank
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                //If amount textbox is Null or Blank it will show the below message box   
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //After clicking on message box OK sets the Focus on amount textbox
                txtCurrency.Focus();
                return;
            }
            //Else if the currency from is not selected or it is default text --SELECT--
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                //It will show the message
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on From Combobox
                cmbFromCurrency.Focus();
                return;
            }
            //Else if Currency To is not Selected or Select Default Text --SELECT--
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                //It will show the message
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on To Combobox
                cmbToCurrency.Focus();
                return;
            }
            //If From and To Combobox selected values are the same
            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                //Use double.parse to convert datatype string to a double.
                //Textbox txtCurrency is a string and ConvertedValue is a double datatype
                convertedValue = double.Parse(txtCurrency.Text);

                //ConvertedValue will appear in lblCurrency Label with the name of currency 
                lblCurrency.Content = cmbToCurrency.Text + " " + convertedValue.ToString("N3");
            }
            else
            {
                //Do the manual conversion based on given values in static datatable
                convertedValue = (double.Parse(cmbFromCurrency.SelectedValue.ToString()) *
                    double.Parse(txtCurrency.Text)) /
                    double.Parse(cmbToCurrency.SelectedValue.ToString());

                //Show in label converted currency and converted currency name.
                lblCurrency.Content = cmbToCurrency.Text + " " + convertedValue.ToString("N3");
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearConverter();
        }

        private void ClearConverter()
        {
            // Error handling in case textCurrency Textbox is empty
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
            {
                cmbFromCurrency.SelectedIndex = 0;
            }
            if (cmbToCurrency.Items.Count > 0)
            {
                cmbToCurrency.SelectedIndex = 0;
            }
            lblCurrency.Content = string.Empty;
            txtCurrency.Focus();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Use Regex to write regular expression for validation textbox
            // Reference using statement: System.Text.RegularExpressions;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //private void MainWindow_Load()
        //{
        //    double convertedValue;
        //    //lblCurrency.Content = convertedValue.ToString();

        //    Dictionary<string, string> currencySymbolsData = currencyConverter.GetCurrencySymbols();
        //    cmbFromCurrency.Items.Clear();
        //    cmbToCurrency.Items.Clear();
        //    Binding dataBinding = new Binding("Currency");
        //    dataBinding.Source = currencySymbolsData;


        //    //cmbFromCurrency.Items.DataSource = new BindingSource(currencySymbolsData, null);
        //}
    }
}
