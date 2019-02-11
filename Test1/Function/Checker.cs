using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Test1.Function;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class Checker
    {
        public bool CheckRestrictions(SimplexPlanningParams param, FunctionParams funcParam)
        {
            bool breakFlag = true;

            //ищем минимальное значение, после подсчётов матрицы в функции
            funcParam.MinFilling = param.indexOfRestrictions.Select(v => v.Value.Filling).Min();
            funcParam.MaxForce = param.indexOfRestrictions.Select(v => v.Value.Force).Max();

            double countOfGoodNodes = param.indexOfRestrictions.Where(v => v.Value.Filling >= 95).Count();
            if (param.arrayStatus == false && countOfGoodNodes < 2 && funcParam.MinFilling < 95)
            {
                MessageBox.Show("Choose another starting point!");
                breakFlag = false;
            }

            if (funcParam.MinFilling >= 95)
            {
                funcParam.Index = param.indexOfRestrictions.Where(v => v.Value.Force == funcParam.MaxForce).Select(k => k.Key).FirstOrDefault();
                funcParam.IndexFilling = param.indexOfRestrictions.Where(v => v.Value.Force == funcParam.MaxForce).Select(v => v.Value.Filling).FirstOrDefault();
                funcParam.IndexForce = param.indexOfRestrictions.Where(v => v.Value.Force == funcParam.MaxForce).Select(v => v.Value.Force).FirstOrDefault();
            }

            if (funcParam.MinFilling < 95)
            {
                funcParam.Index = param.indexOfRestrictions.Where(v => v.Value.Filling == funcParam.MinFilling).Select(k => k.Key).FirstOrDefault();
                funcParam.IndexFilling = param.indexOfRestrictions.Where(v => v.Value.Filling == funcParam.MinFilling).Select(v => v.Value.Filling).FirstOrDefault();
                funcParam.IndexForce = param.indexOfRestrictions.Where(v => v.Value.Filling == funcParam.MinFilling).Select(v => v.Value.Force).FirstOrDefault();
            }

            return breakFlag;
        }
    }
}
