using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? Attempt { get; set; }

        [ForeignKey("fk__Test")]
        public Test Test { get; set; }
        public Guid? fk__Test { get; set; }

        [ForeignKey("fk__User")]
        public ApplicationUser User { get; set; }
        public string fk__User { get; set; }
    }
}
