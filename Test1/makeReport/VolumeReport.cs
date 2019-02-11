using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Test1.Extract_Data;
using Test1.Model;
using Test1.Files;

namespace Test1.makeReport
{
    class VolumeReport
    {
        public void ReportVolume(GeometryOptions model, GeneralReport genReport, FileName file, ExtractVolume extrVolume)
        {
            try
            {
                model.blankVolume = Convert.ToDouble(extrVolume.getVolume());
                model.blankVolume = Math.Round(model.blankVolume,1);
                model.pctOfDieFilling = (model.blankVolume / model.dieVolume) * 100;
                model.pctOfDieFilling = Math.Round(model.pctOfDieFilling, 1);

                String header = String.Join(
                                Environment.NewLine,
                                  "Radius, mm" + ";" 
                                + "Angle, degrees" + ";"
                                + "Friction coeff" + ";"
                                + "Volume, mm^3" + ";" 
                                + "Die Filling, %" + ";" 
                                + "Date" + ";" 
                                + Environment.NewLine);

                String csv = String.Join(
                             Environment.NewLine,
                             (model.blankRadius + ";"
                            + model.stampAngle + ";"
                            + model.friction + ";"
                            + model.blankVolume + ";"
                            + model.pctOfDieFilling + ";"
                            + DateTime.Now + ";"));

                if (!Directory.Exists(genReport.folderName))
                    Directory.CreateDirectory(genReport.folderName);

                string path = Path.Combine(genReport.folderName, file.volumeReport);

                if (!File.Exists(path))
                    File.WriteAllText(path, header);

                File.AppendAllText(path, csv + Environment.NewLine);
            }
            catch (Exception e)
            {
                MessageBox.Show("Volume Report :" + e);
            }
        }
    }
}
