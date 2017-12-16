using DataCollect.Model;
using DataCollect.Service.Provider;
using DataCollect.Service.Service;
using DataCollect.Web.Data;
using DataCollect.Web.Services.Common;
using DataCollect.Web.Services.Provider;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Services.Action
{
    public class ImportConfig
    {
        public bool SkipAnyEmpty { get; set; }
        public bool TitleFirst { get; set; }
    }

    public class ImportAction
    {
        private ApplicationDbContext _context;
        private BookService bookService;

        public ImportConfig Config = new ImportConfig { SkipAnyEmpty = true, TitleFirst = true };

        public ImportAction(ApplicationDbContext context, BookService bookService)
        {
            _context = context;
            this.bookService = bookService;
        }

        public void Import(Stream stream, int bookId,string userId)
        {
            var book = bookService.Get(bookId);
            var sheetIndex = 0;
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    var sheet = book.Sheets[sheetIndex];
                    var colCount = sheet.Columns.Count;
                    List<Row> list = new List<Row>();
                    bool findTitle = !Config.TitleFirst;
                    while (reader.Read())// 表数据
                    {
                        var fullRow = true;
                        Row row = new Row { SheetId = sheet.Id, Data = new List<ColumnData>(),UserId=userId };
                        for (int i = 0; i < colCount; i++) //行数据
                        {
                            var str = reader.GetValue(i)?.ToString();
                            //如果 需要跳过空格或未找到标题行 and 空数据 ，跳到下一行
                            if ((Config.SkipAnyEmpty||!findTitle)&& string.IsNullOrEmpty(str))
                            {
                                fullRow = false;
                                break;
                            }
                            row.Data.Add(new ColumnData { ColumnId = sheet.Columns[i].Id, Value = str });
                        }
                        if (fullRow)
                        {
                            if (!findTitle)
                            {
                                findTitle = true;
                                continue;
                            }
                            list.Add(row);
                        }
                    }
                    _context.Row.AddRange(list);
                    _context.SaveChanges();
                    sheetIndex++;
                } while (reader.NextResult() && book.Sheets.Count < sheetIndex);
            }
        }
    }
}
