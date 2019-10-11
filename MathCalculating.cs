using System;
using System.Collections.Generic;


namespace MarkAnalyzerAutocadPlagin
{
    class MathCalculating
    {
        public double PointDistance(List<double> newCoords, List<double> oldCoords)
        {
            double result = 0;

            for (int i = 0; i < 3; i++)
            {
                result += Math.Pow(oldCoords[i] - newCoords[i], 2);
            }

            if(result <= 0)
            {
                return -1;
            }

            return Math.Sqrt(result);
        }

        public double XStandardDeviation(double x2, double L2, double L1, double mL)
        {
            double result = Math.Sqrt(Math.Pow(L1, 2) + Math.Pow(L2, 2)) * (mL / x2); ;
            return result;
        }

        public double YStandardDeviation(double y3, double L3, double L1, double mL, double x3, double mx)
        {
            double result = Math.Sqrt(((Math.Pow(L1, 2) + Math.Pow(L3, 2)) * Math.Pow(mL, 2)) + Math.Pow((x3 * mx), 2)) / y3;
            return result;
        }

        public double ZStandardDeviation(double x, double y, double z, double mx, double L1, double mL, double my)
        {
            double result = Math.Sqrt(Math.Pow((L1 * mL), 2) + Math.Pow((x * mx), 2) + Math.Pow((y * my), 2)) / z;
            return result;
        }

        public double GeneralErr(double mx, double my, double mz)
        {
            double result = Math.Sqrt(Math.Pow(mx, 2) + Math.Pow(my, 2) + Math.Pow(mz, 2));
            return result;
        }

        public bool IsDoubleEqual(double firstValue, double secondValue, double maxDifferenceAllowed)
        {
            return (Math.Abs(secondValue - firstValue) < maxDifferenceAllowed);
        }

    }
}
