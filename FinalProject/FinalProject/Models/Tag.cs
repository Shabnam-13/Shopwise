using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public int BlogId { get; set; }
        
        public int BlogTagsId { get; set; }
        
        public Blog blog { get; set; }

        public BlogTags blogTags { get; set; }
    }
}