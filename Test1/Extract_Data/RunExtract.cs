using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Test1.Rout;
using Test1.Files;

namespace Test1.Extract_Data
{
    
    class RunExtract
    {
        Routes rout = new Routes();

        public RunExtract(Routes rout)
        {
            this.rout = rout;
        }

        const string name = "ABQcaeG";//процесс, который нужно закрыть
        //запускает файл extract.py
        public void ExtractFiles(string cmd)
        {
            try
            {
                Process[] etc = Process.GetProcesses();//получим процессы
                foreach (Process anti in etc)//обойдем каждый процесс
                    if (anti.ProcessName.ToLower().Contains(name.ToLower())) anti.Kill();//переводим имя в нижний регистр, на всякий пожарный

                var proc = new ProcessStartInfo()
                {
                    UseShellExecute = true, //нужно для правильного запуска консоли, но т.к. мы явно указываем консоль, то оно особо и не нужно
                    WorkingDirectory = rout.workingDirectory,//рабочая дериктория, чтобы консоль видела макрос
                    FileName = Path.Combine(rout.cmdEXE, "cmd.exe"),
                    //FileName = @"C:\Windows\System32\cmd.exe",
                    Arguments = "/" + rout.systemDisk + cmd, //передаем команду в консоль
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Form1._Form1.LogArea("Извлекаем данные...\n");

                Process.Start(proc).WaitForExit();
            }
            catch (Exception e)
            {
                MessageBox.Show("Abaqus", e.Message);
            }
        }

        public void ExtractNodes(string cmd, FileName file)
        {
            try
            {
                rout.nodeIn = file.nodePYFile;
                rout.nodeOut = rout.workingDirectory + @"\" + file.nodePYFile;

                StreamReader reader = new StreamReader("node.py");
                string content = reader.ReadToEnd();
                reader.Close();

                StreamWriter writer = new StreamWriter(rout.workingDirectory + @"\node.py");
                writer.Write(content);
                writer.Close();

                ExtractFiles(cmd);
            }
            catch (Exception e)
            {
                MessageBox.Show("Abaqus", e.Message);
            }
        }

        //Получить название Upper Reference Point
        public void ExtractName(string cmd, FileName file)
        {
            try
            {
                rout.nameIn = file.namePYFile;
                rout.nameOut = rout.workingDirectory + @"\" + file.namePYFile;

                StreamReader reader = new StreamReader(rout.nameIn);
                string content = reader.ReadToEnd();
                reader.Close();

                content = Regex.Replace(content, "@path", Convert.ToString(rout.workingDirectory));

                StreamWriter writer = new StreamWriter(rout.nameOut);

                writer.Write(content);
                writer.Close();

                ExtractFiles(cmd);
            }
            catch (Exception e)
            {
                MessageBox.Show("Abaqus", e.Message);
            }
        }

        public void ChangeName(FileName file)
        {
            rout.historyRegionsNameRoute = rout.workingDirectory + rout.historyRegionsNameFile;

            string path = rout.historyRegionsNameRoute;
            string name = "Assembly ASSEMBLY";
            string[] item = File.ReadLines(path)
                            .SkipWhile(line => line != name)
                            .Skip(1)
                            .Take(1)
                            .ToArray();
            rout.firstName = string.Join("", item);

            string[] items = File.ReadLines(path)
                            .SkipWhile(line => line != rout.firstName)
                            .Skip(1)
                            .TakeWhile(line => line.Length != 0)
                            .ToArray();
            rout.secondName = string.Join("", items);

            rout.ExtractIn = file.extractFileName;
            rout.ExtractOut = rout.workingDirectory + @"\" + file.extractFileName;

            StreamReader reader = new StreamReader(rout.ExtractIn);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, "@LowerPlateName", Convert.ToString(rout.firstName));
            content = Regex.Replace(content, "@UpperPlateName", Convert.ToString(rout.secondName));

            StreamWriter writer = new StreamWriter(rout.ExtractOut);
            writer.Write(content);
            writer.Close();
        }
    }
}
