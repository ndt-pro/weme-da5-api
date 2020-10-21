using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeMe.Services
{
    public interface IFileService
    {
        string WriteFile(IFormFile file, int type);
    }

    public class FileService : IFileService
    {
        private string dir = "Uploads/images";
        private string[] types = new string[] {
            "/avatars",
            "/threads",
        };

        public string WriteFile(IFormFile file, int type = 0)
        {
            string dir = this.dir + types[type];
            try
            {
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), dir);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileExtension = Path.GetExtension(fileName);
                    fileName = Guid.NewGuid() + fileExtension;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return fileName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
