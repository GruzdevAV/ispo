using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Lab4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab4.Controllers
{
    public class HomeController : Controller
    {
        private Lab4DbVers1Entities db = new Lab4DbVers1Entities();
        private static string PrevPattern { get; set; }
        public ActionResult Index(string pattern)
         {
            ActionResult res;
            PrevPattern = pattern;
            List<SearchResultLine> result;
            result = GetListForTable(pattern);
            ViewBag.SearchData = result;
            if (pattern==null)
            {
                res = View();
            }
            else
            {
                res = Json(result, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

        private List<SearchResultLine> GetListForTable(string pattern)
        {
            List<SearchResultLine> result = db.Rooms
               .Join(
                   db.GuestsInRooms,
                   r => r.ROOM_ID,
                   gir => gir.ROOM_ID,
                   (r, gir) => new
                   {
                       RoomName = r.ROOM_NAME,
                       GirId = gir.ID,
                       RoomId = gir.ROOM_ID,
                       GuestId = gir.GUEST_ID
                   }
              ).Join(
                   db.Guests,
                   gir => gir.GuestId,
                   g => g.GUEST_ID,
                   (gir, g) => new SearchResultLine()
                   {
                       GuestName = g.GUEST_NAME,
                       RoomName = gir.RoomName
                   }
            ).ToList();
            if (!string.IsNullOrEmpty(pattern))
            {
                pattern = pattern.ToUpper();
                result = result.Where((p) => p.GuestName.ToUpper().Contains(pattern) || p.RoomName.ToUpper().Contains(pattern)).ToList();
            }

            return result;
        }

        public FileStreamResult GetWord()
        {
            var result = GetListForTable(PrevPattern);
            string[,] data = new string[result.Count + 1, 2];
            data[0, 0] = "Гость";
            data[0, 1] = "Комната";
            for (int i = 0; i < result.Count; i++)
            {
                data[i + 1, 0] = result[i].GuestName;
                data[i + 1, 1] = result[i].RoomName;
            }
            MemoryStream memoryStream = GenerateWord(data);
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "demo.docx"
            };
        }
        private MemoryStream GenerateWord(string[,] data)
        {
            MemoryStream mStream = new MemoryStream();
            // Создаем документ
            WordprocessingDocument document =
                WordprocessingDocument.Create(mStream, WordprocessingDocumentType.Document, true);
            // Добавляется главная часть документа. 
            MainDocumentPart mainPart = document.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = mainPart.Document.AppendChild(new Body());
            // Создаем таблицу. 
            Table table = new Table();
            body.AppendChild(table);

            // Устанавливаем свойства таблицы(границы и размер).
            TableProperties props = new TableProperties(
                new TableBorders(
                new TopBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                },
                new BottomBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                },
                new LeftBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                },
                new RightBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                },
                new InsideHorizontalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                },
                new InsideVerticalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                }));

            // Назначаем свойства props объекту table
            table.AppendChild<TableProperties>(props);

            // Заполняем ячейки таблицы.
            for (var i = 0; i <= data.GetUpperBound(0); i++)
            {
                var tr = new TableRow();
                for (var j = 0; j <= data.GetUpperBound(1); j++)
                {
                    var tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text(data[i, j]))));

                    // размер колонок определяется автоматически.
                    tc.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Auto }));

                    tr.Append(tc);
                }
                table.Append(tr);
            }

            mainPart.Document.Save();
            document.Dispose();
            mStream.Position = 0;
            return mStream;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}