using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Test1.Model;
using Test1.makeReport.DrawCharts;

namespace Test1.makeReport
{
    class GeneralReport
    {
        public string folderName;

        public Dictionary<double, double> AngleVolume = new Dictionary<double, double>();
        public Dictionary<double, double> AngleReactionForce = new Dictionary<double, double>();

        public Dictionary<double, double> FrictionVolume = new Dictionary<double, double>();
        public Dictionary<double, double> FrictionReactionForce = new Dictionary<double, double>();

        public Dictionary<double, double> RadiusVolume = new Dictionary<double, double>();
        public Dictionary<double, double> RadiusReactionForce = new Dictionary<double, double>();

        public void AddRadiusToChart(GeometryOptions model)
        {
            if (!RadiusVolume.Keys.Contains(model.blankRadius))
            {
                RadiusVolume.Add(model.blankRadius, model.pctOfDieFilling);
            }

            if (!RadiusReactionForce.Keys.Contains(model.blankRadius))
            {
                RadiusReactionForce.Add(model.blankRadius, model.reactionForceToBlank);
            }
        }

        public void AddFrictionToChart(GeometryOptions model)
        {
            if (!FrictionVolume.Keys.Contains(model.friction))
            {
                FrictionVolume.Add(model.friction, model.pctOfDieFilling);
            }

            if (!FrictionReactionForce.Keys.Contains(model.friction))
            {
                FrictionReactionForce.Add(model.friction, model.reactionForceToBlank);
            }
        }
    }
}
