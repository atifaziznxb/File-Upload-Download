using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestFileUpload.Controllers
{
    public class AttachmentController : Controller
    {
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ContentResult UploadInChunks(int? chunk, string name)
        {
            // Set upload temp path
            string Parent = Request.QueryString["Parent"];
            string ParentID = Request.QueryString["ParentID"];
            if (string.IsNullOrEmpty(ParentID))
            {
                ParentID = "0";
            }

            var fileUpload = Request.Files[0];
            var uploadPath = Server.MapPath("~/Uploads/Temp/Atif/Documents/1/");

            // If directory does not exist then create
            Directory.CreateDirectory(uploadPath);

            // Upload files
            chunk = chunk ?? 0;
            using (var fs = new FileStream(Path.Combine(uploadPath, name), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }
            //return Content("chunk uploaded");//"chunk uploaded", "text/plain"
            return Content("{\"name\":\"" + Request.Files[0].FileName + "\",\"type\":\"" + Request.Files[0].ContentType + "\",\"size\":\"" + string.Format("{0} bytes", Request.Files[0].ContentLength) + "\"}", "application/json");
        }

        public FileResult Download(int id)
        {
            var file = Server.MapPath("~/Uploads/Temp/Atif/Documents/1/IMG_0978.JPG");

                byte[] fileBytes = System.IO.File.ReadAllBytes(file);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "IMG_0978.JPG");
        }

        public ActionResult Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                file.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Uploads/" + file.FileName);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}