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
    class Angle
    {
        public void AngleVolumeChart(GeneralReport genReport)
        {
            try
            {
                genReport.AngleVolume = genReport.AngleVolume.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value);

                Form1._Form1.lineChart.ChartAreas[0].AxisX.Minimum = genReport.AngleVolume.Select(k => k.Key).First();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Maximum = genReport.AngleVolume.Select(k => k.Key).Last();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.MajorGrid.Interval = 2.5;
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Title = "Angle";
                Form1._Form1.lineChart.ChartAreas[0].AxisY.Title = "Volume, %";
                Form1._Form1.lineChart.Series[0].Points.DataBindXY(genReport.AngleVolume.Keys, genReport.AngleVolume.Values);
                Form1._Form1.lineChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                string file;

                var str = new StringBuilder();
                str.Append("(Volume)");
                str.Append("radius");
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

                genReport.AngleVolume.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Angle (Volume) General report:" + e.Message);
            }
        }

        public void AngleRFChart(GeneralReport genReport)
        {
            try
            {
                genReport.AngleReactionForce = genReport.AngleReactionForce.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value);

                Form1._Form1.lineChart.ChartAreas[0].AxisX.Minimum = genReport.AngleReactionForce.Select(k => k.Key).First();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Maximum = genReport.AngleReactionForce.Select(k => k.Key).Last();
                Form1._Form1.lineChart.ChartAreas[0].AxisX.MajorGrid.Interval = 2.5;
                Form1._Form1.lineChart.ChartAreas[0].AxisX.Title = "Angle";
                Form1._Form1.lineChart.ChartAreas[0].AxisY.Title = "Force, kN";
                Form1._Form1.lineChart.Series[0].Points.DataBindXY(genReport.AngleReactionForce.Keys, genReport.AngleReactionForce.Values);
                Form1._Form1.lineChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                string file;

                var str = new StringBuilder();
                str.Append("(RF)");
                str.Append("radius");
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

                genReport.AngleReactionForce.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Angle RF General report:" + e.Message);
            }
        }
    }
}
