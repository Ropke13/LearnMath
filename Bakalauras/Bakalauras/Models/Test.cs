using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Models
{
    public class Test
    {
        [Key]
        public Guid Id { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Password { get; set; }
        public int Time { get; set; }
    }
}
