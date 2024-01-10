using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace CourseWorkMathModel {
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window {
        private Dictionary<string, int> _dictMethods = new Dictionary<string, int>() {
            { "Метод Гира", 0},
            { "Метод Адамса", 1},
            { "Неявный метод Рунге-Кутта третьего порядка точности", 2},
        };
        public AdminWindow() {
            InitializeComponent();
            FillTable();
            FillComboBox();
        }

        private void back_ButtonClick(object sender, RoutedEventArgs e) {
            StreamWriter sr = new StreamWriter("method.txt");
            sr.WriteLine(methodComboBox.Text);
            sr.Close();
            new LoginWindow().Show();
            Close();
        }



        private void FillComboBox() {
            methodComboBox.Items.Add("Метод Гира");
            methodComboBox.Items.Add("Метод Адамса");
            methodComboBox.Items.Add("Неявный метод Рунге-Кутта третьего порядка точности");

            StreamReader sr = new StreamReader("method.txt");
            string line = sr.ReadLine();
            sr.Close();

            methodComboBox.SelectedIndex = _dictMethods[line];
        }

        private void SetUpColumns() {
            var column = new DataGridTextColumn {
                Header = "Предэкспоненциальный множитель (A)",
                Binding = new Binding("A")
            };
            kineticDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "Значение A",
                Binding = new Binding("AValue")
            };
            kineticDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "Энергия активации (E)",
                Binding = new Binding("E")
            };
            kineticDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "Значение E, Дж/моль",
                Binding = new Binding("EValue")
            };
            kineticDataGrid.Columns.Add(column);
        }

        private record DataForTable {
            public required string A { get; set; }
            public required string AValue { get; set; }
            public required string E { get; set; }
            public required string EValue { get; set; }
        }

        private void editButtonClick(object sender, RoutedEventArgs e) {
            EditWindow editWindow = new(_currentAID, _currentEID, _currentAName, _currentEName, _currentAValue, _currentEValue);
            editWindow.ShowDialog();

            kineticDataGrid.ItemsSource = null;
            kineticDataGrid.Columns.Clear();
            FillTable();

        }

        string _currentAID;
        string _currentEID;
        string _currentAName;
        string _currentEName;
        double _currentAValue;
        double _currentEValue;

        private void kineticDataGridSelectionChanged(object sender, SelectionChangedEventArgs e) {
            editButton.IsEnabled = true;
            int selectedRow = kineticDataGrid.SelectedIndex;

            if (selectedRow == -1) { return; }

            _currentAID = (selectedRow + 1).ToString();
            _currentEID = (selectedRow + 22).ToString();

            TextBlock? aName = kineticDataGrid.Columns[0].GetCellContent(kineticDataGrid.Items[selectedRow]) as TextBlock;
            _currentAName = aName.Text;

            TextBlock? eName = kineticDataGrid.Columns[2].GetCellContent(kineticDataGrid.Items[selectedRow]) as TextBlock;
            _currentEName = eName.Text;

            TextBlock? aValue = kineticDataGrid.Columns[1].GetCellContent(kineticDataGrid.Items[selectedRow]) as TextBlock;
            _currentAValue = double.Parse(aValue.Text);

            TextBlock? eValue = kineticDataGrid.Columns[3].GetCellContent(kineticDataGrid.Items[selectedRow]) as TextBlock;
            _currentEValue = double.Parse(eValue.Text);

        }

        private void FillTable() {
            SetUpColumns();
            List<DataForTable> data = new();
            DatabaseWork databaseWork = new();
            List<List<string>> aData = databaseWork.GetAFromDatabase();
            List<List<string>> eData = databaseWork.GetEFromDatabase();
            for (int i = 0; i < aData.Count; i++) {
                data.Add(new DataForTable { A = aData[i][0], AValue = aData[i][1], E = eData[i][0], EValue = eData[i][1] });
            }

            kineticDataGrid.ItemsSource = data;
        }
    }
}
