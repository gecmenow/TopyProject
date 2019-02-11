using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Check
{
    class CheckPoints
    {
        public bool CheckArrayPoints(double[,] array)
        {
            bool status = false;

            if ((array[0,0] == array[1,0]) && (array[0,0] == array[2,0]))
            {
                status = true;
            }

            if ((array[0, 1] == array[1, 1]) && (array[0, 1] == array[2, 1]))
            {
                status = true;
            }

            return status;
        }
    }
}
