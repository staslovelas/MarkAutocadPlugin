using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Linq;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;

namespace MarkAnalyzerAutocadPlagin
{
    /// <summary>
    /// Class with methods for readinf data from autocad draw
    /// </summary>
    public class GetMarksFromDrawing
    {
        /// <summary>
        /// Common function for getting marks from drawing
        /// </summary>
        /// <param name="defMarks">List for deformation marks</param>
        /// <param name="bearMarks">List for bearing marks</param>
        /// <param name="doc">Current autocad document</param>
        /// <param name="ed">Current autocad editor</param>
        /// <param name="db">Current autocad database</param>
        /// <param name="frstBear">The name of first bear marks</param>
        /// <param name="scndBear">The name of second bear marks</param>
        /// <param name="thrdBear">The name of second bear marks</param>
        public void GetCoordinates(List<DeformationMark> defMarks, List<Mark> bearMarks, Document doc, Editor ed, Database db, string frstBear, string scndBear, string thrdBear)
        {
            List<ObjectId> pointsIds = CreateEntityFilter(ed, "POINT");
            List<ObjectId> namesIds = CreateEntityFilter(ed, "TEXT");
            List<ObjectId> freePoints = new List<ObjectId>(pointsIds);

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //Here we will get all names from the drawing
                foreach (ObjectId nameId in namesIds)
                {
                    DBText name = (DBText)tr.GetObject(nameId, OpenMode.ForRead);

                    if (name.TextString.Trim().Equals(frstBear) ||
                        name.TextString.Trim().Equals(scndBear) ||
                        name.TextString.Trim().Equals(thrdBear))
                    {
                        Mark newBearMark = new Mark
                        {
                            Name = name.TextString.Trim(),
                            NameId = nameId
                        };

                        bearMarks.Add(newBearMark);
                    }
                    else
                    {
                        DeformationMark newDefMark = new DeformationMark
                        {
                            Name = name.TextString.Trim(),
                            NameId = nameId
                        };

                        defMarks.Add(newDefMark);
                    }
                }

                foreach (ObjectId pointId in pointsIds)
                {
                    DBPoint point = (DBPoint)tr.GetObject(pointId, OpenMode.ForRead);

                    Matrix3d mat = ed.CurrentUserCoordinateSystem.Inverse();

                    Point3d wcsPoint = point.Position;
                    Point3d ucsPoint = wcsPoint.TransformBy(mat);

                    foreach (DeformationMark mark in defMarks)
                    {
                        DBText name = (DBText)tr.GetObject(mark.NameId, OpenMode.ForRead);
                        if (ContainMarkAndName(ucsPoint, name, ed))
                        {
                            mark.XCoordinate = Math.Round(ucsPoint.X, 5);
                            mark.YCoordinate = Math.Round(ucsPoint.Y, 5);
                            mark.ZCoordinate = Math.Round(ucsPoint.Z, 5);

                            mark.PointId = pointId;
                            freePoints.Remove(pointId);
                            break;
                        }
                    }
                }

                foreach(ObjectId pointId in freePoints)
                {
                    DBPoint point = (DBPoint)tr.GetObject(pointId, OpenMode.ForRead);
                    Matrix3d mat = ed.CurrentUserCoordinateSystem.Inverse();

                    Point3d wcsPoint = point.Position;
                    Point3d ucsPoint = wcsPoint.TransformBy(mat);

                    foreach (Mark mark in bearMarks)
                    {
                        DBText name = (DBText)tr.GetObject(mark.NameId, OpenMode.ForRead);
                        if (ContainMarkAndName(ucsPoint, name, ed))
                        {
                            mark.XCoordinate = Math.Round(ucsPoint.X, 5);
                            mark.YCoordinate = Math.Round(ucsPoint.Y, 5);
                            mark.ZCoordinate = Math.Round(ucsPoint.Z, 5);

                            mark.PointId = pointId;
                        }
                    }
                }
                bearMarks.Sort(Comparer<Mark>.Create((x, y) => x.Name.CompareTo(y.Name)));
                tr.Commit();
            }
        }

        /// <summary>
        /// Function for searchig object of a certain type on autocad draw
        /// </summary>
        /// <param name="ed">Current autocad editor</param>
        /// <param name="type">Type of object</param>
        /// <returns></returns>
        public static List<ObjectId> CreateEntityFilter(Editor ed, string type)
        {
            TypedValue[] filterlist = new TypedValue[1];
            filterlist[0] = new TypedValue(0, type);
            SelectionFilter filter = new SelectionFilter(filterlist);
            ObjectId[] ids = { };
            try
            {
                PromptSelectionResult selRes = ed.SelectAll(filter);
                ids = selRes.Value.GetObjectIds();

            } catch (Exception e)
            {
                throw new Exception("ERROR: не удалось найти примитивы " + type + " на чертеже");
            }

            List<ObjectId> listOfIds = ids.OfType<ObjectId>().ToList();
            return listOfIds;
        }

        /// <summary>
        /// Funtion for finding coordinates of the point for each text from drawing
        /// </summary>
        /// <param name="point">Point primitive from draw</param>
        /// <param name="name">Text object from draw</param>
        /// <param name="ed">Current autocad editor</param>
        /// <returns></returns>
        public static bool ContainMarkAndName(Point3d point, DBText name, Editor ed)
        {
            Matrix3d mat = ed.CurrentUserCoordinateSystem.Inverse();
            Point3d wcsCoords = name.Position;
            Point3d ucsCoords = wcsCoords.TransformBy(mat);

            double nameX = ucsCoords.X;
            double nameY = ucsCoords.Y;
            double nameZ = ucsCoords.Z;

            MathCalculating mc = new MathCalculating();

            if (mc.IsDoubleEqual(nameX, point.X, 0.00001) & mc.IsDoubleEqual(nameY, point.Y, 0.00001) & mc.IsDoubleEqual(nameZ, point.Z, 0.00001))
            {
                return true;
            }
            return false;
        }
    }
}
