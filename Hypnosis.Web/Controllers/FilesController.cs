using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypnosis.Web.MyHelpers;


namespace Hypnosis.Web.Controllers
{
    public class FilesController : Controller
    {
        public ActionResult Get(string fileName)
        {
            var filePath = FilesHelper.RelativePath + fileName;
            var result = new FilePathResult(filePath, System.Web.MimeMapping.GetMimeMapping(fileName));
            result.FileDownloadName = fileName;
            if (System.IO.File.Exists(Server.MapPath(result.FileName)))
            {
                return result;
            }
            else
            {
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
        }
    }
}