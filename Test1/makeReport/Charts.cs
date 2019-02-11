using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Test1.Model;
using Test1.makeReport;
using Test1.makeReport.DrawCharts;

namespace Test1.makeReport
{
    class Charts
    {
        GeneralReport genReport = new GeneralReport();

        public Charts(GeneralReport genReport)
        {
            this.genReport = genReport;
        }

        public void DrawAngleChart()
        {
            var angle = new Angle();
            angle.AngleVolumeChart(genReport);
            angle.AngleRFChart(genReport);
        }

        public void DrawFrictionChart()
        {
            var friction = new Friction();
            friction.FrictionVolumeChart(genReport);
            friction.FrictionRFChart(genReport);
        }

        public void DrawRadiusChart()
        {
            var radius = new Radius();
            radius.RadiusVolumeChart(genReport);
            radius.RadiusRFChart(genReport);
        }
    }
}
