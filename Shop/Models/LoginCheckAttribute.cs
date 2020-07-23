using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Models
{
    public class LoginCheckAttribute: AuthorizeAttribute
    {
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    //检测Controller是否包含AllowAnonymousAttribute特性
        //    if (filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any())
        //    {
        //        return;
        //    }
        //    //判断session容器中是否包含用户信息，如果包含继续执行，否则跳转到登录页
        //    if (filterContext.HttpContext.Session["user"] == null)
        //    {
        //        //重定向到登录页
        //        filterContext.Result = new RedirectResult("/Login/Index");
        //    } 
        //    //base.OnAuthorization(filterContext);
        //}

        /// <summary>
        /// 判断验证是否通过
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["user"] != null;
        }

        /// <summary>
        /// 验证未通过
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Login/Index");
        }
    }
}