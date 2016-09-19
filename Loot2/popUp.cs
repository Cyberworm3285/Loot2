using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loot2
{
    /// <summary>
    ///     Pop-Up mit Diagramm
    /// </summary>
    public partial class popUpDia : Form
    {
        /// <summary>
        ///     FarbKomponente, die Bestandteile von <see cref="Color.FromArgb(int)"/> ist
        /// </summary>
        private int R = 255, G = 128, B = 0;
        private Random randomizer = new Random();
        /// <summary>
        ///     Namen der Marker am Diagramm (<see cref="popUpDia"/>)
        /// </summary>
        string[] rarNames;
        /// <summary>
        ///     Grenzen der Rarities für Marker am Diagramm (<see cref="popUpDia"/>)
        /// </summary>
        int[] rarBounds;
        /// <summary>
        ///     Farben der MArker am Diagramm (<see cref="popUpDia"/>)
        /// </summary>
        Color[] rarCol;

        /// <summary>
        ///     Konstrktor, der sämtliche Variablen initialisiert
        /// </summary>
        /// <param name="cfg">Die Configurationsdatei aus dem Hauptordner geladen (mit <see cref="ConfigLoader"/>)</param>
        public popUpDia(Config cfg)
        {
            InitializeComponent();
            initializeChart();
            rarNames = cfg.rarNamesCfg;
            rarBounds = cfg.rarBoundsCfg;
            rarCol = cfg.rarColCfg;
        }

        /// <summary>
        ///  setzt die Standard-Formatierung des Diagramms fest
        /// </summary>
        private void initializeChart()
        {
            dataPieChart.ChartAreas[0].AxisX.MajorGrid.Interval = 10.0;
            dataPieChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(64, 0, 0, 0);
            dataPieChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            dataPieChart.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.FromArgb(64, 0, 0, 0);
            dataPieChart.ChartAreas[0].AxisY.MajorGrid.Interval = 1.0;
        }

        /// <summary>
        ///     gibt die angegebenen Daten in dem Diagramm aus
        /// </summary>
        /// <param name="items">fertig gefilterte List(<see cref="Loot"/>)</param>
        /// <param name="maxValue">Gesamtlänge des Rarity-Zahlenstrahls</param>
        public void drawPie(List<Loot> items, int maxValue)
        {
            dataPieChart.Series[0].Points.Clear();
            items.Sort(new LootComparer());

            //damit auch das Wahrscheinlichste Item ordentlich angezeigt werden kann
            double maxPercentage = (double)items[items.Count - 1].rarity / (maxValue / 100);
            dataPieChart.ChartAreas[0].AxisY.Maximum = 1.30 * maxPercentage;

            double allPercentage = 0.0;

            for (int i = 0; i < items.Count; i++)
            {
                double percentage = (double)(items[i].rarity) / (maxValue / 100.0);
                allPercentage += percentage;
                dataPieChart.Series[0].Points.AddXY(i, percentage);
                dataPieChart.Series[0].Points[i].Color = farbVerlauf((int) 2.5f * i, 255);
                dataPieChart.Series[0].Points[i].Label = items[i].name + " ( " + Math.Round(percentage, 2).ToString() + "%/" + items[i].rarity + " )";
                dataPieChart.Series[0].Points[i].AxisLabel = (i + 1).ToString();
            }
            percentageTestLbl.Text = "Alle zusammen: " + allPercentage.ToString() + "% (" + Math.Abs((double)(100.0 - allPercentage)).ToString() + "% Berechnungsfehler)";
            setRarityAnnotations(items);
        }

        /// <summary>
        ///     Teilt das Diagramm in Abschnitte ein (benutzt <see cref="Config"/> Daten!)
        /// </summary>
        /// <param name="items">Itemliste, die momentan im Diagramm verwendet wird</param>
        private void setRarityAnnotations(List<Loot> items)
        {
            dataPieChart.ChartAreas[0].AxisX.CustomLabels.Clear();
            int aktRar = 0, lastRar = aktRar;
            int lastBreak = items.Count -1;
            int labels = 0;

            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (i != 0)
                {
                    //Schleife zum ermitteln der ggf. geänderten mometanen Rarity
                    while (!((items[i].rarity <= rarBounds[aktRar]) && (items[i].rarity >= rarBounds[aktRar+1]))) 
                    {
                        aktRar++;
                    }
                    //Bei einerÄnderung wird der Letzte Abschnitt abgeschlossen
                    if (aktRar != lastRar)
                    {
                        dataPieChart.ChartAreas[0].AxisX.CustomLabels.Add(new System.Windows.Forms.DataVisualization.Charting.CustomLabel(i + 1, lastBreak, rarNames[aktRar - 1], 1, System.Windows.Forms.DataVisualization.Charting.LabelMarkStyle.LineSideMark));
                        dataPieChart.ChartAreas[0].AxisX.CustomLabels[dataPieChart.ChartAreas[0].AxisX.CustomLabels.Count - 1].ToolTip = rarNames[aktRar - 1];
                        dataPieChart.ChartAreas[0].AxisX.CustomLabels[labels].ForeColor = rarCol[aktRar-1];
                        dataPieChart.ChartAreas[0].AxisX.CustomLabels[labels].MarkColor = rarCol[aktRar-1];
                        lastRar = aktRar;
                        lastBreak = i;
                        labels++;
                    }
                }
                //bei 0 ist auch der letzte Abschnitt automatisch abgeschlossen
                else
                {
                    aktRar++;
                    dataPieChart.ChartAreas[0].AxisX.CustomLabels.Add(new System.Windows.Forms.DataVisualization.Charting.CustomLabel(i, lastBreak, rarNames[aktRar - 1], 1, System.Windows.Forms.DataVisualization.Charting.LabelMarkStyle.LineSideMark));
                    dataPieChart.ChartAreas[0].AxisX.CustomLabels[dataPieChart.ChartAreas[0].AxisX.CustomLabels.Count - 1].ToolTip = rarNames[aktRar - 1];
                    dataPieChart.ChartAreas[0].AxisX.CustomLabels[labels].ForeColor = rarCol[aktRar-1];
                    dataPieChart.ChartAreas[0].AxisX.CustomLabels[labels].MarkColor = rarCol[aktRar-1];
                }
            }
        }

        /// <summary>
        ///     ändert über mehrere Aufrufe hinweg die Farbkomponenten <see cref="R"/>,<see cref="G"/>,<see cref="B"/>
        /// </summary>
        /// <param name="i">maximale Änderung pro Komponente</param>
        /// <param name="trans">Alph- Wert</param>
        /// <returns><see cref="Color.FromArgb(int)-Wert aus den 4 Parametern"/></returns>
        private Color farbVerlauf(int i,int trans)
        {
            Color e;

            R += randomizer.Next(0, i + 1);
            G += randomizer.Next(0, i + 1);
            B += randomizer.Next(0, i + 1);

            int RR = (int)((Math.Sin(Math.PI / 255 * R) + 1) / 2 * 102 + 48);
            int GG = (int)((Math.Sin(Math.PI / 255 * G) + 1) / 2 * 102 + 48);
            int BB = (int)((Math.Sin(Math.PI / 255 * B) + 1) / 2 * 102 + 48);

            e = Color.FromArgb(trans, RR, GG, BB);

            return e;
        }
    }
}
