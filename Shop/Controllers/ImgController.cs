using COMMON;
using Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ImgController : Controller
    {
        public ActionResult Upload()
        {
            List<string> fileNames = new List<string>();
            var files = Request.Files;   //["Img"]
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file == null)
                {
                    return Json(new { state = false, msg = "图片不存在，请重新上传" });
                }
                byte[] bytes = Common.StreamToBytes(file.InputStream);
                var fileExtension = FileHelper.CheckTextFile(bytes);

                if (fileExtension == FileExtension.VALIDFILE)
                {
                    return Json(new { state = false, msg = "上传的文件已损坏" });
                }
                if (!(fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
                {
                    return Json(new { state = false, msg = "上传的类型有误" });
                }
                DateTime dt = DateTime.Now;
                Random random = new Random();
                int randomNum = random.Next(0, 99999);
                var extension = Path.GetExtension(file.FileName);
                var newFileName = Guid.NewGuid().ToString("N") + randomNum.ToString() + extension;
                //dt.ToString("yyyyMMddHHmmssfffff") + randonmNum.ToString() + extension;

                var imgType = Request["imgType"];
                string upLoadPath = null;
                if (WebConstants.ImgPathDicts.ContainsKey(imgType))
                {
                    upLoadPath = WebConstants.ImgPathDicts[imgType];
                }
                else
                {
                    return Json(new { state = false });
                }
                string fullPath = upLoadPath + newFileName;
                file.SaveAs(Server.MapPath(fullPath));
                fileNames.Add(fullPath);
            }
            return Json(fileNames);
        }
    }
}