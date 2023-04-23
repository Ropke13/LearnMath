using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Models
{
    public class TestCompletedTask
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? TaskId { get; set; }
        public string SelectedAnswer { get; set; }
        public bool IsAnsweredCorrectly { get; set; }

        [ForeignKey("fk__TestComplete")]
        public TestComplete TestComplete { get; set; }
        public Guid? fk__TestComplete { get; set; }
    }
}
