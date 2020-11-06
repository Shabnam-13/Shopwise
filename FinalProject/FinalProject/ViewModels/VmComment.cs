using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmComment
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int ItemId { get; set; }
        public int? CommentId { get; set; }
        public bool? isReply { get; set; }
    }
}