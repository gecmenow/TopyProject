using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class ExperimentPointsMatrix
    {
        private double smallRadius(double k)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            double r;

            k += 1;

            r = -(1 / Math.Sqrt(2 * k * (k + 1)));

            r = Math.Round(r, 3);

            return r;
        }

        private double bigRadius(double k)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            double r;

            k += 1;

            r = Math.Sqrt(k / (2 * (k + 1)));

            r = Math.Round(r, 3);

            return r;
        }

        public double[,] makeExperimentPointsMatrix(int parametrsCount)
        {
            int N = parametrsCount;

            double[,] A = new double[N + 1, N];

            for (int i = 0; i < N + 1; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i <= j)
                    {
                        A[i, j] = smallRadius(j);
                    }
                    else if (i - j == 1)
                    {
                        A[i, j] = bigRadius(j);
                    }
                    else
                    {
                        A[i, j] = 0;
                    }
                }
            }

            return A;
        }
    }
}
