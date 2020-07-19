using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using MODEL;

namespace Shop.Controllers
{
    public class BaseController<T, B> : Controller
        where T : BaseModel, new()
        where B : IBaseBLL<T>
    {
        public virtual B Bll { get; set; }

        [HttpGet]
        public virtual ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            var result = Bll.Delete(id);
            return Json(new { state = result > 0 ? true : false, msg = result > 0 ? "删除成功！" : "删除失败！" });
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            return View();
        }
    }
}