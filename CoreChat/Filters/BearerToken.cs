using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreChat.Filters
{
    public class BearerToken : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string auth = context.HttpContext.Request.Headers["Authorization"].ToString();
            string token = auth.Replace("Bearer ", "");
            if (String.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            var identity = new ClaimsIdentity(context.HttpContext.User.Identity);
            identity.AddClaim(new Claim("token", token));

            var principal = new ClaimsPrincipal(identity);
            context.HttpContext.User = principal;

            base.OnActionExecuting(context);
        }
    }
}
