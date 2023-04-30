using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("fk__User")]
        public ApplicationUser User { get; set; }
        public string fk__User { get; set; }
    }
}
