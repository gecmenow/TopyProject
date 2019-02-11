using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Test1.Model;
using Test1.Files;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class WorkingArray
    {
        FileCreation fileCreate = new FileCreation();

        //передать в массивы значения параметров топологии и eps
        public double[,] makeWorkingArray(double[,] planningMatrix, GeometryOptions model)
        {
            double[,] newArray = new double[planningMatrix.GetLength(0), planningMatrix.GetLength(1)];

            //for 3 params
            //double[] input = { model.setStartAngle, model.blankRadius, model.setStartFriction };
            //for 2 params
            double[] input = { model.startBlankRadius, model.startFriction };

            //double angleEps = (model.setStartAngle * 10) / 100;

            double blankEps = (model.startBlankRadius * 3) / 100; //3
            double frictionEps = (model.startFriction * 10) / 100;

            //for 3 params
            //double[] eps = { angleEps, blankEps, frictionEps };

            //for 2 params
            double[] eps = { blankEps, frictionEps };

            for (int i = 0; i < planningMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < planningMatrix.GetLength(1); j++)
                {
                    newArray[i, j] = input[j] + eps[j] * planningMatrix[i, j];
                }
            }

            var str = new StringBuilder();
            str.Append("#");
            str.Append(";");
            str.Append("Radius,mm");
            str.Append(";");
            str.Append("Friction");
            str.Append(";");
            str.Append("Filling, %");
            str.Append(";");
            str.Append("Force, MN");
            str.Append(";");

            fileCreate.CreateHeader("", "SimplexNodes", "csv", str.ToString());

            return newArray;
        }
    }
}
