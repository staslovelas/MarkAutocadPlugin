using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WinForms = System.Windows.Forms;

namespace MarkAnalyzerAutocadPlagin
{
    public class MainAnalyze : IExtensionApplication
    {

        public void Initialize()
        {
            HelloWindow hForm = new HelloWindow();
            WinForms.DialogResult diaRes = Application.ShowModalDialog(hForm);
        }

        [CommandMethod("RunAnalyze")]
        public void RunAnalyze()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PluginWizard pForm = new PluginWizard();
            WinForms.DialogResult diaRes = Application.ShowModalDialog(pForm);
            if (diaRes != WinForms.DialogResult.OK) { return; }

            double mL;
            double mN;
            string mnText = pForm.MnParameter;
            string mlText = pForm.MlParameter;

            try
            {
                string decimalPoint = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                mnText = mnText.Replace(".", decimalPoint);
                mnText = mnText.Replace(",", decimalPoint);
                mN = Double.Parse(mnText);
            }
            catch (FormatException)
            {
                WinForms.MessageBox.Show("ERROR: Не удалось распознать параметр mN\n", "Ошибка");
                return;
            }

            try
            {
                string decimalPoint = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                mlText = mlText.Replace(".", decimalPoint);
                mlText = mlText.Replace(",", decimalPoint);
                mL = Double.Parse(mlText);

            }
            catch (FormatException)
            {
                WinForms.MessageBox.Show("ERROR: Не удалось распознать параметр mL\n", "Ошибка");
                return;
            }

            bool isDelete = pForm.DeleteMarks;
            bool fromDraw = pForm.FromAutocad;
            bool exToEx = pForm.ExportToExcel;
            bool expOnlyGoomMarks = pForm.ExportOnlyGoodMarks;
            string xlFileName = "";

            List<DeformationMark> defMarks = new List<DeformationMark>();
            List<Mark> bearMarks = new List<Mark>();

            if (fromDraw)
            {
                try
                {
                    GetMarksFromDrawing gmd = new GetMarksFromDrawing();
                    gmd.GetCoordinates(defMarks, bearMarks, doc, ed, db, pForm.FirstBear, pForm.SecondBear, pForm.ThirdBear);
                } catch (System.Exception e)
                {
                    WinForms.MessageBox.Show(e.Message);
                    return;
                }
            }
            else
            {
                try
                {
                    WorkWithExcel.ExcelWorker(0, defMarks, bearMarks, pForm.OpenFileName, false, pForm.FirstBear, pForm.SecondBear, pForm.ThirdBear);
                }
                catch (System.Exception e)
                {
                    WinForms.MessageBox.Show(e.Message);
                    return;
                }
                             
            }

            ComputeAllDeviations(defMarks, bearMarks, mL, mN);

            EditDrawing.EditCurrentDrawing(defMarks, bearMarks, isDelete, fromDraw, doc, db);
            if (exToEx)
            {
                try
                {
                    WorkWithExcel.ExcelWorker(1, defMarks, bearMarks, pForm.WriteFileName, expOnlyGoomMarks, pForm.FirstBear, pForm.SecondBear, pForm.ThirdBear);

                } catch (System.Exception e)
                {
                    WinForms.MessageBox.Show(e.Message);
                    return;
                }
            }

            for(int i = 0; i < defMarks.Count; i++)
            {
                ed.WriteMessage(defMarks[i].ToString() + "\n");
            }

            for (int i = 0; i < bearMarks.Count; i++)
            {
                ed.WriteMessage(bearMarks[i].ToString() + "\n");
            }
        }

        /// <summary>
        /// Function which computes all deviations for marks
        /// </summary>
        /// <param name="defMarks">List of deformation marks</param>
        /// <param name="bearMarks">List of bear marks</param>
        /// <param name="mL">mL deviation</param>
        /// <param name="mN">nm deviation</param>
        public void ComputeAllDeviations(List<DeformationMark> defMarks, List<Mark> bearMarks, double mL, double mN)
        {
            MathCalculating mc = new MathCalculating();

            Loop: for (int i = 0; i < defMarks.Count; i++)
            {
                for (int j = 0; j < bearMarks.Count; j++)
                {

                    double L = mc.PointDistance(defMarks[i].GetAllCoordinates(), bearMarks[j].GetAllCoordinates());
                    defMarks[i].SetDistance(bearMarks[j], L);

                    if (L == -1)
                    {
                       WinForms.MessageBox.Show("ERROR: Ошибка вычисления расстояний для марки " + defMarks[i].Name + ": результат меньше либо равен нулю\n", "Ошибка");
                       goto Loop;
                    }
                }

                try
                {
                    double mx = mc.XStandardDeviation(bearMarks[1].XCoordinate,
                    defMarks[i].GetDistance(bearMarks[1]),
                    defMarks[i].GetDistance(bearMarks[0]),
                    mL);
                    defMarks[i].Mx = Math.Round(mx, 3);
                    double my = mc.YStandardDeviation(bearMarks[2].YCoordinate,
                            defMarks[i].GetDistance(bearMarks[2]),
                            defMarks[i].GetDistance(bearMarks[0]),
                            mL,
                            bearMarks[2].XCoordinate,
                            mx);
                    defMarks[i].My = Math.Round(my, 3);
                    double mz = mc.ZStandardDeviation(defMarks[i].XCoordinate,
                            defMarks[i].YCoordinate,
                            defMarks[i].ZCoordinate,
                            mx,
                            defMarks[i].GetDistance(bearMarks[0]),
                            mL,
                            my);
                    defMarks[i].Mz = Math.Round(mz, 3);

                    double m = mc.GeneralErr(mx, my, mz);
                    defMarks[i].M = Math.Round(m, 3);
                    defMarks[i].DetermineStatus(mN);
                }
                catch (System.Exception)
                {
                    WinForms.MessageBox.Show("ERROR: Ошибка вычислений для марки " + defMarks[i].Name + "Ошибка");
                    throw;
                }
            }
        }
        public void Terminate() {}
    }
}
