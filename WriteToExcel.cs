using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using WinForms = System.Windows.Forms;

namespace MarkAnalyzerAutocadPlagin
{
    public class WriteToExcel
    {
        public static void ExcelWriter(List<DeformationMark> defMarks, List<DeformationMark> bearMarks, string xlName, Editor ed, bool exportOnlyGoodMarks)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            xlApp = new Excel.Application();
            if(xlApp == null)
            {
                WinForms.MessageBox.Show("ERROR 701: Не удалось открыть приложение Excel для записи\n Работат программы будет прервана\n");
                return;
            }

            xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            if (xlApp == null)
            {
                WinForms.MessageBox.Show("ERROR 702: Не удалось созадть файл Excel для записи\n Работат программы будет прервана\n");
                return;
            }

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);//??
            if (xlApp == null)
            {
                WinForms.MessageBox.Show("ERROR 703: Не удалось созадть страницу Excel для записи\n Работат программы будет прервана\n");
                return;
            }

            if (exportOnlyGoodMarks)
            {
                WriteOnlyGoodMarks(defMarks, bearMarks, xlWorkSheet);
            }
            else
            {
                WriteAllMarks(defMarks, bearMarks, xlWorkSheet);
            }         

            if (xlWorkSheet.Cells[defMarks.Count + 1, 9] == null && xlWorkSheet.Cells[2, 2] == null)
            {
                WinForms.MessageBox.Show("ERROR 704: Не удалось записать результаты в файл Excel\n Работат программы будет прервана\n");
                return;
            }
            else ed.WriteMessage("Данные успешно экспортированы в файл " + xlName);

            xlWorkBook.SaveAs(xlName, Excel.XlFileFormat.xlWorkbookNormal,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Excel.XlSaveAsAccessMode.xlExclusive,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing);
            xlWorkBook.Close(true, Type.Missing, Type.Missing);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet, ed);
            ReleaseObject(xlWorkBook, ed);
            ReleaseObject(xlApp, ed);
        }

        private static void ReleaseObject(object obj, Editor ed)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                ed.WriteMessage("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static void WriteOnlyGoodMarks(List<DeformationMark> defMarks, List<DeformationMark> bearMarks, Excel.Worksheet xlWorkSheet)
        {
            object[] objHeaders = { "Имя марки", "X", "Y", "Z", "mx", "my", "mz", "m"};
            Excel.Range mRange = xlWorkSheet.get_Range("A1", "H1");
            mRange.Value = objHeaders;
            Excel.Font mFont = mRange.Font;
            mFont.Bold = true;

            int j = 2;
            for (int i = 1; i < defMarks.Count + 1; i++)
            {
                if (defMarks[i - 1].relevance == true)
                {
                    xlWorkSheet.Cells[j, 1] = defMarks[i - 1].name;
                    xlWorkSheet.Cells[j, 2] = defMarks[i - 1].xCoordinate;
                    xlWorkSheet.Cells[j, 3] = defMarks[i - 1].yCoordinate;
                    xlWorkSheet.Cells[j, 4] = defMarks[i - 1].zCoordinate;
                    xlWorkSheet.Cells[j, 5] = defMarks[i - 1].mx;
                    xlWorkSheet.Cells[j, 6] = defMarks[i - 1].my;
                    xlWorkSheet.Cells[j, 7] = defMarks[i - 1].mz;
                    xlWorkSheet.Cells[j, 8] = defMarks[i - 1].m;
                    j++;
                }
            }

            for(int i = 0; i < bearMarks.Count; i++)
            {
                xlWorkSheet.Cells[i + j, 1] = bearMarks[i].name;
                xlWorkSheet.Cells[i + j, 2] = bearMarks[i].xCoordinate;
                xlWorkSheet.Cells[i + j, 3] = bearMarks[i].yCoordinate;
                xlWorkSheet.Cells[i + j, 4] = bearMarks[i].zCoordinate;
            }
        }

        public static void WriteAllMarks(List<DeformationMark> defMarks, List<DeformationMark> bearMarks, Excel.Worksheet xlWorkSheet)
        {
            object[] objHeaders = { "Имя марки", "X", "Y", "Z", "mx", "my", "mz", "m", "Статус"};
            Excel.Range mRange = xlWorkSheet.get_Range("A1", "I1");
            mRange.Value = objHeaders;
            Excel.Font mFont = mRange.Font;
            mFont.Bold = true;

            int j = 2;
            for (int i = 1; i < defMarks.Count + 1; i++)
            {
                {
                    xlWorkSheet.Cells[j, 1] = defMarks[i - 1].name;
                    xlWorkSheet.Cells[j, 2] = defMarks[i - 1].xCoordinate;
                    xlWorkSheet.Cells[j, 3] = defMarks[i - 1].yCoordinate;
                    xlWorkSheet.Cells[j, 4] = defMarks[i - 1].zCoordinate;
                    xlWorkSheet.Cells[j, 5] = defMarks[i - 1].mx;
                    xlWorkSheet.Cells[j, 6] = defMarks[i - 1].my;
                    xlWorkSheet.Cells[j, 7] = defMarks[i - 1].mz;
                    xlWorkSheet.Cells[j, 8] = defMarks[i - 1].m;
                    xlWorkSheet.Cells[j, 9] = defMarks[i - 1].relevance;
                    j++;
                }
            }

            for (int i = 0; i < bearMarks.Count; i++)
            {
                xlWorkSheet.Cells[i + j, 1] = bearMarks[i].name;
                xlWorkSheet.Cells[i + j, 2] = bearMarks[i].xCoordinate;
                xlWorkSheet.Cells[i + j, 3] = bearMarks[i].yCoordinate;
                xlWorkSheet.Cells[i + j, 4] = bearMarks[i].zCoordinate;
            }
        }
    }
}
