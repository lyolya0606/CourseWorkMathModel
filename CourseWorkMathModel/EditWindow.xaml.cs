using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CourseWorkMathModel {
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window {
        private string _aIndex;
        private string _eIndex;
        private string _aName;
        private string _eName;
        private double _aValue;
        private double _eValue;

        public EditWindow(string aIndex, string eIndex, string aName, string eName, double aValue, double eValue) {
            InitializeComponent();
            _aIndex = aIndex;
            _eIndex = eIndex;
            _aName = aName;
            _eName = eName;
            _aValue = aValue;
            _eValue = eValue;

            aTextBox.Text = _aName;
            eTextBox.Text = _eName;
            aValueTextBox.Text = _aValue.ToString();
            eValueTextBox.Text = _eValue.ToString();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            var regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OKButtonClick(object sender, RoutedEventArgs e) {
            string aName = aTextBox.Text.ToString();
            string eName = eTextBox.Text.ToString();
            double aValue = double.Parse(aValueTextBox.Text);
            double eValue = double.Parse(eValueTextBox.Text);


            DatabaseWork databaseWork = new DatabaseWork();
            try {
                databaseWork.UpdateAE(_aIndex, aName);
                databaseWork.UpdateAE(_eIndex, eName);
                databaseWork.UpdateAEValue(_aIndex, aValue);
                databaseWork.UpdateAEValue(_eIndex, eValue);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Изменения применены", "Удача!", MessageBoxButton.OK);
            Close();
        }
    }
}
