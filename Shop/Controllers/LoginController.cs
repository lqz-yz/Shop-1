using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COMMON;
using System.Web.Mvc;
using IBLL;
using BLL;
using System.Net;

namespace Shop.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        ILoginBLL BLL = new LoginBLL();
        // GET: Login
        public ActionResult Index()
        {
            //读cookies
            if (Request.Cookies["Name"] != null)
            {
                string name = Server.UrlDecode(Request.Cookies["Name"].Value);
                var result = BLL.Search(x => x.Name == name);
                if (result.Count == 1)
                {
                    if (result[0].Name== name)
                    {
                        Session["user"] = result[0];
                        //下面两句实现滑动过期时间
                        Response.Cookies["Name"].Value = Server.UrlEncode(name);
                        Response.Cookies["Name"].Expires = DateTime.Now.AddDays(1);
                        return Redirect("/Product/List");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string name, string password)
        {
            string salt = ")+_*&^@!&*(";
            password = Md5Helper.Md5(Md5Helper.Md5(salt + password));
            //从数据库查询用户信息，写入到session中
            var result = BLL.Search(x => x.Name == name && x.Password == password);
            if (result.Count==1)
            {
                Session["user"] = result[0];
                Response.Cookies["Name"].Value = Server.UrlEncode(name);
                Response.Cookies["Name"].Expires = DateTime.Now.AddDays(1);
                return Json(new { state = true});
            }
            return Json(new { state = false, msg = "您的用户名或密码输入错误！" });
        }

        public ActionResult SignOut()
        {
            Session["user"] = null;
            Response.Cookies["Name"].Value = null;
            return Redirect("/Login/Index");
        }
    }
}