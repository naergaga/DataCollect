using DataCollect.Test.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Test.Data
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void ClearNoUserRow()
        {
            var context = DbContextProvider.Get();
            var rows = context.Row.Where(t => t.UserId == null);
            foreach (var row in rows)
            {
                context.RemoveRange(context.ColumnData.Where(t => t.RowId == row.Id));
                context.Remove(row);
            }
            context.SaveChanges();
        }
    }
}
