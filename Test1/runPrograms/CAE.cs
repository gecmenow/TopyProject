using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Test1.Rout;

namespace Test1.runPrograms
{
    //отвечает за запуск абакуса      
    class CAE
    {
        const string name = "ABQcaeG";//процесс, который нужно закрыть
        public void runAbaqus(Routes rout, ArgumentsCMD args)
        {
            try
            {
                Process[] etc = Process.GetProcesses();//получим процессы
                foreach (Process anti in etc)//обойдем каждый процесс
                    if (anti.ProcessName.ToLower().Contains(name.ToLower())) anti.Kill();//переводим имя в нижний регистр, на всякий пожарный

                var proc = new Process();
                proc.StartInfo.UseShellExecute = false; //нужно для правильного запуска консоли, но т.к. мы явно указываем консоль, то оно особо и не нужно
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                proc.StartInfo.WorkingDirectory = rout.workingDirectory;//рабочая дериктория, чтобы консоль видела макрос
                proc.StartInfo.FileName = Path.Combine(rout.cmdEXE, "cmd.exe");
                //FileName = @"C:\Windows\System32\cmd.exe",
                proc.StartInfo.Arguments = "/" + rout.systemDisk + args.runAbaqus; //передаем команду в консоль
                proc.StartInfo.CreateNoWindow = true; // Скрывает консоль

                Form1._Form1.LogArea("Начался расчёт...\n");

                proc.Start();

                StreamReader writer = proc.StandardOutput;

                string output = writer.ReadToEnd();
                // Write the redirected output to this application's window.
                File.WriteAllText(rout.workingDirectory + @"\log.txt", output);

                #region Окно лога, которое пишет с консоли

                Form1._Form1.LogArea(output);

                #endregion

                proc.WaitForExit();
                proc.Close();

                //var proc = new ProcessStartInfo()
                //{
                //    UseShellExecute = true, //нужно для правильного запуска консоли, но т.к. мы явно указываем консоль, то оно особо и не нужно
                //    WorkingDirectory = rout.setWorkingDirectory,//рабочая дериктория, чтобы консоль видела макрос
                //    FileName = Path.Combine(rout.setCmdEXE, "cmd.exe"),
                //    //FileName = @"C:\Windows\System32\cmd.exe",
                //    Arguments = "/C " + rout.setCmd, //передаем команду в консоль
                //    //WindowStyle = ProcessWindowStyle.Hidden
                //};

                //Process.Start(proc).WaitForExit();

                //string result = Process.Start(proc).StandardOutput.ReadToEnd();

                //File.WriteAllText("log.txt", result);
            }
            catch (Exception e)
            {
                MessageBox.Show("Abaqus" + e.Message);
            }
        }
    }
}
