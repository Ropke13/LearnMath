using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Models
{
    public class API_Pods
    {
        [Key]
        public Guid Id { get; set; }
        public string imgId { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string Alt { get; set; }
        public string ImgWidth {get; set;}
        public string ImgHeight { get; set; }
        public bool IsSaved { get; set; }
        public string ImgName { get; set; }

        [ForeignKey("fk__Task")]
        public _Task _Task { get; set; }
        public Guid fk__Task { get; set; }

    }
}
