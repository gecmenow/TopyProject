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
    class Perebor
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
            double lastRadius = 10.5;

            //double firstRadius = 10.87;
            //double lastRadius = firstRadius;

            double frictionStep = 0.1;

            for (model.stampAngle = model.startAngle; model.stampAngle <= model.endAngle; model.stampAngle += model.stepAngle)
            {
                for (model.incrBlankRadius = firstRadius; model.incrBlankRadius <= lastRadius; model.incrBlankRadius += 0.05)
                {
                    // округляем из-за проблемы сложения чисел 0.1 + 0.2
                    for (model.friction = model.startFriction; model.friction <= model.endFriction; 
                        model.friction = Math.Round(model.friction + frictionStep, 1))
                    {
                        // считаем радиус усечённого конуса
                        model.dieRadius = (Math.Tan(model.stampAngle * Math.PI / 180) * 60) + 10;

                        //объём цилиндра (верхний радиус 10, нижний может меняться)
                        model.dieVolume = (Math.PI * 60 * (Math.Pow(model.dieRadius, 2) + model.dieRadius * 10 + Math.Pow(10, 2)) / 3);

                        #region необходимо для вычесления облоя

                        double upperRadius;
                        double angle30X;
                        double filletLength;

                        //3 - радиус скругления, 90 - градус
                        filletLength = Math.Tan((180 - (((90 + model.stampAngle) / 2) + 90)) * Math.PI / 180) * 3;

                        upperRadius = model.dieRadius + filletLength + 10;

                        angle30X = upperRadius - 5;
                        #endregion

                        double lowerVolume = Math.Pow((angle30X - 5), 2) * Math.PI * 1;

                        model.dieVolume += lowerVolume;

                        double shave = Math.Pow(angle30X, 2) * Math.PI * 1;

                        shave -= lowerVolume;

                        model.dieVolume = Math.Round(model.dieVolume, 1);

                        model.blankRadius = model.incrBlankRadius;

                        model.blankUpperRadius = model.blankRadius - (Math.Tan(model.stampAngle * Math.PI / 180) * 2);
                        model.blankChamferVolume = (Math.PI * 2 * (Math.Pow(model.blankRadius, 2) + model.blankRadius * (model.blankUpperRadius) + Math.Pow((model.blankUpperRadius), 2))) / 3;
                        model.blankChamferVolume = Math.Round(model.blankChamferVolume, 2);
                        
                        //объем цилиндра, не считая фаску (как будто её нет,а заготовка имеет форму просто цилиндра)
                        double abstractRadiusVolume = Math.PI * Math.Pow(model.blankRadius, 2) * 2;
                        double blankVolume = model.dieVolume + (abstractRadiusVolume - model.blankChamferVolume);

                        blankVolume += shave;

                        model.blankHeight = blankVolume / (Math.PI * Math.Pow(model.blankRadius, 2));
                        model.blankHeight = Math.Round(model.blankHeight, 2);

                        double radiusAfterLowerFillet = angle30X - 5;
                        
                        double radiusHeightInStamp;
                        
                        //необходимо для вычесления объёма от пуансона до конца матрицы
                        model.lowerFilletRadius = radiusAfterLowerFillet;

                        radiusHeightInStamp = 60 * ((radiusAfterLowerFillet - model.blankRadius) / (radiusAfterLowerFillet - 10));

                        if (model.blankRadius > 12.5)
                        {
                            
                            //model.blankPosition = model.blankHeight * ((model.dieRadius - model.blankRadius) / (model.dieRadius - 10));
                            model.blankPosition = model.blankHeight * ((radiusAfterLowerFillet - model.blankRadius) / (radiusAfterLowerFillet - 10));
                            model.blankPosition = Math.Round(model.blankPosition, 1);
                            model.blankPosition = Math.Abs(model.blankPosition - model.blankHeight);
                            //model.blankPosition += 2;
                        }

                        else if (model.blankRadius > 10)
                        {
                            //model.blankPosition = 60 * ((model.dieRadius - model.blankRadius) / (model.dieRadius - 10));
                            model.blankPosition = model.blankHeight - radiusHeightInStamp;
                            model.blankPosition = Math.Round(model.blankPosition, 1);
                            model.blankPosition += 5;
                        }

                        else
                        {
                            //предположим, что это высота заготовки, которая равняется разницей в высоте 
                            //между ей и высотой матрицы, должно помочь, когда заготовка высокая
                            double tempHeight = model.blankHeight - 60;

                            model.blankPosition = tempHeight * ((model.dieRadius - 10) / (model.dieRadius - 10));
                            model.blankPosition = Math.Round(model.blankPosition, 1);
                            //допуск +2мм на всякий пожарный случай
                            model.blankPosition += 2;
                        }
                        
                        //задаем перемещение матрицы
                        model.stampDisplacement = model.blankPosition;
                        
                        //оставляем зазор 3мм
                        model.stampDisplacement -= 1;

                        #region Создание общей папки с название радиуса заготовки

                        genReport.folderName = Convert.ToString(model.blankRadius);

                        if (!Directory.Exists(genReport.folderName))
                        {
                            Directory.CreateDirectory(genReport.folderName);
                        }
                        #endregion

                        string abaqusOutputFolder = Convert.ToString(model.blankRadius)
                                                    + "r" + Convert.ToString(model.stampAngle) + "a"
                                                    + Convert.ToString(model.friction) + "f";

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

                        RadiusChart(model);

                        FrictionChart(model);
                    }
                    //genReport.DrawFrictionChart(model);
                }
                //genReport.DrawRadiusChart(model); 
            }
        }

        private void RadiusChart(GeometryOptions model)
        {
            if (!genReport.RadiusVolume.Keys.Contains(model.blankRadius))
            {
                genReport.RadiusVolume.Add(model.blankRadius, model.pctOfDieFilling);
            }

            if (!genReport.RadiusReactionForce.Keys.Contains(model.blankRadius))
            {
                genReport.RadiusReactionForce.Add(model.blankRadius, model.reactionForceToBlank);
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

