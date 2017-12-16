using DataCollect.Model;
using DataCollect.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Services.Service
{
    public class RowService
    {
        private ApplicationDbContext _context;

        public RowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Remove(Row row)
        {
            _context.RemoveRange(_context.ColumnData.Where(t => t.RowId == row.Id));
            _context.Remove(row);
            _context.SaveChanges();
        }
    }
}
