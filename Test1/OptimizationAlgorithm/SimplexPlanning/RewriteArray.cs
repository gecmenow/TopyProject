using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Test1.Model;
using Test1.Function;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class RewriteArray
    {
        public void makeRewriteArray(SimplexPlanningParams param, FunctionParams funcParam,GeometryOptions model)
        {
            double[] x = new double[param.finalArray.Length];

            double[,] tempFinalArray = new double[param.finalArray.GetLength(0) - 1, param.finalArray.GetLength(1)];
            param.badCoordinate = new double[param.finalArray.GetLength(1)];

            int currentRow = 0;

            for (int i = 0; i < param.finalArray.GetLength(0); i++)
            {
                if (i != funcParam.Index)
                {
                    for (int j = 0; j < param.finalArray.GetLength(1); j++)
                    {
                        tempFinalArray[currentRow, j] = param.finalArray[i, j];
                    }
                    currentRow++;
                }
                else
                {
                    for (int j = 0; j < param.finalArray.GetLength(1); j++)
                    {
                        param.badCoordinate[j] = param.finalArray[i, j];
                    }
                }
            }

            #region Проверка на зацикливание
            //сравнивание двух массивов на наличие одинаковой точки
            //если таково, то добавляем +2 в ячейку(т.к. сравниванием 2 массива)
            for (int i = 0; i < param.finalArray.GetLength(0); i++)
            {
                int Count = 2;
                for (int j = 0; j < param.finalArray.GetLength(1); j++)
                {
                    if (param.finalArray[i, j] == param.tempOldArray[i, j])
                    {
                        param.sameNodeSimplexCount[i, j] += Count;
                    }
                    else
                        param.sameNodeSimplexCount[i, j] = 0;
                }
            }
            //находим максимальную строку, по её сумме, 
            //чтобы по ней посчитать сколько симплексов имеют одинаковую точку
            //симплексы могут иметь 2 одинаковые точки, но таких симплексов может быть 2 или 3
            //поэтому нужно отбросить такой вариант
            List<double> maxList = new List<double>();

            for (int i = 0; i < param.sameNodeSimplexCount.GetLength(0); i++)
            {
                double max = 0;
                for (int j = 0; j < param.sameNodeSimplexCount.GetLength(1); j++)
                {
                    max += param.sameNodeSimplexCount[i, j];
                }
                maxList.Add(max);
            }

            param.countOfArrays = maxList.Max();
            param.countOfArrays /= (funcParam.getFactorsCount() * funcParam.getFactorsCount());

            param.Nmax = (1.65 * funcParam.getFactorsCount()) + (0.05 * Math.Pow(funcParam.getFactorsCount(), 2));

            //записываем новый массив в старый
            for (int i = 0; i < param.finalArray.GetLength(0); i++)
            {
                for (int j = 0; j < param.finalArray.GetLength(1); j++)
                {
                    param.tempOldArray[i, j] = param.finalArray[i, j];
                }
            }

            #endregion

            var newCoord = new NewCoordinate();

            newCoord.makeNewCoordinate(tempFinalArray, model, param, funcParam);

            var addRow = new AddRow();
            addRow.AddingRow(tempFinalArray, param);
        }
    }
}
