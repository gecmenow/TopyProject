using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Test1.Model;
using Test1.Function;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class NewCoordinate
    {
        public void makeNewCoordinate(double[,] array, GeometryOptions model, SimplexPlanningParams param, FunctionParams funcParam)
        {
            double minRadius = 10;
            double minFriction = 0.2;

            double maxRadius = model.startBlankRadius * 1.25;
            double maxFriction = model.endFriction;

            param.newCoordinate = new double[array.GetLength(1)];

            double[,] temp = array;
            double[] f = new double[array.GetLength(1)];

            //новый шаг, который уменьшает размеры симплекса
            double newN = funcParam.getFactorsCount() + param.moreEps;

            if (param.countOfArrays > param.Nmax)
            {
                for (int i = 0; i < temp.GetLength(0); i++)
                {
                    for (int j = 0; j < temp.GetLength(1); j++)
                    {
                        f[j] += array[i, j];
                    }
                }

                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    param.newCoordinate[j] = (2 * f[j] / newN) - param.badCoordinate[j];
                }
                param.moreEps += 0.005;
            }
            else
            {
                for (int i = 0; i < temp.GetLength(0); i++)
                {
                    for (int j = 0; j < temp.GetLength(1); j++)
                    {
                        f[j] += array[i, j];
                    }
                }

                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    param.newCoordinate[j] = (2 * f[j] / funcParam.getFactorsCount()) - param.badCoordinate[j];
                }
            }

            #region проверка каждого элемента на мин и макс
            if (param.newCoordinate[0] < minRadius)
            {
                param.newCoordinate[0] = minRadius;
            }

            if (param.newCoordinate[1] < minFriction)
            {
                param.newCoordinate[1] = minFriction;
            }

            if (param.newCoordinate[0] > maxRadius)
            {
                param.newCoordinate[0] = maxRadius;
            }

            if (param.newCoordinate[1] > maxFriction)
            {
                param.newCoordinate[1] = maxFriction;
            }
            #endregion

            double tempStep = Math.Sqrt(Math.Pow(param.badCoordinate[0] - param.newCoordinate[0], 2)
                            + Math.Pow(param.badCoordinate[1] - param.newCoordinate[1], 2));

            double[] tempPct = new double[param.newCoordinate.Length];

            for (int j = 0; j < tempPct.Length; j++)
                tempPct[j] = (param.badCoordinate[j] * 1 / 100); //badCoordinate[j] * точность / 100

            double maxPct = tempPct.Max();

            if (tempStep < maxPct)
            {
                param.Step = 0;
            }
            else
                param.Step = 1;
        }
    }
}
