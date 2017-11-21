using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Service.Provider
{
    public class WorkBookProvider
    {
        public static IWorkbook Get(string path)
        {
            IWorkbook workbook = null;
            FileStream fs;
            using (fs = File.OpenRead(path))
            {
                // 2007版本  
                if (path.IndexOf(".xlsx") > 0)
                    workbook = new XSSFWorkbook(fs);
                // 2003版本  
                else if (path.IndexOf(".xls") > 0)
                    workbook = new HSSFWorkbook(fs);
            }
            return workbook;
        }

        public static IWorkbook Get(Stream stream, string fileName)
        {
            IWorkbook workbook = null;

            // 2007版本  
            if (fileName.IndexOf(".xlsx") > 0)
                workbook = new XSSFWorkbook(stream);
            // 2003版本  
            else if (fileName.IndexOf(".xls") > 0)
                workbook = new HSSFWorkbook(stream);

            return workbook;
        }
    }
}
