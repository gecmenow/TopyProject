using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
//подключение библиотек SolidWorks
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
//подключение  внешних классов
using Test1.Files;
using Test1.Model;

namespace Test1.runPrograms
{
    class CAD
    {
        public void UseSolid(FileName file, GeometryOptions model)
        {

            SldWorks swApp;
            IModelDoc2 swModel;

            CloseSolid();

            object process = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("SldWorks.Application"));
            swApp = (SldWorks)process;
            swApp.Visible = false;
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.InsertSketch(true);
            swModel.ClearSelection2(true);

            #region oldStamp
            //отрисовка куба
            //swModel.SketchManager.CreateLine(-(model.dieRadius / 1000) - 0.01, (model.dieRadius / 1000) + 0.01, 0, -(model.dieRadius / 1000) - 0.01, -(model.dieRadius / 1000) - 0.01, 0);//размер куба 14 на 14
            //swModel.SketchManager.CreateLine(-(model.dieRadius / 1000) - 0.01, -(model.dieRadius / 1000) - 0.01, 0, (model.dieRadius / 1000) + 0.01, -(model.dieRadius / 1000) - 0.01, 0);
            //swModel.SketchManager.CreateLine((model.dieRadius / 1000) + 0.01, -(model.dieRadius / 1000) - 0.01, 0, (model.dieRadius / 1000) + 0.01, (model.dieRadius / 1000) + 0.01, 0);
            //swModel.SketchManager.CreateLine((model.dieRadius / 1000) + 0.01, (model.dieRadius / 1000) + 0.01, 0, -(model.dieRadius / 1000) - 0.01, (model.dieRadius / 1000) + 0.01, 0);
            //swModel.ClearSelection2(true);
            //swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            //swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, 0.07, 0.02,
            //    false, false, false, false, 1.74532925199433E-02, 1.74532925199433E-02,
            //    false, false, false, false, true, true, true, 0, 0, false);//первое значение 0.07 это высота 70мм

            ////делаем вырез
            //swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            //swModel.SketchManager.InsertSketch(true);
            //swModel.ClearSelection2(true);
            //swModel.SketchManager.CreateLine(0, 0.057, 0, -0.01, 0.057, 0);//0.057-это высота внутри 
            //swModel.SketchManager.CreateLine(-0.01, 0.057, 0, -model.dieRadius / 1000, 0, 0);//-0.01-верхний радиус
            //swModel.SketchManager.CreateLine(-model.dieRadius / 1000, 0, 0, 0, 0, 0);//-0.0046-нижний радиус
            //swModel.SketchManager.CreateLine(0, 0, 0, 0, 0.057, 0);
            //swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            //swModel.ClearSelection2(true);
            //swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, false, 16, null, 0);
            //swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, 6.2831853071796, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            //swModel.SaveAs3(file.stampFolder, 0, 0);//путь сохранения

            //swApp.NewPart();
            //swModel = swApp.IActiveDoc2;
            //swApp.CloseDoc("Деталь1.SLDPRT");

            #endregion

            double upperRadius;
            double angle30X;
            double angle30Y;
            double filletLength;

            //3 - радиус скругления, 90 - градус
            filletLength = Math.Tan((180 - (((90 + model.stampAngle) / 2) + 90)) * Math.PI/180) * 0.003;

            upperRadius = model.dieRadius / 1000 + filletLength + 0.01;

            angle30X = upperRadius - 0.005;
            angle30Y = Math.Tan(30 * Math.PI / 180) * 0.005;

            //отрисовка куба
            swModel.SketchManager.CreateLine(0.01, 0.06, 0, 0, 0.06, 0);//0.6 высота внутри 0,1 радиус верхний
            swModel.SketchManager.CreateLine(0, 0.06, 0, 0, 0.065, 0);//0,65 высота общая
            swModel.SketchManager.CreateLine(0, 0.065, 0, upperRadius, 0.065, 0);//0.02799831333 внешний радиус сверху
            swModel.SketchManager.CreateLine(upperRadius, 0.065, 0, upperRadius, angle30Y, 0);//высота до угла 30   
            swModel.SetInferenceMode(false);
            swModel.SketchManager.CreateLine(upperRadius, angle30Y, 0, angle30X, 0, 0);//под углом 30            
            swModel.SketchManager.CreateLine(angle30X, 0, 0, model.dieRadius / 1000, 0, 0);//прямая до скругления
            swModel.SetInferenceMode(true);
            swModel.SketchManager.CreateLine(model.dieRadius / 1000, 0, 0, 0.01, 0.06, 0);//усеченный конус
            swModel.Extension.SelectByID2("Point6", "SKETCHPOINT", model.dieRadius / 1000, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateFillet(0.003, 1);
            swModel.Extension.SelectByID2("Point1", "SKETCHPOINT", 0.01, 0.06, 0, false, 0, null, 0);
            swModel.SketchManager.CreateFillet(0.002, 1);
            swModel.Extension.SelectByID2("Line5", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.SketchAddConstraints("sgFIXED");
            swModel.Extension.SelectByID2("Line6", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.SketchAddConstraints("sgFIXED");
            swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, false, false, false, 0, 0, 6.2831853071796, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.SaveAs3(file.stampFolder, 0, 0);//путь сохранения

            swApp.NewPart();
            swModel = swApp.IActiveDoc2;
            swApp.CloseDoc("Деталь1.SLDPRT");

            //строим цилиндр
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.InsertSketch(true);
            swModel.ClearSelection2(true);
            swModel.SketchManager.CreateLine(0, 0, 0, 0, model.blankHeight/1000, 0);//0.018-это высота
            //                                                                     радиус заготовки с учетом фаски
            swModel.SketchManager.CreateLine(0, model.blankHeight / 1000, 0, -model.blankUpperRadius / 1000, model.blankHeight / 1000, 0);//-0.0046- радиус
            swModel.SetInferenceMode(false);
            swModel.SketchManager.CreateLine(-model.blankUpperRadius / 1000, model.blankHeight / 1000, 0, -model.blankRadius / 1000, model.blankHeight / 1000 - 0.002, 0);//0.0001748-tan 5 градусов * высоту фаски
            swModel.SetInferenceMode(true);
            swModel.SketchManager.CreateLine(-model.blankRadius / 1000, model.blankHeight / 1000 - 0.002, 0, -model.blankRadius / 1000, 0, 0);//0.016 высота на 2 мм(высота фаски) ниже
            swModel.SketchManager.CreateLine(-model.blankRadius / 1000, 0, 0, 0, 0, 0);
            swModel.Extension.SelectByID2("Line3", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.SketchAddConstraints("sgFIXED");
            swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", -3.11882415580314E-03, 0, 0, true, 0, null, 0);
            ///


            swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", -5.49748992413931E-04, 1.70942234292196E-02, -1.35495829391757E-03, true, 0, null, 0);
            swModel.ClearSelection2(true);
            swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", -5.49748992413931E-04, 1.70942234292196E-02, -1.35495829391757E-03, false, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, false, false, false, 0, 0, 6.2831853071796, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.SaveAs3(file.blankFolder, 0, 0);//путь сохранения

            swApp.NewPart();
            swModel = swApp.IActiveDoc2;
            swApp.CloseDoc("Деталь2.SLDPRT");

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.InsertSketch(true);
            swModel.ClearSelection2(true);

            swModel.SketchManager.CreateLine(-(model.dieRadius / 1000) - 0.01, (model.dieRadius / 1000) + 0.01, 0, -(model.dieRadius / 1000) - 0.01, -(model.dieRadius / 1000) - 0.01, 0);//размер площадки 20 на 20
            swModel.SketchManager.CreateLine(-(model.dieRadius / 1000) - 0.01, -(model.dieRadius / 1000) - 0.01, 0, (model.dieRadius / 1000) + 0.01, -(model.dieRadius / 1000) - 0.01, 0);
            swModel.SketchManager.CreateLine((model.dieRadius / 1000) + 0.01, -(model.dieRadius / 1000) - 0.01, 0, (model.dieRadius / 1000) + 0.01, (model.dieRadius / 1000) + 0.01, 0);
            swModel.SketchManager.CreateLine((model.dieRadius / 1000) + 0.01, (model.dieRadius / 1000) + 0.01, 0, -(model.dieRadius / 1000) - 0.01, (model.dieRadius / 1000) + 0.01, 0);

            swModel.ClearSelection2(true);
            swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, 0.001, 0.02,
                false, false, false, false, 1.74532925199433E-02, 1.74532925199433E-02,
                false, false, false, false, true, true, true, 0, 0, false);//первое значение 0.01 это высота 1мм
            swModel.SaveAs3(file.platformFolder, 0, 0);//путь сохранения

            Thread.Sleep(1000);

            CloseSolid();
        }

        public void CloseSolid()
        {
            //закрытие приложения, если оно открыто
            Process[] processes = Process.GetProcessesByName("SLDWORKS");
            foreach (Process p in processes)
            {
                p.CloseMainWindow();
                p.Kill();
            }
        }
    } 
}

