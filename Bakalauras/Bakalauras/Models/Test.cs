using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool IsActive { get; set; }

        [ForeignKey("fk__User")]
        public ApplicationUser User { get; set; }
        public string fk__User { get; set; }

        public Test()
        {
            IsActive = true;
        }
    }
}
