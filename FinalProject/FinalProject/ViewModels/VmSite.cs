using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmSite
    {
        public List<Question> questions { get; set; }
        public List<Term> terms { get; set; }

        public Contact contact { get; set; }
    }
}