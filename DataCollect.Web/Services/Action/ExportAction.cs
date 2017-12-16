using DataCollect.Model;
using DataCollect.Service.Service;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Services.Action
{
    public class ExportAction
    {

        public ExportAction()
        {
        }

        public byte[] Export(Book book)
        {
            MemoryStream stream1 = new MemoryStream();
            IWorkbook workbook = new XSSFWorkbook();

            foreach (var sheet in book.Sheets)
            {
                var worksheet = workbook.CreateSheet(sheet.Name);
                var rowIndex = 0;
                var row1 = worksheet.CreateRow(rowIndex);
                for (int i = 0; i < sheet.Columns.Count; i++)
                {
                    row1.CreateCell(i).SetCellValue(sheet.Columns[i].Name);
                }
                rowIndex++;

                for (int i = 0; i < sheet.Rows.Count; i++)
                {
                    var rowData = sheet.Rows[i].Data;
                    var row = worksheet.CreateRow(rowIndex);
                    for (int col = 0; col < rowData.Count; col++)
                    {
                        row.CreateCell(col).SetCellValue(rowData[col].Value);
                    }

                    rowIndex++;
                }
               
            }

            workbook.Write(stream1);
            var result = stream1.ToArray();

            workbook.Close();
            stream1.Close();
            return result;
        }
    }
}
