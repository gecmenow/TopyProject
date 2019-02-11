using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

using Test1.Files;


namespace Test1.Check
{
    class CheckFields
    {

        #region Проверка на пустые поля, метод Check

        public bool Check()
        {
            //locale for "."
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Control[] InitialData = new Control[12];

            InitialData[0] = Form1._Form1.blankRadius;
            InitialData[1] = Form1._Form1.massDensity;
            InitialData[2] = Form1._Form1.YoungsModulus;
            InitialData[3] = Form1._Form1.PoissonsRatio;
            InitialData[4] = Form1._Form1.strtAngle;
            InitialData[5] = Form1._Form1.lastAngle;
            InitialData[6] = Form1._Form1.angleStep;
            InitialData[7] = Form1._Form1.startFriction;
            InitialData[8] = Form1._Form1.endFriction;
            InitialData[9] = Form1._Form1.Mesh;
            InitialData[10] = Form1._Form1.Cores;
            InitialData[11] = Form1._Form1.JobName;

            bool error = false;

            foreach (TextBox item in InitialData)
            {
                if (item.Text.Length < 1)
                {
                    MessageBox.Show(item.Name + " - Заполните поле!");
                    error = true;
                    break;
                }
            }
            return error;
        }

        #endregion

        #region Првоерка на заполненеи таблицы на главной

        public bool CheckDGV()
        {
            //locale for "."
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            bool error = false;

            string str = string.Empty;

            if (Form1._Form1.PlasticData.RowCount == 0 || Form1._Form1.PlasticData.ColumnCount == 0)
            {
                MessageBox.Show(Form1._Form1.PlasticData.Name + " - Заполните поле!");
                error = true;
            }

            for (int i = 0; i < Form1._Form1.PlasticData.RowCount - 1; i++)
            {
                for (int j = 0; j < Form1._Form1.PlasticData.ColumnCount; j++)
                {
                    str += Form1._Form1.PlasticData.Rows[i].Cells[j].Value;
                }
            }

            if (str == " ")
            {
                MessageBox.Show(Form1._Form1.PlasticData.Name + " - Заполните поле!");
                error = true;
            }

            return error;
        }

        #endregion

        #region Проверка на ввод поля Mass Density

        public bool CheckMassDensity()
        {
            //locale for "."
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            bool error = false;

            for (int i = 0; i < Form1._Form1.massDensity.Text.Length; i++)
            {
                if (Form1._Form1.massDensity.Text[i] >= '0' && Form1._Form1.massDensity.Text[i] <= '9'
                                                            || Form1._Form1.massDensity.Text[i] == '.'
                                                            || Form1._Form1.massDensity.Text[i] == 'e'
                                                            || Form1._Form1.massDensity.Text[i] == '-')
                {
                    error = false;
                }

                else
                {
                    MessageBox.Show(Form1._Form1.massDensity.Name + " - Проверьте правильность заполнения поля!");
                    error = true;
                    break;
                }

            }

            return error;
        }

        #endregion

        #region Проверка имени файла

        public bool CheckJobName()
        {
            //locale for "."
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            bool error = false;

            for (int i = 0; i < Form1._Form1.JobName.Text.Length; i++)
            {
                if (Form1._Form1.JobName.Text[i] >= 'a' && Form1._Form1.JobName.Text[i] <= 'z'
                 || Form1._Form1.JobName.Text[i] >= 'A' && Form1._Form1.JobName.Text[i] <= 'Z'
                 || Form1._Form1.JobName.Text[i] >= '0' && Form1._Form1.JobName.Text[i] <= '1'
                                                        || Form1._Form1.JobName.Text[i] == '-'
                                                        || Form1._Form1.JobName.Text[i] == '_')
                {
                    error = false;
                }

                else
                {
                    MessageBox.Show(Form1._Form1.JobName.Name + " - Проверьте правильность заполнения поля!");
                    error = true;
                    break;
                }

            }
            return error;
        }

        #endregion

        public bool CheckExtraFields()
        {
            var fileName = new FileName();
            string path = @"Materials\MaterialName.txt";
            string[] temp = File.ReadAllLines(path);
            string tempPath = @"Materials\";

            Control[] InitialData = new Control[1];

            InitialData[0] = Form1._Form1.ExtraTextBox;


            bool error = false;
            foreach (TextBox item in InitialData)
            {
                if (item.Text.Length < 1)
                {
                    MessageBox.Show("Пожалуйста, введите название металла!");
                    Form1._Form1.ExtraTextBox.BackColor = Color.Red;
                    error = true;
                    break;
                }

                if (Regex.IsMatch(item.Text, @"^[a-zA-Z0-9_]+$") == false)
                {
                    MessageBox.Show("Текст не должен содержать !@#$%^&*()+-", 
                         "Пожалуйста, введите корректное назание металла!");
                    Form1._Form1.ExtraTextBox.BackColor = Color.Red;
                    error = true;
                    break;
                }

                if (temp.ToString() == item.Text || File.Exists(tempPath + item.Text + ".csv"))
                {
                    MessageBox.Show("Файл с таким именем уже существует " + item.Text);
                    Form1._Form1.ExtraTextBox.BackColor = Color.Red;
                    error = true;
                    break;
                }
            }
            Control[] InitialData1 = new Control[1];
            InitialData1[0] = Form1._Form1.ExtraDataGridView;

            foreach (DataGridView item in InitialData1)
            {
                if (item.RowCount < 2)
                {
                    MessageBox.Show("Заполните таблицу!");
                    error = true;
                    break;
                }
            }
            return error;
        }
    }
}
