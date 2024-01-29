using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcDB.Models;
using System.Data.Common;
using System.Data;

namespace CalcDB
{
    public class CalcContext : DbContext
    {
        public CalcContext(DbContextOptions<CalcContext> options) : base(options)
        {

        }
        public DbSet<Pets> dbSetPets { get; set; }

        /*public List<Pets> FilterStudents(string name)
        {
            var sql = dbSetExcel.FromSqlRaw($"SELECT * FROM Pets WHERE (FirstName LIKE '%{name}%' OR MiddleName LIKE '%{name}%' OR  LastName LIKE '%{name}%')")
                .ToList();
            return sql;
        }*/

        public static class Helper<T>
        {
            public static List<T> ExecuteSQLQuery(DbContext context, string sql, Func<DbDataReader, T> map)
            {
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    context.Database.OpenConnection();
                    using (var result = cmd.ExecuteReader())
                    {
                        var List = new List<T>();
                        while (result.Read())
                        {
                            List.Add(map(result));
                        }
                        return List;
                    }
                    context.Database.CloseConnection();
                }
                return null;
            }
        }

       

        public int CountPets()
        {
            var sqlorder = "SELECT COUNT(*) FROM Pets ";
            var result = Helper<int>.ExecuteSQLQuery(this, sqlorder, x => (int)x[0]).SingleOrDefault();
            
            return result;
        }

    }
}
