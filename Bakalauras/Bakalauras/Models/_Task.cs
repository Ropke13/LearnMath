using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Models
{
    public class _Task
    {
        [Key]
        public Guid Id { get; set; }
        public string TaskString { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Explaining { get; set; }
        public string Level { get; set; }
        public string Theme { get; set; }
    }
}
