using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataCollect.Model;

namespace DataCollect.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EventBook>().HasKey(t=> new { t.EventId,t.BookId });
            //需要手动清除关联数据
            builder.Entity<ColumnData>().HasOne(t => t.Row).WithMany(t => t.Data).OnDelete(DeleteBehavior.Restrict);
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
