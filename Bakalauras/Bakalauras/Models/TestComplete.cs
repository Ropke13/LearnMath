using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Models
{
    public class TestComplete
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }
        public int? TotalTasks { get; set; }
        public int? TotalCorrectAnswers { get; set; }

        [ForeignKey("fk__Test")]
        public Test Test { get; set; }
        public Guid? fk__Test { get; set; }
    }
}
