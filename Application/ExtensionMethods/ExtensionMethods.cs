using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ExtensionMethods
{
    public static class FormFileExtensions
    {
        public static  byte[] GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static string SaveFile(this IFormFile formFile)
        {
            string rootPath = @"C:\Blogger_Attachments";
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            string filePath = Path.Combine(rootPath, $"{Guid.NewGuid()}_{formFile.FileName}");

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                formFile.CopyTo(fileStream);

            return filePath;
        }
    }
}
