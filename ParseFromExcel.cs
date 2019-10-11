using System;
using Excel = Microsoft.Office.Interop.Excel;
using WinForms = System.Windows.Forms;

namespace MarkAnalyzerAutocadPlagin
{
    public class ParseFromExcel
    {
        public static string[,] ExcelFileParser(string fileName)
        {
            Excel.Application ObjWorkExcel = new Excel.Application(); //открыть эксель

            if (ObjWorkExcel == null)
            {
                WinForms.MessageBox.Show("ERROR 601: Не удалось открыть приложение Excel для чтения\n Работат программы будет прервана\n");
                return null;
            }
            Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(fileName,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing); //открыть файл

            if (ObjWorkBook == null)
            {
                WinForms.MessageBox.Show("ERROR 602: Не удалось открыть файл" + fileName + "\nРабота программы будет прервана\n");
                return null;
            }

            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1 лист

            if (ObjWorkSheet == null)
            {
                WinForms.MessageBox.Show("ERROR 603: Не удалось открыть страницу в файле" + fileName + "\nРабота программы будет прервана\n");
                return null;
            }

            var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);//1 ячейку
            int lastColumn = (int)lastCell.Column;//сохраним непосредственно требующееся в дальнейшем
            int lastRow = (int)lastCell.Row;

            string[,] list = new string[lastCell.Row - 1, lastCell.Column]; // массив значений с листа равен по размеру листу
            for (int i = 1; i <= (int)lastCell.Row - 1; i++) //по всем колонкам
                for (int j = 0; j < (int)lastCell.Column; j++) // по всем строкам
                    list[i - 1, j] = ObjWorkSheet.Cells[i + 1, j + 1].Text.ToString();//считываем текст в строку

            ObjWorkBook.Close(false, Type.Missing, Type.Missing); //закрыть не сохраняя
            ObjWorkExcel.Quit(); // выйти из экселя
            GC.Collect(); // убрать за собой -- в том числе не используемые явно объекты !

            return list;
        }
    }
}
