using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Test1.Model;

namespace Test1.makeReport
{
    class VariablesForReport
    {
        public double Radius { get; set; }
        public double Angle { get; set; }
        public double Friction { get; set; }
        public double VolumePct { get; set; }
        public double ReactionForce { get; set; }

        public VariablesForReport(double radius, double angle, double friction, double volumePCT, double reactionForce)
        {
            this.Radius = radius;
            this.Angle = angle;
            this.Friction = friction;
            this.VolumePct = volumePCT;
            this.ReactionForce = reactionForce;
        }
    }

    class CommonReport
    {
        public List<VariablesForReport> minVolume = new List<VariablesForReport>();

        public string finalReportFolder;
        public string finalReportFile;

        public void AddData(GeometryOptions model)
        {
            minVolume.Add(new VariablesForReport(model.blankRadius, model.stampAngle,
                    model.friction, model.pctOfDieFilling, model.reactionForceToBlank));
        }

        public void makeFinalReport(double[,] array)
        {
            List<VariablesForReport> forListCleaning = new List<VariablesForReport>();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                forListCleaning.Add(new VariablesForReport(Math.Round(array[i, 0], 2), 0, Math.Round(array[i, 1], 2), 0, 0));
            }

            var temp = minVolume
                .Where(p2 => forListCleaning
                .Any(p1 => p1.Radius == p2.Radius && p1.Friction == p2.Friction))
                .ToList();

            minVolume = temp.ToList();

            double min = minVolume.Min(v => v.ReactionForce);

            String header = String.Join(
                            Environment.NewLine,
                                "Radius" + ";"
                                + "Angle" + ";"
                                + "Friction" + ";"
                                + "DieFilling, %" + ";"
                                + "Force, MN" + ";"
                                + "Data" + ";"
                                + Environment.NewLine);

            String csv = String.Join(
                            Environment.NewLine,
                            minVolume.Where(v => v.ReactionForce == min).Select(d => d.Radius + ";"
                                            + d.Angle + ";"
                                            + d.Friction + ";"
                                            + d.VolumePct + ";"
                                            + (d.ReactionForce * Math.Pow(10,-6)) + ";"
                                            + DateTime.Now + ";").First());

            if (!Directory.Exists(finalReportFolder))
            {
                Directory.CreateDirectory(finalReportFolder);
            }
            string path = Path.Combine(finalReportFolder, finalReportFile);

            if (!File.Exists(path))
            {
                File.WriteAllText(path, header);
            }

            File.AppendAllText(path, csv + Environment.NewLine);
        }
    }
}
