using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataCollect.Service.Service;
using DataCollect.Data;
using DataCollect.Model;
using DataCollect.Service.Common;

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
            ApplicationDbContext context = new ApplicationDbContext();
            var service = new BookService(context);
            service.Add(new Book
            {
                Name="英雄使用统计",
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
            ApplicationDbContext context = new ApplicationDbContext();
            var service = new BookService(context);
            var book = service.Get("英雄使用统计");
            Console.WriteLine(BookCommon.GetString(book));
        }
    }
}
