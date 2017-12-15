using DataCollect.Model;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
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

        public Book Get(Stream stream)
        {
            Book book = new Book { Sheets = new List<Sheet>() };
            var sheetIndex = 1;
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    //Excel 表
                    var sheet = new Sheet { Index = sheetIndex++, Name = reader.Name, Columns = new List<Column>() };
                    while (reader.Read())
                    {
                        bool readFullLine = true;   
                        sheet.Columns.Clear();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colStr = reader.GetValue(i)?.ToString();
                            if (String.IsNullOrEmpty(colStr))
                            {
                                readFullLine = false;
                                break;              //没读到完整的一条，跳到下一行
                            }
                            sheet.Columns.Add(new Column { Name = colStr,Position=i });
                        }
                        if (readFullLine)
                        {
                            break;
                        }
                    }

                    if (sheet.Columns.Count > 0)
                    {
                        book.Sheets.Add(sheet);
                    }
                    sheetIndex++;
                } while (reader.NextResult() && book.Sheets.Count <= sheetIndex);
            }
            return book;
        }

    }
}
