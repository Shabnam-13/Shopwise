using System.Web.Mvc;

namespace FinalProject.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller="Home", action = "Login", id = UrlParameter.Optional },
                new[] { "FinalProject.Areas.Admin.Controllers" }
            );
        }
    }
}