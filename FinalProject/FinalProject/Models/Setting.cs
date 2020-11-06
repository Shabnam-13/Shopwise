using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Setting
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string LogoDark { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoDarkFile { get; set; }

        [MaxLength(250)]
        public string LogoLight { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoLightFile { get; set; }

        [MaxLength(250)]
        public string VideoLink { get; set; }

        [MaxLength(250)]
        public string AboutImg { get; set; }

        [NotMapped]
        public HttpPostedFileBase AboutImgFile { get; set; }

        [MaxLength(250)]
        public string HomeImg { get; set; }

        [NotMapped]
        public HttpPostedFileBase HomeImgFile { get; set; }

        [MaxLength(250)]
        public string AboutTitle { get; set; }

        [MaxLength(250)]
        public string AboutSubtitle { get; set; }

        [MaxLength(1000)]
        public string AboutContent { get; set; }

        [MaxLength(50)]
        public string Copyright { get; set; }
    }
}