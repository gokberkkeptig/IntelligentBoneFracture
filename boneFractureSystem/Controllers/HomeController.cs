using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Xceed.Wpf.Toolkit;


namespace boneFractureSystem.Controllers
{
    public class HomeController : Controller
    {
        private object MessageBoxButtons;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page";

            return View();
        }

        public ActionResult Detection()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetImage()
        {
            var dir = Server.MapPath("~/Google Drive/IntelligentBoneFracturesDetectionandClassificationSystem/output");
            var path = Path.Combine(dir,  "1.png");

            for (; ; )
            {
                if (System.IO.File.Exists(path))
                {
                    System.Windows.Forms.MessageBox.Show("Output is ready!", "Success", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return base.File(path, "image/png");
                    break;
                }
            }

            
        }
        public ActionResult Upload()
        {
             
            bool isSavedSuccessfully = true;
            string fName = "";
            var outputDir = Server.MapPath("~/Google Drive/UploadedImages");
            var outPath = Path.Combine(outputDir, "1.png");
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    string extension = System.IO.Path.GetExtension(file.FileName);
                    if (extension == ".jpg" || extension == ".png")
                    {
                        if (file != null && file.ContentLength > 0 && file.ContentLength < 2097152)
                        {
                            var path = Path.Combine(Server.MapPath("~/Google Drive/IntelligentBoneFracturesDetectionandClassificationSystem/upload"));
                            string pathString = System.IO.Path.Combine(path.ToString());
                            var fileName1 = Path.GetFileName(file.FileName);
                            bool isExists = System.IO.Directory.Exists(pathString);
                            if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                            //var uploadpath = string.Format("{0}\\{1}", pathString, file.FileName);
                            var uploadpath = string.Format("{0}\\{1}", pathString, "1.png");
                            file.SaveAs(uploadpath);
                            System.Windows.Forms.MessageBox.Show("Image is Uploaded","Success",System.Windows.Forms.MessageBoxButtons.OK,MessageBoxIcon.Information);
                           

                            return RedirectToAction("Detection","Home");
                            
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Image cannot be bigger than 2 MB", "Error", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Only.jpg or.png are allowed!", "Failed", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return RedirectToAction("Detection");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Image is NOT Uploaded");
                isSavedSuccessfully = false;
                return RedirectToAction("Detection");
            }
            if (isSavedSuccessfully)
            {
                
                return Json(new
                {
                    Message = fName
                });
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Image is NOT Uploaded");
                return Json(new
                {
                    Message = "Error in saving file"
                });
            }



        }
    }

}