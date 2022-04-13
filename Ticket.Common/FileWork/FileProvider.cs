using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Azmoon.Common.ResultDto;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Azmoon.Common.FileWork
{
    public class FileProvider : IFileProvider
    {
        private readonly IHostEnvironment hostEnvironment;



        public FileProvider(IHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;

        }

        public void GetFileAsync(string fileName, string baseDirectory)
        {
            fileName = GetFilePath(fileName , baseDirectory);

            if (File.Exists(fileName))
            {
                File.ReadAllBytesAsync(fileName);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        public ResultUploadFileDto AddFileImageQuestion(IFormFile file, string baseDirectory, int width, int Height)
        {
       
            string directory = GetDirectory(baseDirectory);
            //string fileName = file.FileName;
            string fileName = DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss") + "__"+ file.FileName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var UploadfileName = this.GetFilePath(fileName, baseDirectory);
            // var UploadfileName =  baseDirectory+"/"+ fileName;
            using (var image = Image.Load(file.OpenReadStream()))
            {

                image.Mutate(x => x.Resize(width, Height));
                image.Save(String.Format(UploadfileName));


            }
            return new ResultUploadFileDto
            {
                Status = true,
                FileNameAddress = fileName
            };
        }
        public ResultUploadFileDto AddFileImage(IFormFile file , string baseDirectory , string filename, int width, int Height)
        {
            var fileType = file.ContentType.Split("/");
            string directory = GetDirectory(baseDirectory);
            //string fileName = file.FileName;
            string fileName = filename+"."+ fileType[1];
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

          var UploadfileName = this.GetFilePath(fileName,  baseDirectory);
          // var UploadfileName =  baseDirectory+"/"+ fileName;
            using (var image = Image.Load(file.OpenReadStream()))
            {

                image.Mutate(x => x.Resize(width, Height));
                image.Save(String.Format(UploadfileName));
               
                
            }
            return new ResultUploadFileDto { 
            Status=true,
            FileNameAddress= fileName
            };
        }
        public ResultUploadFileDto AddFile(IFormFile file, string baseDirectory)
        {
            string directory = GetDirectory(baseDirectory);
            string fileName = file.FileName+"__"+ DateTime.Now.ToShortTimeString();
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
          
            var UploadfileName = this.GetFilePath(fileName, baseDirectory);
            var UploadfileName2 = baseDirectory + "\\" + fileName;
           string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

            using (var stream = new FileStream(UploadfileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return new ResultUploadFileDto
            {
                Status = true,
                FileNameAddress = fileName
            };
        }
        public void DeleteFileAsync(string fileName, string baseDirectory)
        {
            fileName = GetFilePath(fileName, baseDirectory);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }


        }

        private string GetDirectory(string baseDirectory)
        {
            return Path.Combine(
                this.hostEnvironment.ContentRootPath,
                baseDirectory);
        }

        private string GetFilePath(string fileName , string baseDirectory)
        {
            return Path.Combine(
                GetDirectory(baseDirectory),
                fileName);
        }

      
    }
}
