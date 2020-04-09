using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSqlite.DbModel
{
    [Table("Actress")]
    public class Actress
    {

        public Int64 ID { get; set; }

        public string Name { get; set; }

        public Int32 Age { get; set; }

    }
}
