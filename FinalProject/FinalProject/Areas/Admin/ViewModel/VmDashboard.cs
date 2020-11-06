using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class VmDashboard
    {
        public List<Testimonial> Testimonials { get; set; }
        public List<User> Users { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Comment> Comments { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
        public List<BlogTags> Tags { get; set; }
        public List<Subscribe> Subscribes { get; set; }
        public List<Message> Messages { get; set; }
    }
}