using DataCollect.Model;
using DataCollect.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Services.Service
{
    public class ColumnService
    {
        private ApplicationDbContext _context;

        public ColumnService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Column> GetOrderList(int sheetId)
        {
            return _context.Column.Where(c => c.SheetId == sheetId).OrderBy(c => c.Position).ToList();
        }
    }
}
