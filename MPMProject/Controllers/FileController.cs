using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MPMProject.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    public class FileController : BaseController
    {
        private IHostingEnvironment hostingEnv;
        string[] fileFormatArray = { "lic" };
        public FileController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        [HttpPost]
        public IActionResult Post()
        {
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            int flag = 0;

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                flag = 1;
                //return Json("pdf total size > 100MB , server refused !");
            }

            List<string> filePathResultList = new List<string>();

            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                System.IO.File.Delete("licence.txt");

                string filePath = hostingEnv.WebRootPath + $@"/Files/Files/";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string suffix = fileName.Split('.')[1];
                if (!fileFormatArray.Contains(suffix))
                {
                    flag = 1;
                    //return Json("the file format not support ! you must upload files that suffix like 'pdf'.");
                }
                //fileName = Guid.NewGuid() + "." + suffix;//对上传的文件名加密

                string fileFullName = filePath + "licence.txt";

                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                

                filePathResultList.Add($"/src/Files/{fileName}");
            }
            if (flag == 1)
            {
                return Json("Fail");
            }
            else
            {
                return Json(filePathResultList[0].Remove(0, 10));
            }

        }
    }
}