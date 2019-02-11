using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test1.OptimizationAlgorithm.SimplexPlanning
{
    class SimplexPlanningParams
    {
        public double[,] finalArray;
        public double[,] oldArray;
        //для проверки на зацикливание 
        public double[,] tempOldArray;

        public double[,] sameNodeSimplexCount;

        public double[] newCoordinate;
        public double[] badCoordinate;

        //кол-во массивов с одинаковой точкой
        public double countOfArrays;

        //ограничение на кол-во массивов с одинаковой точкой
        //если больше этого числа, то зацикливание
        public double Nmax;

        //минимальный шаг между элементами симплекса
        double _step = double.MaxValue;

        public double Step
        {
            get { return _step; }
            set
            {
                if (value > 0 || value == 0)
                    _step = value;
                if (value < 0)
                    MessageBox.Show("Value can't be less then zero", "Step");
            }
        }
        //точность расчётов, минимальная разница между элементами симплекса
        const double _eps = 0.001;

        public double getEps()
        {
            return _eps;
        }

        double _moreEps = 0.01;

        public double moreEps
        {
            get { return _moreEps; }
            set
            {
                if (value > 0)
                    _moreEps = value;
                if (value < 0)
                    MessageBox.Show("Value can't be zero or less", "Step");
            }
        }

        //флажок на проверку старый или новый массив
        public bool arrayStatus = false;

        public Dictionary<int, Restrictions> indexOfRestrictions = new Dictionary<int, Restrictions>();

        public Dictionary<int, Restrictions> oldIndexOfRestrictions = new Dictionary<int, Restrictions>();

        public Dictionary<int, Restrictions> badPointsList = new Dictionary<int, Restrictions>();

        //список индексов одинаковых строк
        public List<int> equalRowsIndex = new List<int>();
    }
}
