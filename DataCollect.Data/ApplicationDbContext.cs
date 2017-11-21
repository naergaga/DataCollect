using DataCollect.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataCollect.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext():base("conn1")
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Column> Column { get; set; }
        public DbSet<Sheet> Sheet { get; set; }
        public DbSet<ColumnData> ColumnData { get; set; }
        public DbSet<CollectEvent> Event { get; set; }
        public DbSet<EventBook> EventBook { get; set; }
        public DbSet<Row> Row { get; set; }
    }
}