﻿using DataCollect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Data.Provider
{
    public class InfoProvider
    {
        public static string GetBook(Book book)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("BookName:").Append(book.Name).AppendLine();

            foreach (var sheet in book.Sheets)
            {
                sb.Append(GetSheet(sheet));
            }
            return sb.ToString();
        }

        public static string GetSheet(Sheet sheet)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(sheet.Name).AppendLine();
            sb.Append("  ");
            if (sheet.Columns != null)
                foreach (var column in sheet.Columns)
                {
                    sb.Append(column.Name).Append(", ");
                }
            sb.AppendLine();
            if (sheet.Rows != null)
            {
                foreach (var row in sheet.Rows)
                {
                    foreach (var item in row.Data)
                    {
                        sb.Append(item.Value).Append(", ");
                    }
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}
