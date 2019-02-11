using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Test1.Model;
using Test1.Rout;
using System.Windows.Forms;

namespace Test1.Extract_Data
{
    class ExtractRF
    {
        Dictionary<double, double> ReactionForce = new Dictionary<double, double>();

        double _reactionForce;

        public double getRF()
        {
            return _reactionForce;
        }

        public void Extract(RoutesForExtract extrRoutes, GeometryOptions model)
        {
            try
            {
                ReactionForce = File.ReadLines(extrRoutes.setReactionForceRoute).Select(line => line.Split(';')).ToDictionary(data => Convert.ToDouble(data[0]), data => Math.Abs(Convert.ToDouble(data[1])));
                ReactionForce = ReactionForce
                .Where(f => f.Value != 0)
                .ToDictionary(x => x.Key, x => x.Value);

                _reactionForce = ReactionForce.Max(y => y.Value);

                ReactionForce.Clear();

            }
            catch (Exception e)
            {
                MessageBox.Show("ExtractRF: " + e);
            }
        }
    } 
}
