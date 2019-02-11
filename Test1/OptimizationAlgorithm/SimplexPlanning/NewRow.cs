using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class NewRow
    {
        public int CheckNewRow(double[,] newArray, double[,] oldArray, SimplexPlanningParams param)
        {
            int newIndex = 0;

            double[] temp1 = new double[newArray.GetLength(1)];
            double[] temp2 = new double[newArray.GetLength(1)];

            param.equalRowsIndex.Clear();
            int k = 0;

            while (k < newArray.GetLength(0))
            {
                for (int i = 0; i <= k; i++)
                {
                    for (int j = 0; j < newArray.GetLength(1); j++)
                    {
                        temp1[j] = oldArray[i, j];
                    }
                }

                for (int i = 0; i < newArray.GetLength(0); i++)
                {
                    for (int j = 0; j < newArray.GetLength(1); j++)
                    {
                        temp2[j] = newArray[i, j];
                    }
                    if (temp1.SequenceEqual(temp2) == true)
                    {
                        newIndex++;
                        param.equalRowsIndex.Add(k);
                    }
                }

                k++;
            }

            return newIndex++;
        }
    }
}
