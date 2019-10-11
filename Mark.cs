using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace MarkAnalyzerAutocadPlagin
{
    public class Mark
    {
        private double xCoordinate;
        private double yCoordinate;
        private double zCoordinate;
        private string name;

        //Ids of objects on autocad drawing
        private ObjectId pointId;
        private ObjectId nameId;

        public double XCoordinate { get => xCoordinate; set => xCoordinate = value; }
        public double YCoordinate { get => yCoordinate; set => yCoordinate = value; }
        public double ZCoordinate { get => zCoordinate; set => zCoordinate = value; }
        public string Name { get => name; set => name = value; }
        public ObjectId PointId { get => pointId; set => pointId = value; }
        public ObjectId NameId { get => nameId; set => nameId = value; }

        public Mark() { }

        public Mark(string name, double xCoordinate, double yCoordinate, double zCoordinate)
        {
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;
            this.ZCoordinate = zCoordinate;
            this.Name = name;
        }

        /// <summary>
        /// Returns  coordinates of the mark as a list
        /// </summary>
        /// <returns>List of coordinates</returns>
        public List<double> GetAllCoordinates()
        {
            List<double> coordinates = new List<double>
            {
                XCoordinate,
                YCoordinate,
                ZCoordinate
            };

            return coordinates;
        }

        /// <summary>
        /// Contains the names of two different marks
        /// If they are equal then marks are equal too
        /// </summary>
        /// <param name="other">Other mark</param>
        /// <returns>Result of the comparison</returns>
        public bool Equals(Mark other)
        {
            if (this.Name.CompareTo(other.Name) == 0)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Combines some fiels of the mark in one string
        /// </summary>
        /// <returns>String with name and coordinates</returns>
        public override string ToString()
        {
            return "name = " + Name + " x= " + XCoordinate + " y= " + YCoordinate + " z= " + ZCoordinate;
        }
    }
}
