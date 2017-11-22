using DataCollect.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Test.Providers
{
    public class DbContextProvider
    {
        private static DbContextOptionsBuilder<ApplicationDbContext> builder;
        private static String connStr1 = "Server=(localdb)\\mssqllocaldb;Database=aspnet-DataCollect-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true";

        static DbContextProvider()
        {
            builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connStr1);
        }

        public static ApplicationDbContext Get()
        {
            return new ApplicationDbContext(builder.Options);
        }
    }
}
