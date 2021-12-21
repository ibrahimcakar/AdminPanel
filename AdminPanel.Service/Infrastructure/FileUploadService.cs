using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace AdminPanel.Services.Infrastructure
{

    public class FileUploadService:IFileUploadService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileUploadService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public string UploadImage(IFormFile file)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extention = Path.GetExtension(file.FileName);
                string path = Path.Combine(wwwRootPath + "\\media\\uploadedfiles", fileName + DateTime.Now.ToString("ddMMyyfff") + extention);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return path;
            }
            return "";
        }
    }
}
