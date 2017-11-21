using DataCollect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Service.Common
{
    public class BookCommon
    {
        public static String GetString(Book book)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("统计表:").Append(book.Name).AppendLine();
            foreach (var sheet in book.Sheets)
            {
                sb.Append("Sheet:").Append(sheet.Name).AppendLine();
                foreach (var col in sheet.Columns)
                {
                    sb.Append(col.Name).Append("  ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
