using AnimeLib.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public string Post([FromForm] FileUpload objectFile)
        {
            try
            {
                if (objectFile.files.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\Images\\";
                    using (FileStream fs = System.IO.File.Create(path + objectFile.files.FileName))
                    {
                        objectFile.files.CopyTo(fs);
                        fs.Flush();
                        return "Upload complete.";
                    }
                }
                else
                {
                    return "Upload Error. No files given.";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
