using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class ListOfComputedPoints
    {
        public Dictionary<int, Restrictions> getListOfComputedPoints(SimplexPlanningParams param)
        {
            Dictionary<int, Restrictions> tempIndexOfRestrictions = new Dictionary<int, Restrictions>();
            tempIndexOfRestrictions.Clear();

            if (param.equalRowsIndex.Count != 0)
            {
                var temp =
                        from x in param.indexOfRestrictions
                        join y in param.equalRowsIndex on x.Key equals y
                        select new { Key = y, x.Value.Filling, x.Value.Force };

                int z = 0;

                foreach (var element in temp)
                {
                    tempIndexOfRestrictions.Add(z, new Restrictions(element.Filling, element.Force));
                    z++;
                }

                param.indexOfRestrictions.Clear();
                param.indexOfRestrictions = tempIndexOfRestrictions.ToDictionary(kvp => kvp.Key, kvp => new Restrictions(kvp.Value.Filling, kvp.Value.Force));

            }

            return param.indexOfRestrictions;
        }
    }
}
