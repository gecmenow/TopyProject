using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Test1.Model;
using Test1.Files;
using Test1.Extract_Data;

namespace Test1.makeReport
{
    class ReactionForceReport
    {
        public void ReportReactionForce(GeometryOptions model,GeneralReport genReport, FileName file, ExtractRF extrRF)
        {
            double RF = Math.Round(extrRF.getRF(), 1);

            model.reactionForceToBlank = RF;

            String header = String.Join(
                            Environment.NewLine,
                              "Radius,mm" + ";" 
                            + "Angle,degrees" + ";"
                            + "Friction coeff" + ";"
                            + "Force, MN" + ";" 
                            + "Date" + ";" 
                            + Environment.NewLine);

            String csv = String.Join(
                         Environment.NewLine,
                         (model.blankRadius + ";"
                        + model.stampAngle + ";"
                        + model.friction + ";"
                        + (model.reactionForceToBlank / 1000000) + ";"
                        + DateTime.Now + ";"));

            if (!Directory.Exists(genReport.folderName))
                Directory.CreateDirectory(genReport.folderName);

            string path = Path.Combine(genReport.folderName, file.RFReport);

            if (!File.Exists(path))
                File.WriteAllText(path, header);

            File.AppendAllText(path, csv + Environment.NewLine);
        }
    }
}
