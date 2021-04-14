using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("logs")]
    public class Log
    {
        [Column("LogId")]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
    }
}
