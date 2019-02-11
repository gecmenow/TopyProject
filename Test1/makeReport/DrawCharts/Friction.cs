using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Test1.Model;

namespace Test1.makeReport.DrawCharts
{
    class Friction
    {
        public void FrictionVolumeChart(GeneralReport genReport)
        {
            try
            {
                genReport.FrictionVolume = genReport.FrictionVolume.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value);

                Form1._Form1.lineChart.ChartAreas[0].AxisX.Minimum = genReport.FrictionVolume.Select(k => k.Key).Min();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Maximum = genReport.FrictionVolume.Select(k => k.Key).Max();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.MajorGrid.Interval = 2.5;
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Title = "Friction";
                Form1._Form1.lineChart.ChartAreas[0].AxisY.Title = "Volume, %";
                Form1._Form1.lineChart.Series[0].Points.DataBindXY(genReport.FrictionVolume.Keys, genReport.FrictionVolume.Values);
                Form1._Form1.lineChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                string file;

                var str = new StringBuilder();
                str.Append("(Volume)");
                str.Append("Friction Chart");
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
                        file = Path.Combine(string.Format("{0} ({1}){2}", filename, numberfile, extension));
                    }
                    while (File.Exists(genReport.folderName + @"\" + file));
                }

                Form1._Form1.lineChart.SaveImage(genReport.folderName + @"\" + file, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                genReport.FrictionVolume.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Friction (Volume) General report:" + e.Message);
            }
        }

        public void FrictionRFChart(GeneralReport genReport)
        {
            try
            {
                genReport.FrictionReactionForce = genReport.FrictionReactionForce.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value * Math.Pow(10, -6));

                Form1._Form1.lineChart.ChartAreas[0].AxisX.Minimum = genReport.FrictionReactionForce.Select(k => k.Key).Min();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Maximum = genReport.FrictionReactionForce.Select(k => k.Key).Max();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.MajorGrid.Interval = 2.5;
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Title = "Friction";
                Form1._Form1.lineChart.ChartAreas[0].AxisY.Title = "Force, MN";
                Form1._Form1.lineChart.Series[0].Points.DataBindXY(genReport.FrictionReactionForce.Keys, genReport.FrictionReactionForce.Values);
                Form1._Form1.lineChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                string file;

                var str = new StringBuilder();
                str.Append("(RF)");
                str.Append("Friction Chart");
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
                        file = Path.Combine(string.Format("{0} ({1}){2}", filename, numberfile, extension));
                    }
                    while (File.Exists(genReport.folderName + @"\" + file));
                }

                Form1._Form1.lineChart.SaveImage(genReport.folderName + @"\" + file, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                genReport.FrictionReactionForce.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Friction RF General report:" + e.Message);
            }
        }
    }
}
