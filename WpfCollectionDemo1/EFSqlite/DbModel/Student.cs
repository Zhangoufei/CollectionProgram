using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSqlite.DbModel
{
    public class Student
    {
        /// <summary>
        /// 自增设置
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Name { set; get; }

        public int Age { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { set; get; }

        public DateTime CreateTime { set; get; }

    }
}
