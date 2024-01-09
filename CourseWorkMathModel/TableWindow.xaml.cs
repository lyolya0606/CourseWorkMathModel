﻿using System;
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
using System.Windows.Shapes;

namespace CourseWorkMathModel {
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window {
        private List<List<double>> _concentrations = new();

        public TableWindow(List<List<double>> concentrations) {
            InitializeComponent();
            _concentrations = concentrations;
            FillTable();
        }

        private void SetUpColumns() {
            var column = new DataGridTextColumn {
                Header = "Время контакта, с",
                Binding = new Binding("Time")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CCl" + '\u2082' + "=CClH" + ", моль/л",
                Binding = new Binding("Concentration1")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "HF" + ", моль/л",
                Binding = new Binding("Concentration2")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CrF" + '\u2083' + ", моль/л",
                Binding = new Binding("Concentration3")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "[CCl" + '\u2082' + "=CClH * HF * CrF" + '\u2083' + "]" + ", моль/л",
                Binding = new Binding("Concentration4")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CFCl" + '\u2082' + "-CClH" + '\u2082' + ", моль/л",
                Binding = new Binding("Concentration5")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CFCl=CClH" + ", моль/л",
                Binding = new Binding("Concentration6")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "HCl" + ", моль/л",
                Binding = new Binding("Concentration7")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "[CFCl" + '\u2082' + " - CClH" + '\u2082' + " * CrF" + '\u2083' + "]" + ", моль/л",
                Binding = new Binding("Concentration8")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "[CFCl=CClH * HF * CrF" + '\u2083' + "]" + ", моль/л",
                Binding = new Binding("Concentration9")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CF" + '\u2082' + "Cl-CClH" + '\u2082' + ", моль/л",
                Binding = new Binding("Concentration10")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CrF" + '\u2082' + "Cl" + ", моль/л",
                Binding = new Binding("Concentration11")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CCl" + '\u2083' + "-CClH" + '\u2082' + ", моль/л",
                Binding = new Binding("Concentration12")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "[CCl" + '\u2083' + "-CClH" + '\u2082' + " * CrF" + '\u2083' + "]" + ", моль/л",
                Binding = new Binding("Concentration13")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "[CF" + '\u2082' + "Cl-CClH" + '\u2082' + " * CrF" + '\u2083' + "]" + ", моль/л",
                Binding = new Binding("Concentration14")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CF" + '\u2082' + "=CClH" + ", моль/л",
                Binding = new Binding("Concentration15")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "[CF" + '\u2082' + "=CClH" + '\u2082' + " * HF * CrF" + ", моль/л",
                Binding = new Binding("Concentration16")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CF" + '\u2083' + "-CClH" + '\u2082' + ", моль/л",
                Binding = new Binding("Concentration17")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CF" + '\u2083' + "-CFH" + '\u2082' + ", моль/л",
                Binding = new Binding("Concentration18")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "[2CF" + '\u2083' + "-CClH" + '\u2082' + " * CrF" + '\u2083' + "]" + ", моль/л",
                Binding = new Binding("Concentration19")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CF" + '\u2083' + "-CH" + '\u2083' + ", моль/л",
                Binding = new Binding("Concentration20")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CF" + '\u2083' + "-CCl" + '\u2082' + "H" + ", моль/л",
                Binding = new Binding("Concentration21")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CF" + '\u2083' + "-CFClH" + ", моль/л",
                Binding = new Binding("Concentration22")
            };
            valuesDataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "CF" + '\u2083' + "-CF" + '\u2082' + "H" + ", моль/л",
                Binding = new Binding("Concentration23")
            };
            valuesDataGrid.Columns.Add(column);
        }

        private record DataForTable {
            public required double Time { get; set; }
            public required double Concentration1 { get; set; }
            public required double Concentration2 { get; set; }
            public required double Concentration3 { get; set; }
            public required double Concentration4 { get; set; }
            public required double Concentration5 { get; set; }
            public required double Concentration6 { get; set; }
            public required double Concentration7 { get; set; }
            public required double Concentration8 { get; set; }
            public required double Concentration9 { get; set; }
            public required double Concentration10 { get; set; }
            public required double Concentration11 { get; set; }
            public required double Concentration12 { get; set; }
            public required double Concentration13 { get; set; }
            public required double Concentration14 { get; set; }
            public required double Concentration15 { get; set; }
            public required double Concentration16 { get; set; }
            public required double Concentration17 { get; set; }
            public required double Concentration18 { get; set; }
            public required double Concentration19 { get; set; }
            public required double Concentration20 { get; set; }
            public required double Concentration21 { get; set; }
            public required double Concentration22 { get; set; }
            public required double Concentration23 { get; set; }
        }

        private void FillTable() {
            SetUpColumns();
            List<DataForTable> data = new();
            
            for (int i = 0; i < _concentrations[0].Count; i++) {
                data.Add(new DataForTable {
                    Time = Math.Round(_concentrations[0][i], 4),
                    Concentration1 = Math.Round(_concentrations[1][i], 4),
                    Concentration2 = Math.Round(_concentrations[2][i], 4),
                    Concentration3 = Math.Round(_concentrations[3][i], 4),
                    Concentration4 = Math.Round(_concentrations[4][i], 4),
                    Concentration5 = Math.Round(_concentrations[5][i], 4),
                    Concentration6 = Math.Round(_concentrations[6][i], 4),
                    Concentration7 = Math.Round(_concentrations[7][i], 4),
                    Concentration8 = Math.Round(_concentrations[8][i], 4),
                    Concentration9 = Math.Round(_concentrations[9][i], 4),
                    Concentration10 = Math.Round(_concentrations[10][i], 4),
                    Concentration11 = Math.Round(_concentrations[11][i], 4),
                    Concentration12 = Math.Round(_concentrations[12][i], 4),
                    Concentration13 = Math.Round(_concentrations[13][i], 4),
                    Concentration14 = Math.Round(_concentrations[14][i], 4),
                    Concentration15 = Math.Round(_concentrations[15][i], 4),
                    Concentration16 = Math.Round(_concentrations[16][i], 4),
                    Concentration17 = Math.Round(_concentrations[17][i], 4),
                    Concentration18 = Math.Round(_concentrations[18][i], 4),
                    Concentration19 = Math.Round(_concentrations[19][i], 4),
                    Concentration20 = Math.Round(_concentrations[20][i], 4),
                    Concentration21 = Math.Round(_concentrations[21][i], 4),
                    Concentration22 = Math.Round(_concentrations[22][i], 4),
                    Concentration23 = Math.Round(_concentrations[23][i], 4)
                });
                
            }

            //foreach (var conc in _concentrations) {
            //    for (int i = 0; i < conc.Count; i++) {
            //        data.Add(new DataForTable {
            //            Time = Math.Round(conc[0], 4),
            //            Concentration1 = Math.Round(conc[1], 4),
            //            Concentration2 = Math.Round(conc[2], 4),
            //            Concentration3 = Math.Round(conc[3], 4),
            //            Concentration4 = Math.Round(conc[4], 4),
            //            Concentration5 = Math.Round(conc[5], 4),
            //            Concentration6 = Math.Round(conc[6], 4),
            //            Concentration7 = Math.Round(conc[7], 4),
            //            Concentration8 = Math.Round(conc[8], 4),
            //            Concentration9 = Math.Round(conc[9], 4),
            //            Concentration10 = Math.Round(conc[10], 4),
            //            Concentration11 = Math.Round(conc[11], 4),
            //            Concentration12 = Math.Round(conc[12], 4),
            //            Concentration13 = Math.Round(conc[13], 4),
            //            Concentration14 = Math.Round(conc[14], 4),
            //            Concentration15 = Math.Round(conc[15], 4),
            //            Concentration16 = Math.Round(conc[16], 4),
            //            Concentration17 = Math.Round(conc[17], 4),
            //            Concentration18 = Math.Round(conc[18], 4),
            //            Concentration19 = Math.Round(conc[19], 4),
            //            Concentration20 = Math.Round(conc[20], 4),
            //            Concentration21 = Math.Round(conc[21], 4),
            //            Concentration22 = Math.Round(conc[22], 4),
            //            Concentration23 = Math.Round(conc[23], 4)
            //        });
            //    }
            //}
            valuesDataGrid.ItemsSource = data;
        }
    }
}
 