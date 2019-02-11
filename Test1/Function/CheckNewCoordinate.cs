using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using  Test1.OptimizationAlgorithm.SimplexPlanning;

namespace Test1.Function
{
    class CheckNewCoordinate
    {
        public void CheckingNewCoordinate(SimplexPlanningParams param, FunctionParams funcParam)
        {
            //если новая координата оказывается минимальной взять любую другую в предыдущем симплексе
            //взять точку со старого

            var temp = param.oldIndexOfRestrictions.ToDictionary(k => k.Key, v => v.Value);
            //убираем из списка значений строки с индексом плохой точки

            Dictionary<int, Restrictions> dict3 = new Dictionary<int, Restrictions>();

            if (param.badPointsList.Count == 3)
            {
                param.badPointsList.Clear();
            }

            foreach (var element in param.badPointsList)
            {
                param.oldIndexOfRestrictions.Remove(element.Key);
            }

            dict3 = param.oldIndexOfRestrictions.ToDictionary(k => k.Key, v => new Restrictions(v.Value.Filling, v.Value.Force));

            param.oldIndexOfRestrictions = dict3.ToDictionary(k => k.Key,
                v => new Restrictions(v.Value.Filling, v.Value.Force));

            //ищем минимальное значение, после подсчётов матрицы в функции
            funcParam.MinFilling = param.oldIndexOfRestrictions.Select(v => v.Value.Filling).Min();
            funcParam.MaxForce = param.oldIndexOfRestrictions.Select(v => v.Value.Force).Max();

            if (funcParam.MinFilling < 95)
            {
                funcParam.Index = param.oldIndexOfRestrictions.Where(v => v.Value.Filling == funcParam.MinFilling).Select(k => k.Key).FirstOrDefault();
                funcParam.IndexFilling = param.oldIndexOfRestrictions.Where(v => v.Value.Filling == funcParam.MinFilling).Select(v => v.Value.Filling).FirstOrDefault();
                funcParam.IndexForce = param.oldIndexOfRestrictions.Where(v => v.Value.Filling == funcParam.MinFilling).Select(v => v.Value.Force).FirstOrDefault();
            }

            if (funcParam.MinFilling >= 95)
            {
                funcParam.Index = param.oldIndexOfRestrictions.Where(v => v.Value.Force == funcParam.MaxForce).Select(k => k.Key).FirstOrDefault();
                funcParam.IndexFilling = param.oldIndexOfRestrictions.Where(v => v.Value.Force == funcParam.MaxForce).Select(v => v.Value.Filling).FirstOrDefault();
                funcParam.IndexForce = param.oldIndexOfRestrictions.Where(v => v.Value.Force == funcParam.MaxForce).Select(v => v.Value.Force).FirstOrDefault();
            }

            //присваиваем старые значения новому симплексу для его персчёта
            param.indexOfRestrictions = temp.ToDictionary(k => k.Key,
                v => new Restrictions(v.Value.Filling, v.Value.Force));

            param.finalArray = param.oldArray;

            param.badPointsList.Add(funcParam.Index, 
                new Restrictions(funcParam.IndexFilling, funcParam.IndexForce));

            dict3.Clear();
        }
    }
}
