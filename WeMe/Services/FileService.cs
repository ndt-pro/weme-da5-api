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
        void SetDir(string path);
        string WriteFile(IFormFile file);
    }

    public class FileService : IFileService
    {
        private string dir = "Uploads/images";
        public void SetDir(string dir)
        {
            this.dir = dir;
        }

        public string WriteFile(IFormFile file)
        {
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
