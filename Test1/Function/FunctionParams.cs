using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test1.Function
{
    class FunctionParams
    {
        const int N = 2; //кол-во факторов

        public int getFactorsCount()
        {
            return N;
        }

        int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                if (value < 0)
                    MessageBox.Show("Wrong index value: " + value, "Index");
                else
                    _index = value;
            }

        }

        double _indexFilling;

        public double IndexFilling
        {
            get { return _indexFilling; }
            set
            {
                if (value < 0)
                    MessageBox.Show("Wrong filling value: " + value, "Filling");
                else
                    _indexFilling = value;
            }

        }

        double _indexForce;

        public double IndexForce
        {
            get { return _indexForce; }
            set
            {
                if (value < 0)
                    MessageBox.Show("Wrong force value: " + value, "Force");
                else
                    _indexForce = value;
            }

        }

        double _maxForce;

        public double MaxForce
        {
            get { return _maxForce; }
            set
            {
                if (value < 0)
                    MessageBox.Show("Wrong force value: " + value, "MaxForce");
                else
                    _maxForce = value;
            }

        }

        double _minFilling;

        public double MinFilling
        {
            get { return _minFilling; }
            set
            {
                if (value < 0)
                    MessageBox.Show("Wrong force value: " + value, "MinFilling");
                else
                    _minFilling = value;
            }
        }
    }
}
