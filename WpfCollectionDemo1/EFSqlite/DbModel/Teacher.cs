using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSqlite.DbModel
{
    public class Teacher
    {
        /// <summary>
        /// 自增设置
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { set; get; }

        public  string name { set; get; }

        /// <summary>
        /// 教授科目列表
        /// </summary>
        public  List<int> subjects { set; get; }

        /// <summary>
        /// 老师管理的所有学生
        /// </summary>
        public List<Student> students { set; get; }

    }
}
