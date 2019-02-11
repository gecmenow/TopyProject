using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Test1.Materials;
using Test1.Files;

namespace Test1.ExtraPage
{
    class ExtraClass
    {
        public void LoadMaterialData()
        {
            try
            {
                var m = new MaterialsOptions();
                string path = @"Materials\MaterialName.txt";
                
                var lines = File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg));
                File.WriteAllLines(path, lines);
                
                m.MaterialName = File.ReadLines(path).ToList();

                foreach (var items in m.MaterialName)
                {
                    Form1._Form1.MaterialsList.Items.Add(items);
                    Form1._Form1.ExtraMaterialsList.Items.Add(items);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void Remove()
        {
            string path = @"Materials\MaterialName.txt";

            string strFilePath = path;
            string strSearchText = Form1._Form1.ExtraTextBox.Text;
            string strOldText;
            string n = "";

            if (Form1._Form1.ExtraMaterialsList.Text == "")
            {
                MessageBox.Show("Выберите имя металла, которое хотите удалить.");
                return;
            }

            StreamReader sr = File.OpenText(strFilePath);
            while ((strOldText = sr.ReadLine()) != null)
            {
                if (!strOldText.Contains(strSearchText))
                {
                    n += strOldText + Environment.NewLine;
                }
            }
            sr.Close();

            File.WriteAllText(strFilePath, n);

            File.Delete(@"Materials\" + Form1._Form1.ExtraTextBox.Text + ".csv");

            Form1._Form1.MaterialsList.Items.Remove(Form1._Form1.ExtraTextBox.Text);
            Form1._Form1.ExtraMaterialsList.Items.Remove(Form1._Form1.ExtraTextBox.Text);
            Form1._Form1.PlasticData.DataSource = null;
            //Form1._Form1.PlasticData.Refresh();
        }

        public void RefreshCombobox()
        {
            try
            {
                Form1._Form1.MaterialsList.Items.Clear();
                Form1._Form1.ExtraMaterialsList.Items.Clear();

                var fileName = new FileName();
                var m = new MaterialsOptions();
                string path = @"Materials\MaterialName.txt";

                var lines = File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg));
                File.WriteAllLines(path, lines);

                m.MaterialName = File.ReadLines(path).ToList();

                foreach (var items in m.MaterialName)
                {
                    Form1._Form1.MaterialsList.Items.Add(items);
                    Form1._Form1.ExtraMaterialsList.Items.Add(items);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void SaveToCSV(DataGridView DGV)
        {
            try
            {
                string filename = "";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = @"Materials\" + Form1._Form1.ExtraTextBox.Text + ".csv";

                //MessageBox.Show("Data will be exported and you will be notified when it is ready.");
                if (File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                    }
                }

                string[] output = new string[DGV.RowCount - 1];
                for (int i = 1; i < DGV.RowCount; i++)
                {
                    for (int j = 0; j < DGV.ColumnCount; j++)
                    {
                        output[i - 1] += DGV.Rows[i - 1].Cells[j].Value.ToString() + ";";
                    }
                }

                File.WriteAllLines(sfd.FileName, output, System.Text.Encoding.UTF8);

                //MessageBox.Show("Your file was generated and its ready for use.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        } 
    }
}
