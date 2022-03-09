using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace PasswordGeneratorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<ComboBoxItem> passwordLengthList;
        public MainWindow()
        {
            InitializeComponent();
            passwordLengthList = new List<ComboBoxItem>();
            passwordLengthList.Add(new ComboBoxItem { Content = "weak" , IsEnabled=false, FontWeight = FontWeights.Bold });
            for (int i = 5; i < 16; i++)
                passwordLengthList.Add(new ComboBoxItem { Content = i});
            passwordLengthList.Add(new ComboBoxItem { Content = "strong", IsEnabled = false , FontWeight = FontWeights.Bold});
            for (int i = 16; i < 65; i++)
                passwordLengthList.Add(new ComboBoxItem { Content = i });
            passwordLengthList.Add(new ComboBoxItem { Content = "weird, but ok", IsEnabled = false, FontWeight = FontWeights.Bold });
            for (int i = 7; i < 11; i++)
                passwordLengthList.Add(new ComboBoxItem { Content = Math.Pow(2, i) });
            passwordLength.ItemsSource = passwordLengthList;


        }

        private void passwordLength_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((int.TryParse((passwordLength.SelectedItem as ComboBoxItem).Content.ToString(), out int len))&&(len > 64)) 
            {
                repeatings.IsChecked = true;
                repeatings.IsEnabled = false;
            }
            else
            {
                repeatings.IsEnabled = true;
            }
        }

        private void button_Generate_Click(object sender, RoutedEventArgs e)
        {
            string password = String.Empty;
            try
            {
                if (!int.TryParse((passwordLength.SelectedItem as ComboBoxItem).Content.ToString(), out int pswdlength)) throw new NullReferenceException();
                PasswordGenerator PswdGen = new PasswordGenerator(pswdlength, lowercase.IsChecked ?? true, uppercase.IsChecked ?? true, numbers.IsChecked ?? true, symbols.IsChecked ?? true, repeatings.IsChecked ?? true, !(similar.IsChecked ?? true));
                password = PswdGen.Generate();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You should choose password length!");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                outputTextBox.Text = password;
            }
            
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(outputTextBox.Text);
        }
    }
}
