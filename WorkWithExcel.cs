using System;
using Excel = Microsoft.Office.Interop.Excel;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;

namespace MarkAnalyzerAutocadPlagin
{
    /// <summary>
    /// Class for parsing excel files and creating new excel files
    /// </summary>
    public class WorkWithExcel
    {
        /// <summary>
        /// Common function in which called functions for reading file or writing to file
        /// </summary>
        /// <param name="mode">Mode: 0 - read, 1 - write</param>
        /// <param name="defMarks">List for writing or reading deformation marks</param>
        /// <param name="bearMarks">List of writing or readin bear marks</param>
        /// <param name="fileName">Name of file to read or write</param>
        /// <param name="expOnlyGoodMarks">Flag if you want write to file only good marks, not used for reading</param>
        /// <param name="frstBear">The name of first bear mark, not used for writing</param>
        /// <param name="scndBear">The name of second bear mark, not used for writing</param>
        /// <param name="thrdBear">The name of third bear mark, not used for writing</param>
        public static void ExcelWorker(int mode, List<DeformationMark> defMarks, List<Mark> bearMarks, string fileName, bool expOnlyGoodMarks, string frstBear, string scndBear, string thrdBear)
        {
            Excel.Application xlApp = null;

            xlApp = new Excel.Application();

            switch (mode)
            {
                case 0:
                    {
                        ExcelReader(fileName, xlApp, defMarks, bearMarks, frstBear, scndBear, thrdBear);
                        break;
                    }
                case 1:
                    {
                        ExcelWriter(defMarks, bearMarks, fileName, expOnlyGoodMarks, xlApp);
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Function for reading marks from excel file
        /// </summary>
        /// <param name="fileName">The name of file to read</param>
        /// <param name="xlApp">The object of Excel app</param>
        /// <param name="defMarks">A list for deformation marks</param>
        /// <param name="bearMarks">A list for bearing marks</param>
        /// <param name="frstBear">The name of first bear mark</param>
        /// <param name="scndBear">The name of second bear mark</param>
        /// <param name="thrdBear">The name of third bear mark</param>
        public static void ExcelReader(string fileName, Excel.Application xlApp, List<DeformationMark> defMarks, List<Mark> bearMarks, string frstBear, string scndBear, string thrdBear)
        {
            Excel.Workbooks xlWorkBooks = null;
            Excel.Workbook xlWorkBook = null;
            Excel.Worksheet xlWorkSheet = null;

            xlWorkBooks = xlApp.Workbooks;

            try
            {
                xlWorkBook = xlWorkBooks.Open(fileName, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                xlApp.Quit();
                ReleaseObject(xlWorkBooks);
                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                throw new Exception("ERROR: не удалось открыть файл с именем " + fileName);
            }

            try
            {
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

            }
            catch (Exception)
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
                ReleaseObject(xlWorkBooks);
                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                throw new Exception("ERROR: не удалось открыть страницу файла " + fileName + " для чтения");
            }
            var lastCell = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
            int lastRow = (int)lastCell.Row;

            for (int i = 2; i <= (int)lastCell.Row; i++)
            {
                if(xlWorkSheet.Cells[i, 1].Text.ToString().Equals(frstBear) ||
                    xlWorkSheet.Cells[i, 1].Text.ToString().Equals(scndBear) ||
                    xlWorkSheet.Cells[i, 1].Text.ToString().Equals(thrdBear))
                {
                    try
                    {
                        Mark newBearMark = new Mark(xlWorkSheet.Cells[i, 1].Text.ToString(),
                        Math.Round(Double.Parse(xlWorkSheet.Cells[i, 2].Text.ToString().Replace(",", ".")), 5),
                        Math.Round(Double.Parse(xlWorkSheet.Cells[i, 3].Text.ToString().Replace(",", ".")), 5),
                        Math.Round(Double.Parse(xlWorkSheet.Cells[i, 4].Text.ToString().Replace(",", ".")), 5));
                        bearMarks.Add(newBearMark);
                    } catch (Exception e)
                    {
                        xlWorkBook.Close(true, Type.Missing, Type.Missing);
                        xlApp.Quit();
                        ReleaseObject(xlWorkBooks);
                        ReleaseObject(xlWorkSheet);
                        ReleaseObject(xlWorkBook);
                        ReleaseObject(xlApp);
                        throw new Exception("ERROR: неверный формат данных в файле " + fileName);
                    }
                }
                else
                {
                    try
                    {
                        DeformationMark newDefMark = new DeformationMark(xlWorkSheet.Cells[i, 1].Text.ToString(),
                        Math.Round(Double.Parse(xlWorkSheet.Cells[i, 2].Text.ToString().Replace(",", ".")), 5),
                        Math.Round(Double.Parse(xlWorkSheet.Cells[i, 3].Text.ToString().Replace(",", ".")), 5),
                        Math.Round(Double.Parse(xlWorkSheet.Cells[i, 4].Text.ToString().Replace(",", ".")), 5));
                        defMarks.Add(newDefMark);
                    } catch(Exception e)
                    {
                        xlWorkBook.Close(true, Type.Missing, Type.Missing);
                        xlApp.Quit();
                        ReleaseObject(xlWorkBooks);
                        ReleaseObject(xlWorkSheet);
                        ReleaseObject(xlWorkBook);
                        ReleaseObject(xlApp);
                        throw new Exception("ERROR: неверный формат данных в файле " + fileName);
                    }
                }
            }

            bearMarks.Sort(Comparer<Mark>.Create((x, y) => x.Name.CompareTo(y.Name)));

            xlWorkBook.Close(false, Type.Missing, Type.Missing);
            xlApp.Quit();
            ReleaseObject(xlWorkBooks);
            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }

        /// <summary>
        /// Common function for writing results to excel file
        /// </summary>
        /// <param name="defMarks">A list of deformation marks</param>
        /// <param name="bearMarks">A list of bearing marks</param>
        /// <param name="fileName">The name of file to create</param>
        /// <param name="exportOnlyGoodMarks">Mode of writing</param>
        /// <param name="xlApp">The object of Excel app</param>
        public static void ExcelWriter(List<DeformationMark> defMarks, List<Mark> bearMarks, string fileName, bool exportOnlyGoodMarks, Excel.Application xlApp)
        {
            Excel.Workbooks xlWorkBooks = null;
            Excel.Workbook xlWorkBook = null;
            Excel.Worksheet xlWorkSheet = null;

            xlWorkBooks = xlApp.Workbooks;
            try
            {
                xlWorkBook = xlWorkBooks.Add(Type.Missing);

            }
            catch (Exception)
            {
                xlApp.Quit();
                ReleaseObject(xlWorkBooks);
                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                throw new Exception("ERROR: не удалось создать новый файл для экспорта результатов");
            }

            try
            {
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            }
            catch (Exception)
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
                ReleaseObject(xlWorkBooks);
                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                throw new Exception("ERROR: не удалось создать новую страницу файла для экспорта результатов");
            }

            if (exportOnlyGoodMarks)
            {
                WriteOnlyGoodMarks(defMarks, bearMarks, xlWorkSheet);
            }
            else
            {
                WriteAllMarks(defMarks, bearMarks, xlWorkSheet);
            }

            try
            {
                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing,
            Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing);

            }
            catch (Exception)
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
                ReleaseObject(xlWorkBooks);
                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                throw new Exception("ERROR: Возникли проблемы при сохранении файла " + fileName);
            }
            xlWorkBook.Close(true, Type.Missing, Type.Missing);
            xlApp.Quit();

            ReleaseObject(xlWorkBooks);
            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }

        /// <summary>
        /// Function for writing to file only good marks
        /// </summary>
        /// <param name="defMarks">A list of deformation marks</param>
        /// <param name="bearMarks">A list of bearing marks</param>
        /// <param name="xlWorkSheet">The object of excel file sheet</param>
        public static void WriteOnlyGoodMarks(List<DeformationMark> defMarks, List<Mark> bearMarks, Excel.Worksheet xlWorkSheet)
        {
            object[] objHeaders = { "Имя марки", "X", "Y", "Z", "mx", "my", "mz", "m" };
            Excel.Range mRange = xlWorkSheet.get_Range("A1", "H1");
            mRange.Value = objHeaders;
            Excel.Font mFont = mRange.Font;
            mFont.Bold = true;

            int j = 2;
            for (int i = 1; i < defMarks.Count + 1; i++)
            {
                if (defMarks[i - 1].Status)
                {
                    xlWorkSheet.Cells[j, 1] = defMarks[i - 1].Name;
                    xlWorkSheet.Cells[j, 2] = defMarks[i - 1].XCoordinate;
                    xlWorkSheet.Cells[j, 3] = defMarks[i - 1].YCoordinate;
                    xlWorkSheet.Cells[j, 4] = defMarks[i - 1].ZCoordinate;
                    xlWorkSheet.Cells[j, 5] = defMarks[i - 1].Mx;
                    xlWorkSheet.Cells[j, 6] = defMarks[i - 1].My;
                    xlWorkSheet.Cells[j, 7] = defMarks[i - 1].Mz;
                    xlWorkSheet.Cells[j, 8] = defMarks[i - 1].M;
                    j++;
                }
            }

            for (int i = 0; i < bearMarks.Count; i++)
            {

                xlWorkSheet.Cells[i + j, 1] = bearMarks[i].Name;
                xlWorkSheet.Cells[i + j, 2] = bearMarks[i].XCoordinate;
                xlWorkSheet.Cells[i + j, 3] = bearMarks[i].YCoordinate;

            }
        }

        /// <summary>
        /// Function for writing to file all marks
        /// </summary>
        /// <param name="defMarks">A list of deformation marks</param>
        /// <param name="bearMarks">A list of bearing marks</param>
        /// <param name="xlWorkSheet">The object of excel file sheet</param>
        public static void WriteAllMarks(List<DeformationMark> defMarks, List<Mark> bearMarks, Excel.Worksheet xlWorkSheet)
        {
            object[] objHeaders = { "Имя марки", "X", "Y", "Z", "mx", "my", "mz", "m", "Статус" };
            Excel.Range mRange = xlWorkSheet.get_Range("A1", "I1");
            mRange.Value = objHeaders;
            Excel.Font mFont = mRange.Font;
            mFont.Bold = true;

            int j = 2;
            for (int i = 1; i < defMarks.Count + 1; i++)
            {
                xlWorkSheet.Cells[j, 1] = defMarks[i - 1].Name;
                xlWorkSheet.Cells[j, 2] = defMarks[i - 1].XCoordinate;
                xlWorkSheet.Cells[j, 3] = defMarks[i - 1].YCoordinate;
                xlWorkSheet.Cells[j, 4] = defMarks[i - 1].ZCoordinate;
                xlWorkSheet.Cells[j, 5] = defMarks[i - 1].Mx;
                xlWorkSheet.Cells[j, 6] = defMarks[i - 1].My;
                xlWorkSheet.Cells[j, 7] = defMarks[i - 1].Mz;
                xlWorkSheet.Cells[j, 8] = defMarks[i - 1].M;
                xlWorkSheet.Cells[j, 9] = defMarks[i - 1].Status;
                j++;
            }

            for (int i = 0; i < bearMarks.Count; i++)
            {

                xlWorkSheet.Cells[i + j, 1] = bearMarks[i].Name;
                xlWorkSheet.Cells[i + j, 2] = bearMarks[i].XCoordinate;
                xlWorkSheet.Cells[i + j, 3] = bearMarks[i].YCoordinate;
                xlWorkSheet.Cells[i + j, 4] = bearMarks[i].ZCoordinate;
            }
        }

        private static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
