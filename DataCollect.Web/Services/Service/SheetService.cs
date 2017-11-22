using DataCollect.Model;
using DataCollect.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Service.Service
{
    public class SheetService
    {
        private readonly ApplicationDbContext _context;

        public SheetService(ApplicationDbContext context) {
            _context = context;
        }

        public void FillRows(Sheet sheet)
        {
            sheet.Rows = _context.Row.Include(t=>t.Data).Where(t => t.SheetId == sheet.Id).ToList();
        }
    }
}
