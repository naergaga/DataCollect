using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataCollect.Service.Service;
using DataCollect.Model;
using DataCollect.Service.Common;
using DataCollect.Web.Data;
using DataCollect.Test.Providers;

namespace DataCollect.Test.Services.Service
{
    /// <summary>
    /// BookServiceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class BookServiceTest
    {

        [TestMethod]
        public void Add()
        {
            ApplicationDbContext context = DbContextProvider.Get();
            BookService service =null;
            service.Add(new Book
            {
                Name = "英雄使用统计",
                Sheets = new List<Sheet>
                {
                    new Sheet
                    {
                        Name="Sheet1",
                        Columns = new List<Column>
                        {
                            new Column{ Name="英雄名称"},
                            new Column{ Name="英雄场次"},
                            new Column{ Name="最佳数据"},
                            new Column{ Name="胜率"}
                        }
                    }
                }
            });
        }

        [TestMethod]
        public void Get()
        {
            ApplicationDbContext context = DbContextProvider.Get();
            BookService service = null;
            var book = service.Get("英雄使用统计");
            Console.WriteLine(BookCommon.GetString(book));
        }
    }
}
