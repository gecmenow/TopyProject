using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test1.Model;
using System.Windows.Forms;

namespace Test1.Materials
{
    class MaterialsOptions
    {
        #region Параметры материала

        public double materialDensity;
        public double youngsModulus;
        public double poissonsRatio;
        private string _plastic;

        public List<string> MaterialName = new List<string>();
        public Dictionary<string, string> MaterialData = new Dictionary<string, string>();

        public string getPlastic()
        {
            return _plastic;
        }

        public string setPlastic(DataGridView DataGridView)
        {
            //Получение из данных из DataGridView и формирование строки
            int i, j;
            string str = string.Empty;
            String[] words;
            string value;

            for (i = 0; i < DataGridView.RowCount - 1; i++)
            {
                for (j = 0; j < DataGridView.ColumnCount; j++)
                {
                    str += DataGridView.Rows[i].Cells[j].Value + " ";
                }
            }

            words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (i = 1; i <= words.Length - 2; i += 2)
            {
                words[i] = words[i] + "), (";
            }

            for (i = 0; i < words.Length; i += 2)
            {
                words[i] = words[i] + ", ";
            }

            value = String.Concat<string>(words);

            _plastic = value;

            return _plastic;

            }

        #endregion
    }
}
