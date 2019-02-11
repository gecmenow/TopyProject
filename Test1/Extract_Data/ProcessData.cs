using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Test1.Model;
using Test1.Files;
using Test1.Rout;
using Test1.Volume;
using Test1.makeReport;


namespace Test1.Extract_Data
{
    class ProcessData
    {
        Routes rout = new Routes();
        ArgumentsCMD cmd = new ArgumentsCMD();

        public ProcessData(Routes rout, ArgumentsCMD cmd)
        {
            this.rout = rout;
            this.cmd = cmd;
        }

        #region Обработка полученных результатов

        public void Processing(GeometryOptions model, FileName file, FilesForVolume filesForVolume, GeneralReport genReport)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                var extract = new RunExtract(rout);

                extract.ExtractName(cmd.extractName, file);

                extract.ChangeName( file);

                extract.ExtractFiles(cmd.extractFiles);

                extract.ExtractNodes(cmd.extractNodes, file);

                var extrRoutes = new RoutesForExtract();

                extrRoutes.setReactionForceRoute = rout.workingDirectory + @"\RF3Stamp.csv";

                var extrRF = new ExtractRF();
                extrRF.Extract(extrRoutes, model);

                var reprRF = new ReactionForceReport();
                reprRF.ReportReactionForce(model, genReport, file, extrRF);

                filesForVolume.stampNodeDisplacementRoute = rout.workingDirectory + @"\" + filesForVolume.stampNodeDisplacementName;
                filesForVolume.blankNodeDisplacementRoute = rout.workingDirectory + @"\" + filesForVolume.blankNodeDisplacementName;
                filesForVolume.blankCoordinatesRoute = rout.workingDirectory + @"\" + filesForVolume.blankCoordinatesName;
                filesForVolume.elementCoordinatesRoute = rout.workingDirectory + @"\" + filesForVolume.elementCoordinatesName;
                filesForVolume.stampNodesRoute = rout.workingDirectory + @"\" + filesForVolume.stampNodesName;
                filesForVolume.elementVolumeRoute = rout.workingDirectory + @"\" + filesForVolume.elementVolumeName;

                var extrVolume = new ExtractVolume();
                
                //extrVolume.GetStampNodesDisplacement(filesForVolume);
                extrVolume.GetBlankNodesDisplacement(filesForVolume);
                //extrVolume.GetBlanksNodeCoordinates(filesForVolume);
                //extrVolume.GetElementCoordinates(filesForVolume, model);
                extrVolume.Volume(model);

                var reprVolume = new VolumeReport();
                reprVolume.ReportVolume(model, genReport, file, extrVolume);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion
    }
}
