using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcDB.Models;

namespace CalcDB
{
    public class CalcContext : DbContext
    {
        public CalcContext(DbContextOptions<CalcContext> options) : base(options)
        {

        }
        public DbSet<Excel> dbSetExcel { get; set; }

        
    }
}
