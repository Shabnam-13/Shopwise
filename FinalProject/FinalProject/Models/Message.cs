using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Deployment.Internal;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Message
    {
        public int Id { get; set; }
        
        [Required,MaxLength(50)]
        public string Name { get; set; }

        [Required,MaxLength(250)]
        public string Email { get; set; }

        [Required, MaxLength(15)]
        public string Phone { get; set; }

        [Required, MaxLength(250)]
        public string Subject { get; set; }

        [Required, MaxLength(1000)]
        public string Messages { get; set; }

        public DateTime SendDate { get; set; }
    }
}