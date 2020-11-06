using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required,MaxLength(250)]
        public string Questions { get; set; }

        [Required,MaxLength(500)]
        public string Reply { get; set; }

        [Column(TypeName ="bit"),Display(Name ="Is general question?")]
        public bool isGeneral { get; set; }
    }

}