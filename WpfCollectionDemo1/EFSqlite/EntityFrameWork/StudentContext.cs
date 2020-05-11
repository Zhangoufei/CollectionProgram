using EFSqlite.DbModel;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace EFSqlite.EntityFrameWork
{


    public class StudentContext : DbContext
    {

        //定义属性，便于外部访问数据表
        public DbSet<Student> Students { get { return Set<Student>(); } }

        //定义属性，便于外部访问数据表
        public DbSet<Teacher> Teachers { get { return Set<Teacher>(); } }

        public StudentContext() : base("SQLiteConnect")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //ModelConfiguration.Configure(modelBuilder);
            //var init = new SqliteDropCreateDatabaseWhenModelChanges<StudentContext>(modelBuilder);
            //Database.SetInitializer(init);
        }

    }


    public class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureBookEntity(modelBuilder);
        }
        private static void ConfigureBookEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>();
            modelBuilder.Entity<Teacher>();
        }
    }
    
}
