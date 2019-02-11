using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
using Test1.ChangeWords;

namespace Test1.OptimizationAlgorithm
{
    class GoldenRatioMethod
    {
        GeneralReport genReport = new GeneralReport();
        Routes rout = new Routes();
        Change change = new Change();
        ReactionForceReport report = new ReactionForceReport();
        VolumeReport rpr = new VolumeReport();
        CAD solid = new CAD();

        public void Optimization(GeometryOptions model, MaterialsOptions material, AdditionallyOptions add, FileName file, CommonReport fReport)
        {
            double firstRadius = model.startBlankRadius;
            double lastRadius = firstRadius * 1.5;

            double eps = 0.01, z = (3 - Math.Sqrt(5)) / 2; //0.25
            double x1 = firstRadius + z * (lastRadius - firstRadius),
                   x2 = lastRadius - z * (lastRadius - firstRadius);

            // изменение угла в матрице
            #region hide
            //if (i == 5)
            //{
            //    model.setRadius = 3 + (0.09 * 18);
            //}

            //if (i == 10)
            //{
            //    model.setRadius = 3 + (0.18 * 18);
            //}

            //if (i == 15)
            //{
            //    model.setRadius = 3 + (0.27 * 18);
            //}
            #endregion

            for (model.incrBlankRadius = 0; lastRadius - firstRadius > eps; 
                model.incrBlankRadius+= 0.01)
            {
                //для поиска max функции, для поиска min функции поменять знак на <=
                if (GetVolumeByRadius(x1, model, file, material, add, fReport) >= GetVolumeByRadius(x2, model, file, material, add, fReport))
                {
                    lastRadius = x2;
                    x2 = x1;
                    x1 = firstRadius + lastRadius - x2;
                }
                else
                {
                    firstRadius = x1;
                    x1 = x2;
                    x2 = firstRadius + lastRadius - x1;
                }
                CalculateAngle(model, material, add, file, fReport);
            }
            double result = (firstRadius + lastRadius) / 2;

            GetVolumeByRadius(result, model, file, material, add, fReport);
            CalculateAngle(model, material, add, file, fReport);

            Form1._Form1.LogArea("Конец расчётов.");
        }

        public void CalculateAngle(GeometryOptions model, MaterialsOptions material, AdditionallyOptions add, FileName file, CommonReport fReport)
        {
            double eps = 50, z = (3 - Math.Sqrt(5)) / 2; //5
            //максимальная высота
            double minStampAngle = model.startAngle;
            double maxStampAngle = model.endAngle;

            double x3 = minStampAngle + z * (maxStampAngle - minStampAngle),
                    x4 = maxStampAngle - z * (maxStampAngle - minStampAngle);

            for (int j = 0; maxStampAngle - minStampAngle > eps &&
                model.blankVolume <= model.dieVolume; j++)
            {
                //для поиска max функции, для поиска min функции поменять знак на <=
                if (GetVolumeByAngle(x3, model, file, material, add, fReport) >= GetVolumeByAngle(x4, model, file, material, add, fReport))
                {
                    maxStampAngle = x4;
                    x4 = x3;
                    x3 = minStampAngle + maxStampAngle - x4;
                }
                else
                {
                    minStampAngle = x3;
                    x3 = x4;
                    x4 = minStampAngle + maxStampAngle - x3;
                }
                //просчёт с изменением силы трения
                CalculateFriction(model, material, add, file, fReport);
            }
            double x = (minStampAngle + maxStampAngle) / 2;
            GetVolumeByAngle(x, model, file, material, add, fReport);
            //genReport.DrawAngleChart();
        }

        public double CalculateFriction(GeometryOptions model, MaterialsOptions material, AdditionallyOptions add, FileName file, CommonReport fReport)
        {
            double eps = 0.01, z = (3 - Math.Sqrt(5)) / 2; //0.1
            //максимальная высота
            double minFriction = model.startFriction;
            double maxFriction = model.endFriction;

            double x5 = minFriction + z * (maxFriction - minFriction),
                    x6 = maxFriction - z * (maxFriction - minFriction);

            for (int j = 0; maxFriction - minFriction > eps &&
                model.blankVolume <= model.dieVolume; j++)
            {
                //для поиска max функции, для поиска min функции поменять знак на <=
                if (GetVolumeByFriction(x5, model, file, material, add, fReport) >= GetVolumeByFriction(x6, model, file, material, add, fReport))
                {
                    maxFriction = x6;
                    x6 = x5;
                    x5 = minFriction + maxFriction - x6;
                }
                else
                {
                    minFriction = x5;
                    x5 = x6;
                    x6 = minFriction + maxFriction - x5;
                }
            }
            double x = (minFriction + maxFriction) / 2;
            GetVolumeByFriction(x, model, file, material, add, fReport);
            //genReport.DrawFrictionChart(model);
            return model.pctOfDieFilling;
        }

        public void CalculateHeight(GeometryOptions model, MaterialsOptions material, AdditionallyOptions add, FileName file)
        {
            double eps = 0.1, z = (3 - Math.Sqrt(5)) / 2;
            //максимальная высота
            double maxBlankHeight = model.blankHeight;
            maxBlankHeight = maxBlankHeight + ((maxBlankHeight * 4) / 100);
            maxBlankHeight = Math.Round(maxBlankHeight,2);

            double x3 = model.blankHeight + z * (maxBlankHeight - model.blankHeight),
                    x4 = maxBlankHeight - z * (maxBlankHeight - model.blankHeight);

            for (int j = 0; maxBlankHeight - model.blankHeight > eps &&
                model.blankVolume <= model.dieVolume; j++)
            {
                //для поиска max функции, для поиска min функции поменять знак на <=
                if (GetVolumeByHeight(x3, model, file, material, add) >= GetVolumeByHeight(x4, model, file, material, add))
                {
                    maxBlankHeight = x4;
                    x4 = x3;
                    x3 = model.blankHeight + maxBlankHeight - x4;
                }
                else
                {
                    model.blankHeight = x3;
                    x3 = x4;
                    x4 = model.blankHeight + maxBlankHeight - x3;
                }
            }
            double x = (model.blankHeight + maxBlankHeight) / 2;
            GetVolumeByHeight(x, model, file, material, add);
            //genReport.DrawAngleChart();
        }

        public double GetVolumeByRadius(double x, GeometryOptions model, FileName file, MaterialsOptions material, AdditionallyOptions add, CommonReport finalReport)
        {
            model.stampAngle = model.startAngle;
            model.stampAngle = Math.Round(model.stampAngle);

            model.friction = model.startFriction;
            model.friction = Math.Round(model.startFriction,2);

            #region "+" будут обозначены строки, котоыре необходимы в методе вычесления угла матрицы, заключенніе в регион
            //10 - верхний радиус
            model.dieRadius = (Math.Tan(model.stampAngle * Math.PI / 180) * 57) + 10;
            model.dieRadius = Math.Round(model.dieRadius, 1);
            //объем цилиндрического выреза в матрице
            // double tempVolume = Math.PI * Math.Pow(model.dieRadius, 2) * 4;
            //объём усеченного конуса (верхний радиус 10, нижний может меняться) 
            model.dieVolume = (Math.PI * 57 * (Math.Pow(model.dieRadius, 2) + model.dieRadius * 10 + Math.Pow(10, 2))) / 3;
            // model.dieVolume = model.dieVolume + tempVolume;
            model.dieVolume = Math.Round(model.dieVolume, 2);
            #endregion
            //ЗАМЕНИТЬ НА Х ОБРАТНО
            model.blankRadius = x;
            model.blankRadius = Math.Round(model.blankRadius, 2);

            #region "+"
            model.blankUpperRadius = model.blankRadius - (Math.Tan(model.stampAngle * Math.PI / 180) * 2);
            model.blankPosition = (Math.PI * 2 * (Math.Pow(model.blankRadius, 2) + model.blankRadius * (model.blankUpperRadius) + Math.Pow((model.blankUpperRadius), 2))) / 3;
            model.blankPosition = Math.Round(model.blankPosition, 2);
            //объем цилиндра, не считая фаску (как будто её нет,а заготовка имеет форму просто цилиндра)
            double abstractRadiusVolume = Math.PI * Math.Pow(model.blankRadius, 2) * 2;
            double blankVolume = model.dieVolume + (abstractRadiusVolume - model.blankPosition);
            //объём зазора между матрицей и площадкой
            //double tempVolume = Math.Pow((model.dieRadius + 10), 2) * 3;
            double tempVolume = blankVolume * 10;
            tempVolume /= 100;
            blankVolume += tempVolume;

            model.blankHeight = blankVolume / (Math.PI * Math.Pow(model.blankRadius, 2));
            model.blankHeight = Math.Round(model.blankHeight, 2);

            model.blankPosition = model.blankHeight * ((model.dieRadius - model.blankRadius) / (model.dieRadius - 10));
            model.blankPosition = Math.Round(model.blankPosition, 1);
            model.blankPosition += 2;

            //задаем перемещение матрицы
            model.stampDisplacement = model.blankPosition;
            model.stampDisplacement -= 3;
            #endregion

            #region Создание общей папки с название радиуса заготовки

            genReport.folderName = Convert.ToString(model.blankRadius);

            if (!Directory.Exists(genReport.folderName))
            {
                Directory.CreateDirectory(genReport.folderName);
            }
            #endregion

            string abaqusOutputFolder = "b" + Convert.ToString(model.blankRadius)
                                        + "r" + Convert.ToString(model.stampAngle) + "a";

            file.outputFolder = Path.Combine(genReport.folderName, abaqusOutputFolder);

            if (!Directory.Exists(file.outputFolder))
            {
                Directory.CreateDirectory(file.outputFolder);
            }
            file.caeFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, add.jobName + ".cae");
            file.caeFolder = file.caeFolder.Replace(@"\", "/");
            file.stampFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Stamp.IGS");
            file.stampFolder = file.stampFolder.Replace(@"\", "/");
            file.blankFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Blank.IGS");
            file.blankFolder = file.blankFolder.Replace(@"\", "/");
            file.platformFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Platform.IGS");
            file.platformFolder = file.platformFolder.Replace(@"\", "/");
            
            solid.UseSolid(file,model);

            rout.pathIn = "abaqusMacros.py";
            // old rout.setWorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            rout.workingDirectory = file.outputFolder;
            rout.pathOut = rout.workingDirectory + @"\abaqusMacros.py";

            change.Output(rout, model, material, add, file);

            //Form1._Form1.runAbaqus(file);

            //Form1._Form1.GetHistoryregionName(file);

            //Form1._Form1.Processing(model, file);

            //report.ReportReactionForce(model, genReport, file);

            //rpr.ReportVolume(model, genReport, file);

            finalReport.AddData(model);

            //FReport.setMaxVolume.Add(
            //    new VariablesForReport(model.blankRadius, model.blankRadius, 
            //        model.friction, model.blankVolume, model.setRectionForceToBlank)
            //        );

            return model.pctOfDieFilling;
        }

        public double GetVolumeByAngle(double x, GeometryOptions model, FileName file, MaterialsOptions material, AdditionallyOptions add, CommonReport finalReport)
        {
            //добавляем здесь, так как прошло изменение радиуса
            //после изменения радиуса, добавляем последние значения в постройку графика
            //округляем высоту заготовки, иначе после работы алгоритма добавит как новую высоту,
            //из-за точности знаков после запятой и моего костыля :)
            model.stampAngle = Math.Round(model.startAngle);
            AngleChart(model);

            model.friction = model.startFriction;
            model.friction = Math.Round(model.startFriction, 2);

            x = Math.Round(x);

            #region "+" будут обозначены строки, котоыре необходимы в методе вычесления угла матрицы, заключенніе в регион
            model.dieRadius = (Math.Tan(x * Math.PI / 180) * 57) + 10;
            model.dieRadius = Math.Round(model.dieRadius, 1);
            //объём усеченного конуса (верхний радиус 10, нижний может меняться) 
            model.dieVolume = (Math.PI * 57 * (Math.Pow(model.dieRadius, 2) + model.dieRadius * 10 + Math.Pow(10, 2))) / 3;
            model.dieVolume = Math.Round(model.dieVolume, 2);
            #endregion

            #region "+"
            model.blankUpperRadius = model.blankRadius - (Math.Tan(model.stampAngle * Math.PI / 180) * 2);
            model.blankPosition = (Math.PI * 2 * (Math.Pow(model.blankRadius, 2) + model.blankRadius * (model.blankUpperRadius) + Math.Pow((model.blankUpperRadius), 2))) / 3;
            model.blankPosition = Math.Round(model.blankPosition, 2);
            //объем цилиндра, не считая фаску (как будто её нет,а заготовка имеет форму просто цилиндра)
            double abstractRadiusVolume = Math.PI * Math.Pow(model.blankRadius, 2) * 2;
            double blankVolume = model.dieVolume + (abstractRadiusVolume - model.blankPosition);
            double tempVolume = blankVolume * 10;
            tempVolume /= 100;
            blankVolume += tempVolume;

            model.blankHeight = blankVolume / (Math.PI * Math.Pow(model.blankRadius, 2));
            model.blankHeight = Math.Round(model.blankHeight, 2);

            model.blankPosition = model.blankHeight * ((model.dieRadius - model.blankRadius) / (model.dieRadius - 10));
            model.blankPosition = Math.Round(model.blankPosition, 1);
            model.blankPosition += 2;

            //задаем перемещение матрицы
            model.stampDisplacement = model.blankPosition;
            model.stampDisplacement -= 3;
            #endregion

            model.stampAngle = x;

            string abaqusOutputFolder = "b" + Convert.ToString(model.blankRadius)
                                  + "r" + Convert.ToString(model.stampAngle) + "a";

            file.outputFolder = Path.Combine(genReport.folderName, abaqusOutputFolder);

            if (!Directory.Exists(file.outputFolder))
            {
                Directory.CreateDirectory(file.outputFolder);
            }

            file.caeFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, add.jobName + ".cae");
            file.caeFolder = file.caeFolder.Replace(@"\", "/");
            file.stampFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Stamp.IGS");
            file.stampFolder = file.stampFolder.Replace(@"\", "/");
            file.blankFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Blank.IGS");
            file.blankFolder = file.blankFolder.Replace(@"\", "/");
            file.platformFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Platform.IGS");
            file.platformFolder = file.platformFolder.Replace(@"\", "/");

            solid.UseSolid(file, model);

            rout.workingDirectory = file.outputFolder;
            rout.pathOut = rout.workingDirectory + @"\abaqusMacros.py";

            change.Output(rout, model, material, add, file);

            //Form1._Form1.runAbaqus(file);

            //Form1._Form1.GetHistoryregionName(file);

            //Form1._Form1.Processing(model, file);

            genReport.folderName = Convert.ToString(model.blankRadius);

            //report.ReportReactionForce(model, genReport, file);

            //rpr.ReportVolume(model, genReport, file);

            AngleChart(model);

            //просчёт с изменением силы трения
            //CalculateFriction(model, material, add, file, finalReport);

            finalReport.AddData(model);

            //FReport.setMaxVolume.Add(
            //    new VariablesForReport(model.blankRadius, model.blankRadius,
            //        model.friction, model.pctOfDieFilling, model.setRectionForceToBlank)
            //        );

            return model.pctOfDieFilling;
        }

        public double GetVolumeByFriction(double x, GeometryOptions model, FileName file, MaterialsOptions material, AdditionallyOptions add, CommonReport finalReport)
        {
            model.friction = Math.Round(model.startFriction,2);
            FrictionChart(model);

            x = Math.Round(x,2);

            model.friction = x;

            string abaqusOutputFolder = "b" + Convert.ToString(model.stampAngle)
                                  + "a" + Convert.ToString(model.friction) + "f";

            file.outputFolder = Path.Combine(genReport.folderName, abaqusOutputFolder);

            if (!Directory.Exists(file.outputFolder))
            {
                Directory.CreateDirectory(file.outputFolder);
            }

            file.caeFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, add.jobName + ".cae");
            file.caeFolder = file.caeFolder.Replace(@"\", "/");
            file.stampFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Stamp.IGS");
            file.stampFolder = file.stampFolder.Replace(@"\", "/");
            file.blankFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Blank.IGS");
            file.blankFolder = file.blankFolder.Replace(@"\", "/");
            file.platformFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Platform.IGS");
            file.platformFolder = file.platformFolder.Replace(@"\", "/");

            solid.UseSolid(file, model);

            rout.workingDirectory = file.outputFolder;
            rout.pathOut = rout.workingDirectory + @"\abaqusMacros.py";

            change.Output(rout, model, material, add, file);

            //Form1._Form1.runAbaqus(file);

            //Form1._Form1.GetHistoryregionName(file);

            //Processing(model, file);

            genReport.folderName = Convert.ToString(model.blankRadius);

            //report.ReportReactionForce(model, genReport, file);

            //rpr.ReportVolume(model, genReport, file);

            FrictionChart(model);

            finalReport.AddData(model);

            //FReport.setMaxVolume.Add(
            //    new VariablesForReport(model.blankRadius, model.blankRadius,
            //        model.friction, model.blankVolume, model.setRectionForceToBlank)
            //        );

            return model.pctOfDieFilling;
        }

        public double GetVolumeByHeight(double x, GeometryOptions model, FileName file, MaterialsOptions material, AdditionallyOptions add)
        {
            //добавляем здесь, так как прошло изменение радиуса
            //после изменения радиуса, добавляем последние значения в постройку графика
            //округляем высоту заготовки, иначе после работы алгоритма добавит как новую высоту,
            //из-за точности знаков после запятой и моего костыля :)
            model.blankHeight = Math.Round(model.blankHeight, 2);
            AngleChart(model);

            model.blankHeight = Math.Round(x, 2);

            string abaqusOutputFolder = "b" + Convert.ToString(model.blankRadius)
                                  + "r" + Convert.ToString(model.blankHeight) + "h";

            file.outputFolder = Path.Combine(genReport.folderName, abaqusOutputFolder);

            if (!Directory.Exists(file.outputFolder))
            {
                Directory.CreateDirectory(file.outputFolder);
            }

            file.stampFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Stamp.IGS");
            file.stampFolder = file.stampFolder.Replace(@"\", "/");
            file.blankFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Blank.IGS");
            file.blankFolder = file.blankFolder.Replace(@"\", "/");
            file.platformFolder = Path.Combine(Environment.CurrentDirectory, file.outputFolder, "Platform.IGS");
            file.platformFolder = file.platformFolder.Replace(@"\", "/");

            solid.UseSolid(file, model);

            rout.workingDirectory = file.outputFolder;
            rout.pathOut = rout.workingDirectory + @"\abaqusMacros.py";

            change.Output(rout, model, material, add, file);

            //Form1._Form1.runAbaqus(file);

            //Form1._Form1.GetHistoryregionName(file);

            //Processing(model, file);

            genReport.folderName = Convert.ToString(model.blankRadius);

            //report.ReportReactionForce(model, genReport, file);

            //rpr.ReportVolume(model, genReport, file);
            //теперь добавляем, после изменения радиуса
            //genReport.volTempX.Add(model.blankHeight);
            //genReport.volTempY.Add(model.pctOfDieFilling);

            //genReport.RFTempX.Add(model.blankHeight);
            //genReport.RFTempY.Add(model.setRectionForceToBlank);
            AngleChart(model);

            return model.pctOfDieFilling;
        }
        
        public void AngleChart(GeometryOptions model)
        {
            if (!genReport.AngleVolume.Keys.Contains(model.stampAngle))
            {
                genReport.AngleVolume.Add(model.stampAngle, model.pctOfDieFilling);
            }

            if (!genReport.AngleReactionForce.Keys.Contains(model.stampAngle))
            {
                genReport.AngleReactionForce.Add(model.stampAngle, model.reactionForceToBlank);
            }
        }

        public void FrictionChart(GeometryOptions model)
        {
            if (!genReport.FrictionVolume.Keys.Contains(model.friction))
            {
                genReport.FrictionVolume.Add(model.friction, model.pctOfDieFilling);
            }

            if (!genReport.FrictionReactionForce.Keys.Contains(model.friction))
            {
                genReport.FrictionReactionForce.Add(model.friction, model.reactionForceToBlank);
            }
        }
    }
}

