using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class PlanningArray
    {
        public double[,] makePlanningArray(double[,] array)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            double[,] B = array;
            double max = Int32.MinValue;

            foreach (var number in array)
            {
                if (number > max)
                {
                    max = number;
                }
            }

            for (int i = 0; i < B.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    B[i, j] /= max;
                    B[i, j] = Math.Round(B[i, j], 3);
                }
            }
            return B;
        }
    }
}
