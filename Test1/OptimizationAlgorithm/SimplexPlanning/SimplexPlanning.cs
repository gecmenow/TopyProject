using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

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
using Test1.Check;
using Test1.ChangeWords;
using Test1.Function;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class Restrictions
    {
        public double Filling;
        public double Force;

        public Restrictions(double filling, double force)
        {
            Filling = filling;
            Force = force;
        }
    }

    class SimplexPlanning
    {
        #region для конструктора
        GeometryOptions model = new GeometryOptions(); 
        MaterialsOptions material = new MaterialsOptions();
        AdditionallyOptions add = new AdditionallyOptions();
        FileName file = new FileName();
        CommonReport finalReport = new CommonReport();
        Routes rout = new Routes();
        ArgumentsCMD cmd = new ArgumentsCMD();
        FilesForVolume filesForVolume = new FilesForVolume();

        public SimplexPlanning(GeometryOptions model, MaterialsOptions material, AdditionallyOptions add, 
                                FileName file, CommonReport finalReport, Routes rout, ArgumentsCMD cmd,
                                FilesForVolume filesForVolume)
        {
            this.model = model;
            this.material = material;
            this.add = add;
            this.file = file;
            this.finalReport = finalReport;
            this.rout = rout;
            this.cmd = cmd;
            this.filesForVolume = filesForVolume;
        }
        #endregion

        GeneralReport genReport = new GeneralReport();
        Change change = new Change();
        CAD solid = new CAD();
        CAE abaqus = new CAE();
        CheckPoints check = new CheckPoints();
        FileCreation fileCreate = new FileCreation();
        SimplexPlanningParams param = new SimplexPlanningParams();
        FunctionParams funcParam = new FunctionParams();

        //-----------
        double kForNodes = 1;
        //--------

        public void Optimization()
        {
            var experimentArray = new ExperimentPointsMatrix();

            double[,] expArray = experimentArray.makeExperimentPointsMatrix(funcParam.getFactorsCount());

            var planningArray = new PlanningArray();

            double[,] planArray = planningArray.makePlanningArray(expArray);

            var workArray = new WorkingArray();

            param.finalArray = workArray.makeWorkingArray(planArray, model);

            Computing(model);
        }

        double[,] prevOldArray;

        private void Computing(GeometryOptions model)
        {
            prevOldArray = new double[param.finalArray.GetLength(0), param.finalArray.GetLength(1)];
            //для проверки на зацикливание 
            param.tempOldArray = new double[param.finalArray.GetLength(0), param.finalArray.GetLength(1)];
            param.sameNodeSimplexCount = new double[param.finalArray.GetLength(0), param.finalArray.GetLength(1)];

            param.oldArray = new double[param.finalArray.GetLength(0), param.finalArray.GetLength(1)];

            int kForNodes = 1;

            while (param.Step > param.getEps())
            {
                if (check.CheckArrayPoints(param.finalArray) == true)
                {
                    param.oldArray = prevOldArray;

                    break;
                }

                var newRow = new NewRow();

                int counterStart = newRow.CheckNewRow(param.finalArray, param.oldArray, param);

                var listOfComputedPoints = new ListOfComputedPoints();

                param.indexOfRestrictions = listOfComputedPoints
                                    .getListOfComputedPoints(param)
                                    .ToDictionary(k=> k.Key, v=> new Restrictions(v.Value.Filling,v.Value.Force));

                for (int i = counterStart; i < param.finalArray.GetLength(0); i++)
                {
                    var mainFunction = new MainFunction(model, material, add, file, finalReport, genReport,
                        rout, cmd, filesForVolume);
                    //выполнение главной функции
                    mainFunction.Compute(param.finalArray, i, kForNodes);

                    param.indexOfRestrictions.Add(i, new Restrictions(model.pctOfDieFilling, model.reactionForceToBlank));

                    kForNodes++;
                }

                var checker = new Checker();

                if (checker.CheckRestrictions(param, funcParam) == false)
                    break;

                //если в новая точка оказалась плохой, то возвращаемся к старому массиву
                //и данынм о заполняемости с индексам точек

                var checkNewCoord = new CheckNewCoordinate();

                if (param.arrayStatus == true && funcParam.Index == funcParam.getFactorsCount())
                    checkNewCoord.CheckingNewCoordinate(param, funcParam);
                else
                {
                    param.badPointsList.Clear();
                    param.badPointsList.Add(funcParam.Index,
                        new Restrictions(funcParam.IndexFilling, funcParam.IndexForce));
                }
                
                #region Копия старого симплекса с плохой точкой

                prevOldArray = param.oldArray;

                param.oldIndexOfRestrictions = param.indexOfRestrictions.ToDictionary(k => k.Key,
                                       v => new Restrictions(v.Value.Filling, v.Value.Force));
                param.oldArray = param.finalArray;
                
                #endregion

                var rewrire = new RewriteArray();

                rewrire.makeRewriteArray(param, funcParam, model);

                param.arrayStatus = true;

                param.Step = 0;
            }
            genReport.folderName = finalReport.finalReportFolder;

            finalReport.makeFinalReport(param.oldArray);

            var charts = new Charts(genReport);

            charts.DrawFrictionChart();
            charts.DrawRadiusChart();

            MessageBox.Show("Computing is complete!");
        }        
    }
}
