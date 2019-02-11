using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Rout
{
    class RoutesForExtract
    {
        private string _routeStressComponnets;
        private string _routeReactionForce;
        

        public string setStressComponnetsRoute
        {
            get { return _routeStressComponnets; }
            set { _routeStressComponnets = value; }
        }

        public string setReactionForceRoute
        {
            get { return _routeReactionForce; }
            set { _routeReactionForce = value; }
        }

       
    }
}
