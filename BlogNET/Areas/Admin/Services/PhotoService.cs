using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Areas.Admin.Services
{
    public class PhotoService
    {
        private readonly IWebHostEnvironment _env;

        public PhotoService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string SavePhoto(IFormFile file)
        {
            if (file == null) return null;
                        
            var ext = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid() + ext;
            var path = Path.Combine(_env.WebRootPath, "uploads", fileName);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }

        public void DeletePhoto(string photoPath)
        {
            if (string.IsNullOrEmpty(photoPath))
            {
                return;
            }

            try
            {
                var deletePath = Path.Combine(_env.WebRootPath, "uploads", photoPath);
                System.IO.File.Delete(deletePath);
            }
            catch (Exception) { }
        }
    }
}
