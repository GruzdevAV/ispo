using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;


namespace WPF
{
    public static class DocWorkings
    {
        public static Word.Application wordapp;
        public static Word.Documents worddocuments;
        public static Word.Document worddocument;
        public static void Open()
        {
            try
            {
                wordapp = new Word.Application
                {
                    Visible = true
                };
                Object template = Type.Missing;
                Object newTemplate = false;
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                Object visible = true;
                //Создаем документ 1
                wordapp.Documents.Add(
               ref template, ref newTemplate, ref documentType, ref visible);
                //Меняем шаблон
                template = AppContext.BaseDirectory + @"temp.doc";
                //Создаем документ 2 worddocument в данном случае создаваемый объект 
                worddocument =
                wordapp.Documents.Add(
                 ref template, ref newTemplate, ref documentType, ref visible);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Close()
        {
            Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
            Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
            Object routeDocument = Type.Missing;
            wordapp.Quit(ref saveChanges,
                         ref originalFormat, ref routeDocument);
            wordapp = null;
        }
        public static void Test()
        {
            //Объявляем приложение
            Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
            //Отобразить Excel
            ex.Visible = true;
            //Количество листов в рабочей книге
            ex.SheetsInNewWorkbook = 2;
            //Добавить рабочую книгу
            Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            //Отключить отображение окон с сообщениями
            ex.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            //Название листа (вкладки снизу)
            sheet.Name = "Отчет за 13.12.2017";
            //Пример заполнения ячеек
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j < 9; j++)
                    sheet.Cells[i, j] = String.Format("Boom {0} {1}", i, j);
            }
            //Захватываем диапазон ячеек
            Excel.Range range1 = sheet.Range[sheet.Cells[1, 1], sheet.Cells[9, 9]];
            //Шрифт для диапазона
            range1.Cells.Font.Name = "Tahoma";
            //Размер шрифта для диапазона
            range1.Cells.Font.Size = 10;
            //Захватываем другой диапазон ячеек
            Excel.Range range2 = sheet.Range[sheet.Cells[1, 1], sheet.Cells[9, 2]];
            range2.Cells.Font.Name = "Times New Roman";
            object filename = AppContext.BaseDirectory + @"test.xls";
            workBook.SaveAs(filename);
            workBook.Close();
            ex.Quit();


        }
        public static void Lab3TaskWord(string supplier, string buyer, List<Good> list)
        {
            Word.Application winword = new Word.Application()
            //{ Visible = true }
            ;
            Word.Document document = winword.Documents.Add();
            Word.Paragraph paragraph = document.Content.Paragraphs.Add();
            Normalize(paragraph);

            paragraph.Range.Text = $"Расходная накладная №{1} от {DateTime.Now.Date.ToString("d")}";
            BoldUnderlineify(document, paragraph, @"(?<=Расходная накладная №).+(?= от)", true, true);
            BoldUnderlineify(document, paragraph, @"(?<=от ).+", true, true);
            InsertAfterAndDropStyle(paragraph);

            paragraph.Range.Text = $"Поставщик: {supplier}";
            BoldUnderlineify(document, paragraph, supplier, true, true);
            InsertAfterAndDropStyle(paragraph);

            paragraph.Range.Text = $"Поставщик: {buyer}";
            BoldUnderlineify(document, paragraph, buyer, true, true);
            InsertAfterAndDropStyle(paragraph);

            Word.Table table = document.Content.Tables.Add(paragraph.Range, list.Count + 1, 5);
            table.Borders.Enable = 1;
            var dataRow = table.Rows[1].Cells;
            dataRow[1].Range.Text = "№";
            dataRow[2].Range.Text = "Товар";
            dataRow[3].Range.Text = "Количество";
            dataRow[4].Range.Text = "Цена";
            dataRow[5].Range.Text = "Сумма";
            decimal sum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                dataRow = table.Rows[i + 2].Cells;
                dataRow[1].Range.Text = list[i].Number.ToString();
                dataRow[2].Range.Text = list[i].Name;
                dataRow[3].Range.Text = $"{list[i].Quantity} кг";
                dataRow[4].Range.Text = $"{list[i].Price} кг/руб.";
                dataRow[5].Range.Text = $"{list[i].Sum} руб.";
                sum += list[i].Sum;
            }
            paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            paragraph.Range.Text = $"Итого:\t{sum}\t";
            paragraph.Range.InsertParagraphAfter();

            object filename = AppContext.BaseDirectory + @"wordExample.doc";
            document.SaveAs(filename);
            document.Close();
            winword.Quit();
        }

        private static void Normalize(Word.Paragraph paragraph)
        {
            paragraph.Range.Font.Name = "Times new roman";
            paragraph.Range.Font.Size = 14;
            paragraph.Range.Font.Bold = 0;
            paragraph.Range.Font.Underline = 0;
            paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
        }

        private static void InsertAfterAndDropStyle(Word.Paragraph paragraph)
        {
            paragraph.Range.InsertParagraphAfter();
            Normalize(paragraph);
        }

        private static void BoldUnderlineify(Word.Document document, Word.Paragraph paragraph, string pattern, bool bold, bool underline)
        {
            var match = new Regex(pattern).Match(paragraph.Range.Text);
            var start = paragraph.Range.Start + match.Index;
            var end = start + match.Length;
            var range = document.Range(start, end);
            if (bold)
            {
                range.Bold = 1;
            }
            if (underline)
            {
                range.Underline = Word.WdUnderline.wdUnderlineSingle;
            }
        }
        public static void Lab3TaskExcel(string supplier, string buyer, List<Good> list)
        {
            Excel.Application winexcel = new Excel.Application();
            winexcel.SheetsInNewWorkbook = 2;
            Excel.Workbook workBook = winexcel.Workbooks.Add(Type.Missing);
            //Отключить отображение окон с сообщениями
            winexcel.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            Excel.Worksheet sheet = (Excel.Worksheet)winexcel.Worksheets.get_Item(1);
            sheet.Name = $"Заказ за {DateTime.Today.ToString("d")}";
            sheet.Cells.Font.Name = "Times new roman";
            sheet.Cells.Font.Size = 14;
            sheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;

            sheet.Cells[1, 1] = "Поставщик:";
            sheet.Cells[2, 1] = "Покупатель:";

            Excel.Range range1 = sheet.Range[sheet.Cells[1, 1], sheet.Cells[2, 1]];
            range1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            sheet.Cells[1, 2] = supplier;
            sheet.Cells[2, 2] = buyer;

            Excel.Range range2 = sheet.Range[sheet.Cells[1, 2], sheet.Cells[2, 2]];
            range2.Cells.Font.Bold = 1;
            range2.Cells.Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleSingle;

            Excel.Range allTableRange = sheet.Range[sheet.Cells[4, 1], sheet.Cells[4 + list.Count, 5]];
            
            allTableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            allTableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            Excel.Range tableHeaders = sheet.Range[sheet.Cells[4, 1], sheet.Cells[4, 5]];
            Excel.Range tableFirstCol = sheet.Range[sheet.Cells[4, 1], sheet.Cells[4 + list.Count, 1]];
            tableHeaders.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            tableFirstCol.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            Excel.Range tableBodyRange = sheet.Range[sheet.Cells[5, 1], sheet.Cells[5 + list.Count, 5]];
            tableBodyRange.Cells.Font.Bold = 1;

            sheet.Cells[4, 1] = "№";
            sheet.Cells[4, 2] = "Товар";
            sheet.Cells[4, 3] = "Количество";
            sheet.Cells[4, 4] = "Цена";
            sheet.Cells[4, 5] = "Сумма";
            decimal sum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                var row = 5 + i;
                sheet.Cells[row, 1] = list[i].Number.ToString();
                sheet.Cells[row, 2] = list[i].Name;
                sheet.Cells[row, 3] = $"{list[i].Quantity} кг";
                sheet.Cells[row, 4] = $"{list[i].Price} кг/руб.";
                sheet.Cells[row, 5] = $"{list[i].Sum} руб.";
                sum += list[i].Sum;
            }

            Excel.Range totalLabel = sheet.Range[sheet.Cells[6 + list.Count, 4], sheet.Cells[6 + list.Count, 4]];
            totalLabel.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            sheet.Cells[6 + list.Count, 4] = "Итого:";
            Excel.Range totalValue = sheet.Range[sheet.Cells[6 + list.Count, 5], sheet.Cells[6 + list.Count, 5]];
            sheet.Cells[6 + list.Count, 5] = $"{sum} руб.";
            object filename = AppContext.BaseDirectory + @"excelExample.xls";
            workBook.SaveAs(filename);
            workBook.Close();
            winexcel.Quit();
        }
    }
    class Commodity
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
