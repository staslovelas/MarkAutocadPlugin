using System.Collections.Generic;

namespace MarkAnalyzerAutocadPlagin
{
    /// <summary>
    /// Class for deformation mark
    /// Includes parameters of deformation marks an standart actions
    /// </summary>
    public class DeformationMark : Mark
    {
        private bool status;
        private double m;
        private double mx;
        private double my;
        private double mz;

        //The distances between current mark and each bearing mark
        public Dictionary<Mark, double> distances = new Dictionary<Mark, double>();

        public bool Status { get => status; set => status = value; }
        public double Mx { get => mx; set => mx = value; }
        public double My { get => my; set => my = value; }
        public double Mz { get => mz; set => mz = value; }
        public double M { get => m; set => m = value; }

        public double GetDistance(Mark bearMark)
        {
                return distances[bearMark];
        }

        public void SetDistance(Mark bearMark, double L)
        {
            this.distances.Add(bearMark, L);
        }

        public DeformationMark() { }

        public DeformationMark(string name, double xCoordinate, double yCoordinate, double zCoordinate) : base(name, xCoordinate, yCoordinate, zCoordinate) { }

        /// <summary>
        /// Finds actual mark
        /// </summary>
        /// <param name="m">General error of the current mark </param>
        /// <param name="mN">Normative value of coordinate measurement deviation</param>
        public void DetermineStatus(double mN)
        {
            if (this.m <= mN)
            {
                this.Status = true;
            }
            else
                this.Status = false;
        }
    }
}
