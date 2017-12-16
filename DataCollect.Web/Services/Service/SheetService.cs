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

        public void FillRows(Sheet sheet,string userId)
        {
            sheet.Rows = _context.Row.Where(t => t.SheetId == sheet.Id && userId == t.UserId).ToList();
            sheet.Rows.ForEach(row =>
            {
                row.Data = (from cd in _context.ColumnData
                           join c in _context.Column
                           on cd.ColumnId equals c.Id
                           where cd.RowId == row.Id
                            orderby c.Position
                           select cd).ToList();

            });
        }

        public List<Column> GetColumns(int sheetId)
        {
            return _context.Column.Where(c => c.SheetId == sheetId).OrderBy(t => t.Position).ToList();
        }

        public void FillRows(Sheet sheet)
        {
            sheet.Rows = _context.Row.Where(t => t.SheetId == sheet.Id).ToList();
            sheet.Rows.ForEach(row =>
            {
                row.Data = (from cd in _context.ColumnData
                            join c in _context.Column
                            on cd.ColumnId equals c.Id
                            where cd.RowId == row.Id
                            orderby c.Position
                            select cd).ToList();

            });
        }

        public void FillCols(Sheet sheet)
        {
            sheet.Columns = _context.Column.Where(c => c.SheetId == sheet.Id).OrderBy(t=>t.Position).ToList();
        }

        public List<Sheet> GetList()
        {
            return _context.Sheet.ToList();
        }
    }
}
