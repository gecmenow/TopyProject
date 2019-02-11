using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test1.Rout;
using System.IO;

namespace Test1.Volume
{
    //class NodeDisplacement
    //{
    //    Dictionary<double, double> Displacement = new Dictionary<double, double>();
    //    double NodeDisplacementValue;

    //    public void GetDisplacement(FilesForVolume filesForVolume)
    //    {
    //        Displacement = File.ReadLines(filesForVolume.setNodeDisplacementRoute).Select(line => line.Split(';')).ToDictionary(data => Convert.ToDouble(data[0]), data => Convert.ToDouble(data[1]));
    //        Displacement = Displacement
    //        .Where(f => f.Value != 0)
    //        .ToDictionary(x => x.Key, x => x.Value);

    //        NodeDisplacementValue = Displacement.Values.Last();

    //        File.WriteAllText("NodeDisplacement.txt", Convert.ToString(NodeDisplacementValue));
    //    }
    //}
}
