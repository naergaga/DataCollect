using DataCollect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Services.Common
{
    public class SheetValidator
    {
        public static bool validate(Sheet dbSheet,Sheet exSheet)
        {
            return dbSheet.Columns.Count == exSheet.Columns.Count;
        }
    }
}
