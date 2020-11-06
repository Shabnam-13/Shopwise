using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmBlog
    {
        public List<Blog> Blogs { get; set; }
        public List<BlogCategory> Categories { get; set; }
        public List<BlogImages> Images { get; set; }
        public List<BlogTags> BlogTags { get; set; }
        public List<Tag> tags { get; set; }
        public List<Comment> comments { get; set; }
        public List<SaleBanner> saleBanners { get; set; }

        public Tag Tag { get; set; }
        public Blog Blog { get; set; }
        public BlogCategory Category { get; set; }
        public BlogTags blogTag { get; set; }

        public bool? isReply { get; set; }
        public int? CommentId { get; set; }
    }
}