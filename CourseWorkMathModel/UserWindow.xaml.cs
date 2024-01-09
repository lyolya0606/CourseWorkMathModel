﻿using System;
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
using LiveCharts.Defaults;
using LiveCharts;
using LiveCharts.Wpf;
using static Python.Runtime.TypeSpec;


namespace CourseWorkMathModel {
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window {
        private const int COUNT_OF_ELEMENTS = 23;
        private const int COUNT_OF_REACTIONS = 21;
        private List<List<double>> _concentrations = new();
        private List<int> _chosenElements = new() { 17 };
        private ChooseElementWindow _chooseElementWindow = new();
        private List<string> _elements = new() { "CCl" + '\u2082' + "=CClH", "HF", "CrF" + '\u2083', "[CCl" + '\u2082' + "=CClH * HF * CrF" + '\u2083' + "]",
            "CFCl" + '\u2082' + "-CClH" + '\u2082', "CFCl=CClH", "HCl", "[CFCl" + '\u2082' + " - CClH" + '\u2082' + " * CrF" + '\u2083' + "]", "[CFCl=CClH * HF * CrF" + '\u2083' + "]",
            "CF" + '\u2082' + "Cl-CClH" + '\u2082', "CrF" + '\u2082' + "Cl", "CCl" + '\u2083' + "-CClH" + '\u2082', "[CCl" + '\u2083' + "-CClH" + '\u2082' + " * CrF" + '\u2083' + "]",
            "[CF" + '\u2082' + "Cl-CClH" + '\u2082' + " * CrF" + '\u2083' + "]", "CF" + '\u2082' + "=CClH", "[CF" + '\u2082' + "=CClH" + '\u2082' + " * HF * CrF", "CF" + '\u2083' + "-CClH" + '\u2082',
            "CF" + '\u2083' + "-CFH" + '\u2082', "[2CF" + '\u2083' + "-CClH" + '\u2082' + " * CrF" + '\u2083' + "]", "CF" + '\u2083' + "-CH" + '\u2083', "CF" + '\u2083' + "-CCl" + '\u2082' + "H",
            "CF" + '\u2083' + "-CFClH", "CF" + '\u2083' + "-CF" + '\u2082' + "H"};


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

        // TODO: -0 wtf
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
            _concentrations = pythonMathModel.RunScript();
            DrawCharts();
            showTableButton.IsEnabled = true;
        }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public SeriesCollection SeriesCollectionConc { get; set; }
        Func<double, string> FormatFunc = (x) => string.Format("{0:0.0000}", x);

        private void showTableButton_Click(object sender, RoutedEventArgs e) {
            TableWindow tableWindow = new TableWindow(_concentrations);
            tableWindow.ShowDialog();
        }

        private void chooseButtonClick(object sender, RoutedEventArgs e) {
            _chooseElementWindow.ShowDialog();
            _chosenElements = _chooseElementWindow.GetChosenElements();

        }

        private void mainUserWindowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            //if (_chooseElementWindow.IsActive) {
            //    _chooseElementWindow.Close();
            //}
        }

        private void mainUserWindowClosed(object sender, EventArgs e) {
            if (_chooseElementWindow.Visibility == Visibility.Hidden) {
                _chooseElementWindow.Close();
            }
            this.Close();
        }

        private Brush[] _colors = { Brushes.Blue, Brushes.Red, Brushes.Purple, Brushes.Pink, Brushes.Orange, Brushes.Green, Brushes.Gold, Brushes.Gray,
        Brushes.GreenYellow, Brushes.SkyBlue, Brushes.DarkOliveGreen, Brushes.Black, Brushes.Brown, Brushes.DarkBlue, Brushes.HotPink, Brushes.Lime, Brushes.Plum, 
        Brushes.Tomato, Brushes.Cyan, Brushes.DarkViolet, Brushes.DarkMagenta, Brushes.Peru, Brushes.Maroon};

        private void back_ButtonClick(object sender, RoutedEventArgs e) {
            new LoginWindow().Show();
            Close();
        }

        private void DrawCharts() {
            concChart.DataContext = null;
            concChart.AxisX.Clear();
            concChart.AxisY.Clear();
            
            concChart.AxisX.Add(new Axis { Title = "Время контакта, с", FontSize = 15 });
            concChart.AxisY.Add(new Axis { Title = "Концентрация, моль/л", LabelFormatter = FormatFunc, FontSize = 15 });

            List<LineSeries> lines = new();
            SeriesCollectionConc = new SeriesCollection();
            int color = 0;
            foreach (var item in _chosenElements) {
                var points = new ChartValues<ObservablePoint>();
                for (var i = 0; i < _concentrations[0].Count; i++)
                    points.Add(new ObservablePoint {
                        X = _concentrations[0][i],
                        Y = _concentrations[item + 1][i]
                    });
                PointLabel = chartPoint => $"{"Время контакта"}: {Math.Round(chartPoint.X, 4)}, {"Концентрация"}: {Math.Round(chartPoint.Y, 4)}";

                LineSeries line = new LineSeries {
                    Values = new ChartValues<ObservablePoint>(points),
                    PointGeometrySize = 10,
                    Fill = Brushes.Transparent,
                    LabelPoint = PointLabel,
                    Title = _elements[item],
                    //PointForeground = 
                    Stroke = _colors[color],
                };
                lines.Add(line);
                color++;
 


            }
            SeriesCollectionConc.AddRange(lines);
            concChart.DataContext = this;

            //var points = new ChartValues<ObservablePoint>();

            //for (var i = 0; i < _concentrations[0].Count; i++)
            //    points.Add(new ObservablePoint {
            //        X = _concentrations[0][i],
            //        Y = _concentrations[18][i]
            //    });
            //PointLabel = chartPoint => $"{"Время контакта"}: {Math.Round(chartPoint.X, 4)}, {"Концентрация"}: {Math.Round(chartPoint.Y, 4)}";


            //SeriesCollectionConc = new SeriesCollection {
            //    new LineSeries {
            //        Values = new ChartValues<ObservablePoint>(points),
            //        PointGeometrySize = 10,
            //        Fill = Brushes.Transparent,
            //        LabelPoint = PointLabel,
            //        Title = "sadfsgthyjujyhtgre"
            //    }
            //};


        }
    }
}
