using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
//
using Test1.Model;
using Test1.Materials;
using Test1.Additionally;
using Test1.Rout;
using Test1.Files;
using Test1.Extract_Data;

namespace Test1.ChangeWords
{
    //Замена значений для построения
    class Change
    {
        public void Output(Routes rout, GeometryOptions model, MaterialsOptions material, AdditionallyOptions add, FileName file)
        {
            try
            {
                StreamReader reader = new StreamReader(rout.pathIn);
                string content = reader.ReadToEnd();
                reader.Close();

                content = Regex.Replace(content, "@StampFolder", Convert.ToString(file.stampFolder));
                content = Regex.Replace(content, "@BlankFolder", Convert.ToString(file.blankFolder));
                content = Regex.Replace(content, "@PlatformFolder", Convert.ToString(file.platformFolder));
                content = Regex.Replace(content, "@blankHeight", Convert.ToString(model.blankHeight));
                content = Regex.Replace(content, "@materialDensity", Convert.ToString(material.materialDensity));
                content = Regex.Replace(content, "@YoungsModulus", Convert.ToString(material.youngsModulus));
                content = Regex.Replace(content, "@PoissonsRatio", Convert.ToString(material.poissonsRatio));
                content = Regex.Replace(content, "@Plastic", material.getPlastic());
                content = Regex.Replace(content, "@DieRadius", Convert.ToString(model.dieRadius));
                content = Regex.Replace(content, "@Displacement", Convert.ToString(model.stampDisplacement));
                content = Regex.Replace(content, "@Friction", Convert.ToString(model.friction));
                content = Regex.Replace(content, "@Mesh", Convert.ToString(add.mesh));
                content = Regex.Replace(content, "@Name", add.jobName);
                content = Regex.Replace(content, "@Core", Convert.ToString(add.numberOfCores));
                content = Regex.Replace(content, "@caeSave", Convert.ToString(file.caeFolder));
                content = Regex.Replace(content, "@blankPosition", Convert.ToString(model.blankPosition));

                StreamWriter writer = new StreamWriter(rout.pathOut);
                writer.Write(content);
                writer.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Change: ", e.Message);
            }
        }
    }
}
