using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Test1.Files
{
    class FileCreation
    {
        /// <summary>
        /// </summary>
        /// <param name="path">
        /// You can leave this field empty if you wanan create file in the root folder of program.
        /// </param>
        /// <param name="fileName"></param>
        /// <param name="fileFormat">
        /// Just write  down file format without dot.
        /// </param>
        /// <param name="header">
        /// For .csv files use ";" between headers.
        /// </param>
        public void CreateHeader(string path, string fileName, string fileFormat, string header)
        {
            String Header = String.Join(
                    Environment.NewLine, header + Environment.NewLine);

            path = path + fileName + "." + fileFormat;

            if (!File.Exists(path))
            {
                File.WriteAllText(path, Header);
            }
        }

        public void CreateHeaderedFile(string path, string fileName, string fileFormat, string content)
        {
            path = path + fileName + fileFormat;

            if (!File.Exists(path))
            {
                MessageBox.Show("First, you need to create a file with some header.");
            }

            File.AppendAllText(path, content);
        }

        public void CreateFile(string path, string fileName, string fileFormat, string content)
        {
            path = path + fileName + fileFormat;

            if (!File.Exists(path))
            {
                File.AppendAllText(path, content);
            }

            File.AppendAllText(path, content);
        }
    }
}
