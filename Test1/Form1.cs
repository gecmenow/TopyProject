using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web;
using System.Linq;

//Подключение классов из других папок
using Test1.runPrograms;
using Test1.Model;
using Test1.Materials;
using Test1.Additionally;
using Test1.Extract_Data;
using Test1.Rout;
using Test1.Volume;
using Test1.makeReport;
using Test1.Files;
using Test1.OptimizationAlgorithm.SimplexPlanning;
using Test1.ExtraPage;
using Test1.Check;

namespace Test1
{
    public partial class Form1 : Form
    {
        public static Form1 _Form1;

        GeometryOptions model = new GeometryOptions();
        FileName file = new FileName();
        MaterialsOptions material = new MaterialsOptions();
        AdditionallyOptions add = new AdditionallyOptions();
        ExtraClass extra = new ExtraClass();
        CheckFields check = new CheckFields();
        CommonReport rpr = new CommonReport();
        ArgumentsCMD cmd = new ArgumentsCMD();
        Routes rout = new Routes();
        FilesForVolume filesForVolume = new FilesForVolume();

        public Form1()
        {
            InitializeComponent();
            _Form1 = this;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check.Check())
            {
                return;
            }

            if (check.CheckMassDensity())
            {
                return;
            }

            if (check.CheckJobName())
            {
                return;
            }

            if (check.CheckDGV())
            {
                return;
            }

            ReadFormData();

            FileNames();

            RunProgram();
                
        }

        #region Считывание данных с формы

        public void ReadFormData()
        {
            model.startBlankRadius = Convert.ToDouble(blankRadius.Text);
            material.materialDensity = double.Parse(massDensity.Text, NumberStyles.Any);
            material.youngsModulus = Convert.ToDouble(YoungsModulus.Text);
            material.poissonsRatio = Convert.ToDouble(PoissonsRatio.Text);
            model.startFriction = Convert.ToDouble(startFriction.Text);
            model.endFriction = Convert.ToDouble(endFriction.Text);

            model.startAngle = Convert.ToInt32(strtAngle.Text);//начальный угол в матрице
            model.endAngle = Convert.ToInt32(lastAngle.Text);//конечный угол в матрице
            model.stepAngle = Convert.ToInt32(angleStep.Text);//шаг, на который он меняется

            add.mesh = Convert.ToDouble(Mesh.Text);
            add.numberOfCores = Convert.ToInt32(Cores.Text);
            add.jobName = JobName.Text;

            material.setPlastic(PlasticData);
        }

        public void FileNames()
        {
            file.volumeReport = "Volume.csv";
            file.RFReport = "ReactionForce.csv";

            rpr.finalReportFolder = "Report";
            rpr.finalReportFile = "Report.csv";

            file.stampName = "Stamp.IGS";
            file.blankName = "Blank.IGS";
            file.platformName = "Platform.IGS";

            cmd.runAbaqus = "abaqus cae script noGUI=abaqusMacros.py";
            cmd.extractName = "abaqus cae script noGUI=name.py";
            cmd.extractFiles = "abaqus cae script noGUI=extract.py";
            cmd.extractNodes = "abaqus cae script noGUI=node.py";

            rout.cmdEXE = Environment.GetFolderPath(Environment.SpecialFolder.System);
            rout.systemDisk = Environment.SystemDirectory.Substring(0, 1) + " ";

            file.extractFileName = "extract.py";
            file.namePYFile = "name.py";
            file.nodePYFile = "node.py";

            rout.historyRegionsNameFile = @"\outputs.txt";

            //filesForVolume.stampNodeDisplacementName = @"\U3Stamp.csv";
            filesForVolume.blankNodeDisplacementName = @"\COORDOutput.csv";
            //filesForVolume.blankCoordinatesName = @"\blankNodes.csv";
            //filesForVolume.elementCoordinatesName = @"\blankElementsCoordinates.csv";
            //filesForVolume.stampNodesName = @"\stampCoordinates.csv";
            //filesForVolume.elementVolumeName = @"\EVOLOutput.csv";
        }

        public void RunProgram()
        {
            SimplexPlanning simplex = new SimplexPlanning(model, material, add, file, rpr,rout, cmd, filesForVolume);
            simplex.Optimization();

            //GoldenRatioMethod goldenRatio = new GoldenRatioMethod();
            //goldenRatio.Optimization(model, material, add, file, rpr);

            //var perebor = new Perebor();
            //perebor.Optimization(model, material, add, file, rpr);
        }

        #endregion


        #region Log area

        public void LogArea(string text)
        {
            Log.AppendText(text);
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            Mesh.Visible = !Mesh.Visible;
            Cores.Visible = !Cores.Visible;
            JobName.Visible = !JobName.Visible;

            label10.Visible = !label10.Visible;
            label11.Visible = !label11.Visible;
            label12.Visible = !label12.Visible;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            extra.LoadMaterialData();
        }

        private void ExtraMaterialsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExtraTextBox.Text = ExtraMaterialsList.Text;
        }

        private void MaterialsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var materials = new MaterialsOptions();
                string filename = MaterialsList.Text;

                string path = @"Materials\" + filename + ".csv";

                materials.MaterialData = File.ReadLines(path)
                                        .Select(line => line.Split(';'))
                                        .ToDictionary(data => Convert.ToString(data[0]), data => Convert.ToString(data[1]));

                var _priceDataArray = from row in materials.MaterialData select new { Yield_Stress = row.Key, Plastic_Strain = row.Value };

                PlasticData.DataSource = _priceDataArray.ToArray();
            }
            catch (Exception exp)
            {
                MessageBox.Show(Convert.ToString(exp.Message));
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string path = @"Materials\MaterialName.txt";

            string[] temp = File.ReadAllLines(path);

            if (check.CheckExtraFields())
            { return; }

            extra.SaveToCSV(ExtraDataGridView);

            File.AppendAllText(path, ExtraTextBox.Text + Environment.NewLine);

            extra.RefreshCombobox();
        }

        private void ExtraTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ExtraTextBox.Text != "")
            {
                ExtraTextBox.BackColor = Color.Empty;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            extra.Remove();
        }
    }
}
