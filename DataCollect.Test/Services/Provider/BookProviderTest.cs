using DataCollect.Service.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            var book = BookProvider.Get(@"C:\Users\Administrator\Desktop\装备录入\公车统计.xlsx");

            Console.WriteLine(book);
        }
    }
}
