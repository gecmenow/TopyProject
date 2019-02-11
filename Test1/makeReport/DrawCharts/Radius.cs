using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

using Test1.Model;

namespace Test1.makeReport.DrawCharts
{
    class Radius
    {
        public void RadiusVolumeChart(GeneralReport genReport)
        {
            try
            {
                genReport.RadiusVolume = genReport.RadiusVolume.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value);

                Form1._Form1.lineChart.ChartAreas[0].AxisX.Minimum = genReport.RadiusVolume.Select(k => k.Key).Min();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Maximum = genReport.RadiusVolume.Select(k => k.Key).Max();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.MajorGrid.Interval = 2.5;
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Title = "Radius,mm";
                Form1._Form1.lineChart.ChartAreas[0].AxisY.Title = "Volume, %";
                Form1._Form1.lineChart.Series[0].Points.DataBindXY(genReport.RadiusVolume.Keys, genReport.RadiusVolume.Values);
                Form1._Form1.lineChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                string file;

                var str = new StringBuilder();
                str.Append("(Volume)");
                str.Append("Radius Chart");
                str.Append(".png");

                file = str.ToString();

                str.Clear();

                if (File.Exists(genReport.folderName + @"\" + file))
                {
                    int numberfile = 0;
                    string filename = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);

                    Match regex = Regex.Match(file, @"(.+) \((\d+)\)\.\w+");

                    if (regex.Success)
                    {
                        filename = regex.Groups[1].Value;
                        numberfile = int.Parse(regex.Groups[2].Value);
                    }

                    do
                    {
                        numberfile++;
                        file = Path.Combine(string.Format("{0} ({1}) {2}", filename, numberfile, extension));
                    }
                    while (File.Exists(genReport.folderName + @"\" + file));
                }

                Form1._Form1.lineChart.SaveImage(genReport.folderName + @"\" + file, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                genReport.FrictionVolume.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Radius (Volume) General report:" + e.Message);
            }
        }

        public void RadiusRFChart(GeneralReport genReport)
        {
            try
            {
                genReport.RadiusReactionForce = genReport.RadiusReactionForce.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value * Math.Pow(10, -6));

                Form1._Form1.lineChart.ChartAreas[0].AxisX.Minimum = genReport.RadiusReactionForce.Select(k => k.Key).Min();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Maximum = genReport.RadiusReactionForce.Select(k => k.Key).Max();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.MajorGrid.Interval = 2.5;
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Title = "Radius, mm";
                Form1._Form1.lineChart.ChartAreas[0].AxisY.Title = "Force, MN";
                Form1._Form1.lineChart.Series[0].Points.DataBindXY(genReport.RadiusReactionForce.Keys, genReport.RadiusReactionForce.Values);
                Form1._Form1.lineChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                string file;

                var str = new StringBuilder();
                str.Append("(RF)");
                str.Append("Radius Chart");
                str.Append(".png");

                file = str.ToString();

                str.Clear();

                if (File.Exists(genReport.folderName + @"\" + file))
                {
                    int numberfile = 0;
                    string filename = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);

                    Match regex = Regex.Match(file, @"(.+) \((\d+)\)\.\w+");

                    if (regex.Success)
                    {
                        filename = regex.Groups[1].Value;
                        numberfile = int.Parse(regex.Groups[2].Value);
                    }

                    do
                    {
                        numberfile++;
                        file = Path.Combine(string.Format("{0} ({1}) {2}", filename, numberfile, extension));
                    }
                    while (File.Exists(genReport.folderName + @"\" + file));
                }

                Form1._Form1.lineChart.SaveImage(genReport.folderName + @"\" + file, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                genReport.FrictionReactionForce.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Radius RF General report:" + e.Message);
            }
        }
    }
}
