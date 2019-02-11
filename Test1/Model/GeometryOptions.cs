using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test1.Model;

namespace Test1.Model
{
    public class GeometryOptions
    {
        public double pctOfDieFilling;

        public double blankChamferVolume;

        public double stampDisplacement;
        public double blankPosition;

        public double reactionForceToBlank;

        public double friction;
        public double startFriction;
        public double endFriction;

        //нижний радиус матрицы, после скругления
        public double lowerFilletRadius;

        #region Данные парметрической модели

        public double blankHeight;
        public double blankRadius;
        public double blankUpperRadius;
        public double blankVolume;

        public double stampAngle;
        public double startAngle;
        public double endAngle;
        public int stepAngle;

        public double startBlankRadius;
        public double endBlankRadius;
        public double incrBlankRadius;

        public double dieRadius;
        public double dieVolume;

        #endregion
    }
}

