using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmAbout
    {
        public Setting setting { get; set; }
        public List<Testimonial> testimonials { get; set; }
        public List<Partner> partners { get; set; }
    }
}