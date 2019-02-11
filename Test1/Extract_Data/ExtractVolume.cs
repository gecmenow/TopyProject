using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Test1.Rout;
using Test1.Volume;
using Test1.Model;
using Test1.makeReport;

namespace Test1.Extract_Data
{
    class BlanksNodeCoordinates
    {
        public double X;
        public double Y;
        public double Z;

        public BlanksNodeCoordinates(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    class ExtractVolume
    {
        // Словарь координат узлов заготовки, после деформации
        Dictionary<double, BlanksNodeCoordinates> BlanksNodeDisplacementDic = new Dictionary<double, BlanksNodeCoordinates>();

        public void GetBlankNodesDisplacement(FilesForVolume filesForVolume)
        {
            BlanksNodeDisplacementDic = File.ReadLines(filesForVolume.blankNodeDisplacementRoute)
                        .Select(line => line.Split(';'))
                        .Where(split => split[0] != "Node")
                        .ToDictionary(split => double.Parse(split[0]),
                                      split => new BlanksNodeCoordinates(double.Parse(split[1]), double.Parse(split[2]), double.Parse(split[3])));
            
            #region Проверка, путём записис в  файл, что в словаре имеются правильные узлы с координатами
            
            String NodeInStampCSV = String.Join(
                Environment.NewLine,
                BlanksNodeDisplacementDic.Select(d => d.Key + ";" + d.Value.X + ";" + d.Value.Y + ";" + d.Value.Z + ";"));

            File.WriteAllText("BlanksNodeDisplacementDic.csv", NodeInStampCSV);
            #endregion
        }

        double _volume;

        public double getVolume()
        { return _volume; }

        public void Volume(GeometryOptions model)
        {
            double nodeSum = BlanksNodeDisplacementDic.Max(v => v.Value.Z) 
                           + Math.Abs(BlanksNodeDisplacementDic.Min(v => v.Value.Z));

            double delta;
            double lowerRadius;

            double tempStampVolume;

            if (nodeSum >= 60.8)
                _volume = model.dieVolume;

            else
            {
                delta = 61 - nodeSum;
                lowerRadius = model.lowerFilletRadius - ((delta / 61) * (model.lowerFilletRadius - 10));

                tempStampVolume = (Math.PI * delta * (Math.Pow(lowerRadius, 2) + lowerRadius * 10 + Math.Pow(10, 2)) / 3);

                _volume = model.dieVolume - tempStampVolume;
            }
        }
    }
}
