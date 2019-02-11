using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class AddRow
    {
        public void AddingRow(double[,] original, SimplexPlanningParams param)
        {
            int lastRow = original.GetUpperBound(0);
            int lastColumn = original.GetUpperBound(1);
            // Create new array.
            double[,] result = new double[lastRow + 2, lastColumn + 1];
            // Copy existing array into the new array.
            for (int i = 0; i <= lastRow; i++)
            {
                for (int x = 0; x <= lastColumn; x++)
                {
                    result[i, x] = original[i, x];
                }
            }
            // Add the new row.
            for (int i = 0; i < param.newCoordinate.Length; i++)
            {
                result[lastRow + 1, i] = param.newCoordinate[i];
            }

            param.finalArray = result;
        }
    }
}
