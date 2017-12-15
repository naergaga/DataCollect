using DataCollect.Service.Provider;
using ExcelReader.Data.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Test.Services.Provider
{
    [TestClass]
    public class BookProviderTest
    {
        [TestMethod]
        public void Get()
        {
            BookProvider bp = new BookProvider();
            var book = bp.Get(File.OpenRead("test.xls"));
            Console.WriteLine(InfoProvider.GetBook(book));
        }
    }
}
