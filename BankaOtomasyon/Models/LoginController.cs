using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankaOtomasyon.Models
{
    public class LoginController:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
            
                httpContext.Response.Redirect("~/Uyelik/Giris");
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}