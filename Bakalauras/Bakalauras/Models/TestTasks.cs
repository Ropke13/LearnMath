using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Models
{
    public class TestTasks
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("fk__Test")]
        public Test Test { get; set; }
        public Guid? fk__Test { get; set; }

        [ForeignKey("fk__Task")]
        public _Task _Task { get; set; }
        public Guid? fk__Task { get; set; }
    }
}
