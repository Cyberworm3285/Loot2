namespace Loot2
{
    partial class popUpDia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataPieChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.percentageTestLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataPieChart)).BeginInit();
            this.SuspendLayout();
            // 
            // dataPieChart
            // 
            this.dataPieChart.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX2.MajorGrid.Enabled = false;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.ScrollBar.BackColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.dataPieChart.ChartAreas.Add(chartArea1);
            this.dataPieChart.Location = new System.Drawing.Point(12, 12);
            this.dataPieChart.Name = "dataPieChart";
            this.dataPieChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.Color = System.Drawing.Color.Black;
            series1.Name = "Series1";
            series1.SmartLabelStyle.IsOverlappedHidden = false;
            series1.YValuesPerPoint = 4;
            this.dataPieChart.Series.Add(series1);
            this.dataPieChart.Size = new System.Drawing.Size(998, 596);
            this.dataPieChart.TabIndex = 0;
            this.dataPieChart.Text = "chart1";
            // 
            // percentageTestLbl
            // 
            this.percentageTestLbl.AutoSize = true;
            this.percentageTestLbl.Location = new System.Drawing.Point(12, 611);
            this.percentageTestLbl.Name = "percentageTestLbl";
            this.percentageTestLbl.Size = new System.Drawing.Size(0, 13);
            this.percentageTestLbl.TabIndex = 1;
            // 
            // popUpDia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1022, 635);
            this.Controls.Add(this.percentageTestLbl);
            this.Controls.Add(this.dataPieChart);
            this.Name = "popUpDia";
            this.Text = "Loot Diagramm";
            ((System.ComponentModel.ISupportInitialize)(this.dataPieChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart dataPieChart;
        private System.Windows.Forms.Label percentageTestLbl;
    }
}