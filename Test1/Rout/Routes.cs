using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Rout
{
    //Пути для запуска абакуса, скриптов и их изменения.
    class Routes
    {
        public string pathIn;
        public string pathOut;

        public string workingDirectory;
        public string systemDisk;
        public string cmdEXE;

        //Считывание имени для извлечения данных из csv
        public string firstName;
        public string secondName;

        //Путь для получения имени матрицы(верхней давящей части)
        public string historyRegionsNameFile;

        public string historyRegionsNameRoute;

        public string ExtractIn;
        public string ExtractOut;

        public string nameIn;
        public string nameOut;

        public string nodeIn;
        public string nodeOut;
    }
}
