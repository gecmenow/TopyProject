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

namespace Test1.Extract_Data.OLD
{
    class BlanksNodeCoordinates
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public BlanksNodeCoordinates(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    class BlanksNodeDisplacement
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public BlanksNodeDisplacement(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    class BlankCoordinates
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public BlankCoordinates(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }


    class ElementNodes
    {
        public int Node1 { get; set; }
        public int Node2 { get; set; }
        public int Node3 { get; set; }
        public int Node4 { get; set; }
        public int Node5 { get; set; }
        public int Node6 { get; set; }
        public int Node7 { get; set; }
        public int Node8 { get; set; }

        public ElementNodes(int node1, int node2, int node3, int node4, int node5, int node6, int node7, int node8)
        {
            this.Node1 = node1;
            this.Node2 = node2;
            this.Node3 = node3;
            this.Node4 = node4;
            this.Node5 = node5;
            this.Node6 = node6;
            this.Node7 = node7;
            this.Node8 = node8;
        }
    }

    class NodesInStamp
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public NodesInStamp(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    class ExtractVolumeOLD
    {
        // Штамп = матрица
        // Словарь координат узлов заготовки
        Dictionary<double, BlanksNodeCoordinates> BlanksNodeCoordinatesDic = new Dictionary<double, BlanksNodeCoordinates>();

        Dictionary<double, BlanksNodeCoordinates> BlankCoordinatesDic = new Dictionary<double, BlanksNodeCoordinates>();
        // Словарь номера узлов и их перемещение, после деформации
        Dictionary<double, BlanksNodeCoordinates> BlanksNodeDisplacementDic = new Dictionary<double, BlanksNodeCoordinates>();
        // Словарь перемещения точки на штампе, относительно времени,
        Dictionary<double, double> StampNodesDisplacement = new Dictionary<double, double>();
        // Объём каждого элемента в заготовке
        Dictionary<int, double> ElementVolume = new Dictionary<int, double>();
        // Номера узлов для каждого элемента
        Dictionary<int, ElementNodes> ElementCoordinates = new Dictionary<int, ElementNodes>();

        // Номера узлов, которые вошли в штамп, первый параметр номер элемента, второй не учитывается, сделан немного криво, а вообще это первый узел Node1
        Dictionary<int, int> ElementNumber = new Dictionary<int, int>();

        // На сколько едениц переместилась точка штампа
        double NodeDisplacement;

        public void GetStampNodesDisplacement(FilesForVolume filesForVolume)
        {
            StampNodesDisplacement = File.ReadLines(filesForVolume.stampNodeDisplacementRoute)
                                    .Select(line => line.Split(';'))
                                    .ToDictionary(data => Convert.ToDouble(data[0]), data => Convert.ToDouble(data[1]));
            StampNodesDisplacement = StampNodesDisplacement
            .Where(f => f.Value != 0)
            .ToDictionary(x => x.Key, x => x.Value);

            NodeDisplacement = StampNodesDisplacement.Values.Last();

            File.WriteAllText("StampNodeDisplacement.txt", Convert.ToString(NodeDisplacement));
        }

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

        public void GetBlanksNodeCoordinates(FilesForVolume filesForVolume)
        {
            //правильный вариант текущих координат заготовки 
            BlankCoordinatesDic = BlanksNodeDisplacementDic.ToDictionary(k => k.Key, v => v.Value);
            //new
            double min = BlankCoordinatesDic.Min(v => v.Value.Z);

            min = Math.Abs(min);

            NodeDisplacement += min;

            //new
            BlankCoordinatesDic = BlankCoordinatesDic.ToDictionary(k => k.Key, v => new BlanksNodeCoordinates(v.Value.X, v.Value.Y, v.Value.Z + min));

            #region Проверка, путём записис в  файл, что в словаре имеются правильные узлы с координатами
            String BlankCoordDic = String.Join(
                Environment.NewLine,
                BlankCoordinatesDic.Select(d => d.Key + ";" + d.Value.X + ";" + d.Value.Y + ";" + d.Value.Z + ";"));

            File.WriteAllText("BlankCoordinatesDic.csv", BlankCoordDic);
            #endregion
        }

        public void GetElementCoordinates(FilesForVolume filesForVolume, GeometryOptions model)
        {
            try
            {
                // Новый список координат узлов заготовки, координаты которых меньше координаты перемещения штампа из списка координат узлов выше ^
                Dictionary<double, NodesInStamp> NodesInStampDic = new Dictionary<double, NodesInStamp>();

                Dictionary<int, double> radius = new Dictionary<int, double>();

                Dictionary<int, double> StampNodes = new Dictionary<int, double>();

                //double radius;

                ElementCoordinates = File.ReadLines(filesForVolume.elementCoordinatesRoute)
                                    .Skip(1)
                                    .Select(line => line.Split(';'))
                                    .ToDictionary(split => int.Parse(split[0]),
                                    split => new ElementNodes(int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]), int.Parse(split[4]), int.Parse(split[5]), int.Parse(split[6]), int.Parse(split[7]), int.Parse(split[8])));
                //пишем знак меньше, так как координаты увеличиваются к основанию заготовки (от меньшего к большему)
                //следовательно, для подсчёта объема в матрице, нам нужно то, что не больше перемещения матрицы

                NodesInStampDic = BlankCoordinatesDic.Where(f => f.Value.Z <= NodeDisplacement).ToDictionary(k => k.Key, v => new NodesInStamp(v.Value.X, v.Value.Y, v.Value.Z));

                //Новый отбор точек.
                //Отбираются точки заготовки, которые меньше радиуса, после скругления
                //Т.к. подсчёт объёма идёт от пуансона до конца матрицы и мы прсото заносим в этот словарь
                //ведь он используется дальше и чтобы не менять структуру проги
                NodesInStampDic = BlankCoordinatesDic.ToDictionary(k => k.Key, v => new NodesInStamp(v.Value.X, v.Value.Y, v.Value.Z));

                #region Проверка, путём записис в  файл, что в словаре имеются правильные узлы, находящиеся в матрице, с координатами
                String NodeInStampCSV = String.Join(
                    Environment.NewLine,
                    NodesInStampDic.Select(d => d.Key + ";" + d.Value.X + ";" + d.Value.Y + ";" + d.Value.Z + ";"));

                File.WriteAllText("NodesInStamp.csv", NodeInStampCSV);
                #endregion

                StampNodes = File.ReadLines(filesForVolume.stampNodesRoute)
                                    .Select(line => line.Split(';'))
                                    .ToDictionary(key => Convert.ToInt32(key[0]), value => Math.Abs(Convert.ToDouble(value[3])));

                #region Проверка, путём записис в  файл, что в словаре имеются правильные соотнешения узла и его координаты Z (считается радиусом матрицы)
                String StampNodesCSV = String.Join(
                    Environment.NewLine,
                    StampNodes.Select(d => d.Key + ";" + d.Value + ";"));

                File.WriteAllText("StampNodes.csv", StampNodesCSV);
                #endregion

                double stampRadius = model.dieRadius;
                radius = NodesInStampDic.ToDictionary(k => Convert.ToInt32(k.Key), v => Math.Sqrt(Math.Pow(v.Value.X, 2) + Math.Pow(v.Value.Y, 2)));

                //добавить сраненние радиусов
                //Dictionary<double, double> stampDeltaRadius = new Dictionary<double, double>();
                //stampDeltaRadius = NodesInStampDic.ToDictionary(k =>k.Key, v=> model.dieRadius - ((v.Value.Z/60) * (model.dieRadius - 10)));

                //radius = radius.Where(v => stampDeltaRadius[v.Key] <= v.Value).ToDictionary(k => k.Key, v => v.Value);
                //старый вариант отбора радиусов
                //radius = radius.Where(v => v.Value <= stampRadius).ToDictionary(k => k.Key, v => v.Value);
                //test
                radius = radius.Where(v => v.Value <= model.lowerFilletRadius).ToDictionary(k => k.Key, v => v.Value);

                String csvradius = String.Join(
                   Environment.NewLine,
                   radius.Select(d => d.Key + ";" + d.Value + ";"));

                File.WriteAllText("Radius.csv", csvradius);

                double minNodeNumber = radius.Min(x => x.Key);

                File.WriteAllText("minNodeNumber.txt", Convert.ToString(minNodeNumber));

                ElementNumber = ElementCoordinates
                                .Where(v => radius.Keys.Contains(v.Value.Node1)
                                        && radius.Keys.Contains(v.Value.Node2)
                                        && radius.Keys.Contains(v.Value.Node3)
                                        && radius.Keys.Contains(v.Value.Node4)
                                        && radius.Keys.Contains(v.Value.Node5)
                                        && radius.Keys.Contains(v.Value.Node6)
                                        && radius.Keys.Contains(v.Value.Node7)
                                        && radius.Keys.Contains(v.Value.Node8))
                                .ToDictionary(keyValue => keyValue.Key, keyValue => keyValue.Value.Node1);

                String csv = String.Join(
                    Environment.NewLine,
                    ElementNumber.Select(d => d.Key + ";" + d.Value + ";"));

                File.WriteAllText("ElementNumbers.csv", csv);

            }
            catch (Exception e)
            {
                MessageBox.Show("Get Elemnt Coordinates: " + e);
            }
            //string test = ElementCoord.FirstOrDefault(x=>x.Key == 1).Value;

        }

        public void Volume(FilesForVolume filesForVolume, GeometryOptions model)
        {

            //Верменная переменная для подсчёта объёма
            Dictionary<double, double> array = new Dictionary<double, double>();

            ElementVolume = File.ReadLines(filesForVolume.elementVolumeRoute).Select(line => line.Split(';')).ToDictionary(data => Convert.ToInt32(data[0]), data => Convert.ToDouble(data[1]));
            ElementVolume = ElementVolume
            .Where(f => f.Value != 0)
            .ToDictionary(x => x.Key, x => x.Value);

            ElementVolume = ElementVolume.Keys.Intersect(ElementNumber.Keys).ToDictionary(t => t, t => ElementVolume[t]);

            double sum = ElementVolume.Sum(y => y.Value);

            array.Add(model.blankRadius, sum);

            String csv = String.Join(
                    Environment.NewLine,
                    array.Select(d => d.Key + ";" + d.Value));

            File.AppendAllText("TempVolume.csv", csv + Environment.NewLine);
        }
    }
}
