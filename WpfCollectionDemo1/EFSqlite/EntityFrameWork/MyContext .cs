using EFSqlite.DbModel;
using System.Data.Common;
using System.Data.Entity;

namespace EFSqlite.EntityFrameWork
{
    public class MyContext : DbContext
    {
        public MyContext(DbConnection conn) : base(conn, false)
        {

        }

        public DbSet<Actress> ActressSet { get; set; }

    }
}
