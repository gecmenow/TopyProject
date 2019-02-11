using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Test1.Model;
using Test1.Materials;
using Test1.makeReport;
using Test1.Files;
using Test1.Rout;
using Test1.runPrograms;
using Test1.Additionally;
using Test1.ChangeWords;
using Test1.Volume;
using Test1.Extract_Data;

namespace Test1.Function
{
    class MainFunction
    {
        GeometryOptions model = new GeometryOptions();
        MaterialsOptions material = new MaterialsOptions();
        GeneralReport genReport = new GeneralReport();
        FileName file = new FileName();
        Routes rout = new Routes();
        AdditionallyOptions add = new AdditionallyOptions();
        ArgumentsCMD cmd = new ArgumentsCMD();
        CommonReport finalReport = new CommonReport();
        FilesForVolume filesForVolume = new FilesForVolume();

        public MainFunction(GeometryOptions model, MaterialsOptions material, AdditionallyOptions add, 
            FileName file, CommonReport finalReport, GeneralReport genReport, Routes rout, ArgumentsCMD cmd, FilesForVolume filesForVolume)
        {
            this.model = model;
            this.material = material;
            this.add = add;
            this.file = file;
            this.finalReport = finalReport;
            this.genReport = genReport;
            this.rout = rout;
            this.cmd = cmd;
            this.filesForVolume = filesForVolume;
        }

        //MAIN FUNCTION
        public void Compute(double[,] array, int counterStart, int kForNodes)
        {
            //model.stampAngle = array[i, 0];
            //model.stampAngle = Math.Round(model.stampAngle);

            model.stampAngle = model.startAngle;

            model.blankRadius = array[counterStart, 0];
            model.blankRadius = Math.Round(model.blankRadius, 2);

            model.friction = array[counterStart, 1];
            model.friction = Math.Round(model.friction, 2);

            model.dieRadius = (Math.Tan(model.stampAngle * Math.PI / 180) * 60) + 10;
            model.dieRadius = Math.Round(model.dieRadius, 1);
            //объём усеченного конуса (верхний радиус 10, нижний может меняться) 60 - высота усеченного конуса
            model.dieVolume = (Math.PI * 60 * (Math.Pow(model.dieRadius, 2) + model.dieRadius * 10 + Math.Pow(10, 2))) / 3;

            #region необходимо для вычесления облоя

            //3 - радиус скругления, 90 - градус
            double filletLength = Math.Tan((180 - (((90 + model.stampAngle) / 2) + 90)) * Math.PI / 180) * 3;

            double upperRadius = model.dieRadius + filletLength + 10;

            double angle30X = upperRadius - 5;

            double lowerVolume = Math.Pow((angle30X - 5), 2) * Math.PI * 1;

            model.dieVolume += lowerVolume;

            double shave = Math.Pow(angle30X, 2) * Math.PI * 1;

            shave -= lowerVolume;

            model.dieVolume = Math.Round(model.dieVolume, 1);
            
            #endregion

            model.blankUpperRadius = model.blankRadius - (Math.Tan(model.stampAngle * Math.PI / 180) * 2);
            model.blankPosition = (Math.PI * 2 * (Math.Pow(model.blankRadius, 2) + model.blankRadius * (model.blankUpperRadius) + Math.Pow((model.blankUpperRadius), 2))) / 3;
            model.blankPosition = Math.Round(model.blankPosition, 2);

            //объем цилиндра, не считая фаску (как будто её нет,а заготовка имеет форму просто цилиндра)
            double abstractRadiusVolume = Math.PI * Math.Pow(model.blankRadius, 2) * 2;
            double blankVolume = model.dieVolume + (abstractRadiusVolume - model.blankPosition);

            //объём зазора между матрицей и площадкой
            blankVolume += shave;

            model.blankHeight = blankVolume / (Math.PI * Math.Pow(model.blankRadius, 2));
            model.blankHeight = Math.Round(model.blankHeight, 2);

            double radiusAfterLowerFillet = angle30X - 5;

            double radiusHeightInStamp;

            radiusHeightInStamp = 60 * ((radiusAfterLowerFillet - model.blankRadius) / (radiusAfterLowerFillet - 10));

            //необходимо для вычесления объёма от пуансона до конца матрицы
            model.lowerFilletRadius = radiusAfterLowerFillet;

            if (model.blankRadius > 10)
            {
                model.blankPosition = model.blankHeight - radiusHeightInStamp;
                model.blankPosition = Math.Round(model.blankPosition, 1);
                model.blankPosition += 5;
            }
            else
            {
                double tempHeight = model.blankHeight - 60;

                model.blankPosition = tempHeight * ((model.dieRadius - 10) / (model.dieRadius - 10));
                model.blankPosition = Math.Round(model.blankPosition, 1);
                //допуск +2мм на всякий пожарный случай
                model.blankPosition += 2;
            }

            model.stampDisplacement = model.blankPosition;
            //оставляем зазор 1мм
            model.stampDisplacement -= 1;

            #region Создание общей папки с название радиуса заготовки

            genReport.folderName = Convert.ToString(model.blankRadius);

            if (!Directory.Exists(genReport.folderName))
                Directory.CreateDirectory(genReport.folderName);

            #endregion

            string abaqusOutputFolder = Convert.ToString(model.blankRadius)
                                        + "r" + Convert.ToString(model.friction) + "f";

            file.outputFolder = Path.Combine(genReport.folderName, abaqusOutputFolder);

            if (!Directory.Exists(file.outputFolder))
                Directory.CreateDirectory(file.outputFolder);

            #region Пути для солида
                
            file.caeFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, add.jobName + ".cae");
            file.caeFolder = file.caeFolder.Replace(@"\", "/");
            file.stampFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, file.stampName);
            file.stampFolder = file.stampFolder.Replace(@"\", "/");
            file.blankFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, file.blankName);
            file.blankFolder = file.blankFolder.Replace(@"\", "/");
            file.platformFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, file.platformName);
            file.platformFolder = file.platformFolder.Replace(@"\", "/");
                
            #endregion

            var solid = new CAD();

            solid.UseSolid(file, model);

            rout.pathIn = "abaqusMacros.py";
            rout.workingDirectory = file.outputFolder;
            rout.pathOut = rout.workingDirectory + @"\abaqusMacros.py";

            var change = new Change();

            change.Output(rout, model, material, add, file);

            var abaqus = new CAE();

            abaqus.runAbaqus(rout, cmd);

            var processData = new ProcessData(rout, cmd);

            processData.Processing(model, file, filesForVolume, genReport);

            finalReport.AddData(model);

            genReport.AddFrictionToChart(model);

            genReport.AddRadiusToChart(model);

            #region проверка симплекса

            string fileName = "SimplexNodes";

            var str = new StringBuilder();
            str.Append(kForNodes);
            str.Append(";");
            str.Append(model.blankRadius);
            str.Append(";");
            str.Append(model.friction);
            str.Append(";");
            str.Append(model.pctOfDieFilling);
            str.Append(";");
            str.Append((model.reactionForceToBlank * Math.Pow(10, -6)));
            str.Append(";");
            str.Append(Environment.NewLine);

            var fileCreate = new FileCreation();

            fileCreate.CreateFile("", fileName, ".csv", str.ToString());

            #endregion
        }

        //для проерки работы программы
        /*public void Compute(double[,] array, int counterStart, int kForNodes)
        {
            model.pctOfDieFilling = (counterStart + 1)  * 100;
            model.reactionForceToBlank = counterStart * 3;
        }*/
    }
}
