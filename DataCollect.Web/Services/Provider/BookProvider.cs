using DataCollect.Model;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Service.Provider
{
    public class BookProvider
    {
        public static Book Get(Stream stream, string fileName)
        {
            var workBook = WorkBookProvider.Get(stream,fileName);


            return Get(workBook,Path.GetFileNameWithoutExtension(fileName));
        }

        private static Book Get(IWorkbook workbook,string name)
        {
            Book book = new Book { Name = name, Sheets = new List<Sheet>()};
            for (int index=0;index<workbook.NumberOfSheets;index++)
            {
                var sheet = workbook.GetSheetAt(index);
                Sheet sheetItem = new Sheet { Name = sheet.SheetName, Columns = new List<Column>()};
                var row = sheet.GetRow(0);
                for (int i = row.FirstCellNum; i < row.LastCellNum; i++)
                {
                    var cell = row.GetCell(i);
                    Column column = new Column { Name = cell.StringCellValue };
                    sheetItem.Columns.Add(column);
                }
                book.Sheets.Add(sheetItem);
            }

            return book;
        }

        public static Book Get(string path)
        {
            var workBook = WorkBookProvider.Get(path);

            return Get(workBook,Path.GetFileNameWithoutExtension(path));
        }
    }
}
