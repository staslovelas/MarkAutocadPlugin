using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using System;
using WinForms = System.Windows.Forms;

namespace MarkAnalyzerAutocadPlagin
{
    /// <summary>
    /// Class for editing AutoCADs drawings
    /// </summary>
    public class EditDrawing
    {
        /// <summary>
        /// Common function for editing any drawing, empty or not
        /// </summary>
        /// <param name="defMarks">List of deformation marks</param>
        /// <param name="bearMarks">List of bear marks</param>
        /// <param name="isDelete">Flag if user want to delete bad marks or color all marks</param>
        /// <param name="fromDrawing">Flag if data was loaded from autocad draw, not excel file</param>
        /// <param name="doc">Current autocad doc</param>
        /// <param name="db">Current autocad database</param>
        /// <param name="ed">Current autocad editor</param>
        public static void EditCurrentDrawing(List<DeformationMark> defMarks, List<Mark> bearMarks, bool isDelete, bool fromDrawing, Document doc, Database db)
        {
            using (Transaction newTrans = db.TransactionManager.StartTransaction())
            {
                switch (fromDrawing)
                {
                    case true:
                        EditExistingDrawing(isDelete, newTrans, defMarks);
                        break;
                    case false:
                        EditNewDrawing(isDelete, newTrans, db, defMarks, bearMarks);
                        break;
                    default:
                        break;
                }
                newTrans.Commit();
            }
        }

        /// <summary>
        /// Function for creating new mark primitive in current model space 
        /// during current transaction
        /// </summary>
        /// <param name="trans">Current transaction</param>
        /// <param name="modSpace">Current model space</param>
        /// <param name="name">The name of mark</param>
        /// <param name="x">X coordinate of mark</param>
        /// <param name="y">Y coordinate of mark</param>
        /// <param name="z">Z coordinate of mark</param>
        /// <param name="color">Special color of the primitive</param>
        static void CreateMarkPrimitive(Transaction trans, BlockTableRecord modSpace, string name, double x, double y, double z, Color color)
        {
            //Create point primitive
            DBPoint newPoint = CreatePointPrimitive(x, y, z, color);
            DBText newText = CreateTextPrimitive(name, x, y, z, color, 0.5);
            
            //Add primitives to transaction and model space
            modSpace.AppendEntity(newPoint);
            modSpace.AppendEntity(newText);
            trans.AddNewlyCreatedDBObject(newPoint, true);
            trans.AddNewlyCreatedDBObject(newText, true);
        }

        /// <summary>
        /// Function for creatin text primitive on autocad drawing
        /// </summary>
        /// <param name="text">String text</param>
        /// <param name="x">X coordinate of text</param>
        /// <param name="y">Y coordinate of text</param>
        /// <param name="z">Z coordinate of text</param>
        /// <param name="height">Height of text</param>
        /// <param name="color">Color of text</param>
        /// <returns></returns>
        static DBText CreateTextPrimitive(string text, double x, double y, double z, Color color, double height)
        {
            DBText acadText = new DBText();

            acadText.SetDatabaseDefaults();
            acadText.Position = new Point3d(x, y, z);
            acadText.Height = height;
            acadText.TextString = text;
            acadText.Color = color;

            return acadText;
        }

        /// <summary>
        /// Function for creating point primitive on autocad drawing
        /// </summary>
        /// <param name="x">X coordinate of point</param>
        /// <param name="y">Y coordinate of point</param>
        /// <param name="z">Z coordinate of point</param>
        /// <param name="color">Color of point</param>
        /// <returns></returns>
        static DBPoint CreatePointPrimitive(double x, double y, double z, Color color)
        {
            DBPoint acadPoint = new DBPoint(new Point3d(x, y, z));

            acadPoint.SetDatabaseDefaults();
            acadPoint.Color = color;

            return acadPoint;
        }

        /// <summary>
        /// Creates marks if user chose to delete bad marks
        /// </summary>
        /// <param name="mark">Current deformation mark</param>
        /// <param name="trans">Current transaction</param>
        /// <param name="modSpace">Current model space</param>
        static void CreateIfDelete(DeformationMark mark, Transaction trans, BlockTableRecord modSpace)
        {
            if (mark.Status == true)
            {
                CreateMarkPrimitive(trans, modSpace, mark.Name, mark.XCoordinate, mark.YCoordinate, mark.ZCoordinate, Color.FromRgb(0, 0, 0));
            }
            else
                return;
        }

        /// <summary>
        /// Creates marks if user chose to color marks
        /// </summary>
        /// <param name="mark">Current deformation mark</param>
        /// <param name="trans">Current transaction</param>
        /// <param name="modSpace">Current model space</param>
        static void CreateIfColor(DeformationMark mark, Transaction trans, BlockTableRecord modSpace)
        {
            if (mark.Status == true)
            {
                CreateMarkPrimitive(trans, modSpace, mark.Name, mark.XCoordinate, mark.YCoordinate, mark.ZCoordinate, Color.FromRgb(0, 255, 0));
            }
            else
            {
                CreateMarkPrimitive(trans, modSpace, mark.Name, mark.XCoordinate, mark.YCoordinate, mark.ZCoordinate, Color.FromRgb(255, 0, 0));
            }
        }

        /// <summary>
        /// Funtion for creating new drawing
        /// </summary>
        /// <param name="mode">Flag if user want color marks or delete</param>
        /// <param name="newTrans">Current transaction</param>
        /// <param name="db">Current autocad database</param>
        /// <param name="defMarks">List of deformation marks</param>
        /// <param name="bearMarks">List of bearing marks</param>
        /// <param name="ed">Current autocad editor</param>
        static void EditNewDrawing(bool mode, Transaction newTrans, Database db, List<DeformationMark> defMarks, List<Mark> bearMarks)
        {
            BlockTable blockTable = null;
            BlockTableRecord ms = null;

            try
            {
                blockTable = newTrans.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                ms = newTrans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
            }
            catch (Exception)
            {
                WinForms.MessageBox.Show("ERROR: Не удалось получить доступ к пространству модели", "Ошибка");
                throw;
            }

            foreach (DeformationMark mark in defMarks)
            {
                switch (mode)
                {
                    //If user wants to delete bad marks
                    case true:
                        CreateIfDelete(mark, newTrans, ms);
                        break;
                    //If user wants to color marks
                    case false:
                        CreateIfColor(mark, newTrans, ms);
                        break;
                    default:
                        break;
                }
            }

            foreach (Mark mark in bearMarks)
            {
                CreateMarkPrimitive(newTrans, ms, mark.Name, mark.XCoordinate, mark.YCoordinate, mark.ZCoordinate, Color.FromRgb(0, 0, 0));
            }
            //The format of the points 
            db.Pdmode = 32;
            db.Pdsize = 1;          
        }

        /// <summary>
        /// Common function for edit loaded autocad drawing
        /// </summary>
        /// <param name="mode">Flag delete bad marks (true) or color (false)</param>
        /// <param name="trans">Current transaction</param>
        /// <param name="defMarks">List of deformation marks</param>
        static void EditExistingDrawing(bool mode, Transaction trans, List<DeformationMark> defMarks)
        {
            foreach (DeformationMark mark in defMarks)
            {
                switch (mode)
                {
                    case true:
                        EditIfDelete(mark, trans);
                        break;
                    case false:
                        EditIfColor(mark, trans);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Function for changing color of the text primitive on autocad drawing
        /// </summary>
        /// <param name="text">Text object</param>
        /// <param name="colorIndex">Index of color: 1- red, 3-green</param>
        public static void ColorText(DBText text, int colorIndex)
        {          
            text.UpgradeOpen();
            text.ColorIndex = colorIndex;       
        }

        /// <summary>
        /// Function for changing color of the point primitive on autocad drawing
        /// </summary>
        /// <param name="point">Point object</param>
        /// <param name="colorIndex">Index of color: 1- red, 3-green</param>
        public static void ColorPoint(DBPoint point, int colorIndex)
        {
            point.UpgradeOpen();
            point.ColorIndex = colorIndex;
        }

        /// <summary>
        /// Function for deleting the text object from autocad drawing 
        /// </summary>
        /// <param name="text">Text object</param>
        public static void DeleteText(DBText text)
        {
            text.UpgradeOpen();
            text.Erase();
        }

        /// <summary>
        /// Function for deleting the text object from autocad drawing 
        /// </summary>
        /// <param name="point">Point object</param>
        public static void DeletePoint(DBPoint point)
        {
            point.UpgradeOpen();
            point.Erase();
        }

        /// <summary>
        /// Function for editing autocad draw id user want to delete bad marks
        /// </summary>
        /// <param name="mark">Current deformation mark</param>
        /// <param name="trans">Current transaction</param>
        public static void EditIfDelete(DeformationMark mark, Transaction trans)
        {
            DBText name = null;
            DBPoint point = null;

            try
            {
                name = (DBText)trans.GetObject(mark.NameId, OpenMode.ForRead);
                point = (DBPoint)trans.GetObject(mark.PointId, OpenMode.ForRead);

            }
            catch (Exception)
            {
                WinForms.MessageBox.Show("ERROR: Не удалось найти на чертеже примитивы для марки " + mark.Name, "Ошибка");
                throw;
            }

            if (mark.Status == false)
            {
                DeleteText(name);
                DeletePoint(point);
            }
            else
                return;
        }

        /// <summary>
        /// Function for editing autocad draw if user want to color marks
        /// </summary>
        /// <param name="mark">Current deformation mark</param>
        /// <param name="trans">Current transaction</param>
        public static void EditIfColor(DeformationMark mark, Transaction trans)
        {
            DBText name = null;
            DBPoint point = null;

            try
            {
                name = (DBText)trans.GetObject(mark.NameId, OpenMode.ForRead);
                point = (DBPoint)trans.GetObject(mark.PointId, OpenMode.ForRead);

            }
            catch (Exception)
            {
                WinForms.MessageBox.Show("ERROR: Не удалось найти на чертеже примитивы для марки " + mark.Name, "Ошибка");
                throw;
            }

            if (mark.Status == false)
            {
                ColorText(name, 1);
                ColorPoint(point, 1);
            }
            else
            {
                ColorText(name, 3);
                ColorPoint(point, 3);
            }
        }
    }
}

