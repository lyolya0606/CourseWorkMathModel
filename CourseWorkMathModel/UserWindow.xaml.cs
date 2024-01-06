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
using LiveCharts.Wpf;


namespace CourseWorkMathModel {
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window {
        private const int COUNT_OF_ELEMENTS = 23;
        private const int COUNT_OF_REACTIONS = 21;

        public UserWindow() {
            InitializeComponent();
            FillTable();
            concChart.AxisX.Clear();
            concChart.AxisY.Clear();

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            var regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SetUpColumns() {
            var column = new DataGridTextColumn {
                Header = "Вещество",
                Binding = new Binding("Element")
            };
            concDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "Концентрация, моль/л",
                Binding = new Binding("Concentration")
            };
            concDataGrid.Columns.Add(column);
        }

        private record DataForTable {
            public required string Element { get; set; }
            public required double Concentration { get; set; }
        }


        private void FillTable() {
            SetUpColumns();
            List<DataForTable> data = new();

            data.Add(new DataForTable { Element = "CCl" + '\u2082' + "=CClH", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "HF", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CrF" + '\u2083', Concentration = 0.1 });
            data.Add(new DataForTable { Element = "[CCl" + '\u2082' + "=CClH * HF * CrF" + '\u2083' + "]", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CFCl" + '\u2082' + "-CClH" + '\u2082', Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CFCl=CClH", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "HCl", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "[CFCl" + '\u2082' + "-CClH" + '\u2082' + " * CrF" + '\u2083' + "]", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "[CFCl=CClH * HF * CrF" + '\u2083' + "]", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CF" + '\u2082' + "Cl-CClH" + '\u2082', Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CrF" + '\u2082' + "Cl", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CCl" + '\u2083' + "-CClH" + '\u2082', Concentration = 0.1 });
            data.Add(new DataForTable { Element = "[CCl" + '\u2083' + "-CClH" + '\u2082' +  " * CrF" + '\u2083' + "]", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "[CF" + '\u2082' + "Cl-CClH" + '\u2082' + " * CrF" + '\u2083' + "]", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CF" + '\u2082' + "=CClH", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "[CF" + '\u2082' + "=CClH" + '\u2082' + " * HF * CrF" + '\u2083' + "]", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CF" + '\u2083' + "-CClH" + '\u2082', Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CF" + '\u2083' + "-CFH" + '\u2082', Concentration = 0.1 });
            data.Add(new DataForTable { Element = "[2CF" + '\u2083' + "-CClH" + '\u2082' + " * CrF" + '\u2083' + "]", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CF" + '\u2083' + "-CH" + '\u2083', Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CF" + '\u2083' + "-CCl" + '\u2082' + "H", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CF" + '\u2083' + "-CFClH", Concentration = 0.1 });
            data.Add(new DataForTable { Element = "CF" + '\u2083' + "-CF" + '\u2082' + "H", Concentration = 0.1 });
            concDataGrid.ItemsSource = data;
        }

        private List<double> GetStartConcentration() {
            List<double> startConcentrations = new();

            for (int i = 0; i < COUNT_OF_ELEMENTS; i++) {
                try {
                    var x = concDataGrid.Columns[1].GetCellContent(concDataGrid.Items[i]) as TextBlock;
                    startConcentrations.Add(double.Parse(x.Text));
                } catch (Exception)  {
                    
                } 

            }
            return startConcentrations;
        }

        private List<double> GetReactionSpeed() {
            List<double> reactionSpeed = new();

            for (int i = 0; i < COUNT_OF_REACTIONS; i++) {
                reactionSpeed.Add(2);
            }
            return reactionSpeed;
        }

        private double GetContactTime() {
            return double.Parse(timeTextBox.Text);
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e) {
            List<double> startConcentrations = new();
            try {
                startConcentrations = GetStartConcentration();
            } catch (Exception) {
                return;
            }
            List<double> reactionSpeed = GetReactionSpeed();
            double contactTime = GetContactTime();

            PythonMathModel pythonMathModel = new PythonMathModel(startConcentrations, reactionSpeed, contactTime);
        }
    }
}
