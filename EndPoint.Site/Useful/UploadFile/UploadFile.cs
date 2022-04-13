using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Persistence.UploadFile
{
    public class UploadFile
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UploadFile(IWebHostEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }
        public UploadDto UploadImage(IFormFile file)
        {
            if (file != null && file.ContentType != "")
            {

                string folder = @"Uploud\Images\";
                string folderResize = @"Uploud\ImgResized\";
                string folderShow = @"/Uploud/Images/";
                string folderShowResized = @"/Uploud/ImgResized/";

                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                var uploadsRootFolderResize = Path.Combine(_hostingEnvironment.WebRootPath, folderResize);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = file.FileName;

                var absolutePath = _hostingEnvironment.WebRootPath + @"\" + folder + file.FileName;
                if (System.IO.File.Exists(absolutePath))
                {
                    fileName = DateTime.Now.Ticks.ToString() + "__" + file.FileName;
                }

                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                {

                    image.Mutate(x => x.Resize(2000, 1333));
                    image.Save(String.Format(@"wwwroot\Uploud\Images\{0}", fileName));

                }
                //  resize img
                using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                {

                    image.Mutate(x => x.Resize(400, 300));
                    image.Save(String.Format(@"wwwroot\Uploud\ImgResized\{0}", fileName));

                }
                return new UploadDto()
                {
                    FileNameAddressResize = folderShowResized + fileName,
                    FileNameAddress = folderShow + fileName,
                    Status = true,
                };
            }
            return null;
        }
        public UploadDto UploadMedia(IFormFile file)
        {
            if (file != null && file.ContentType != "")
            {

                string folder = @$"Uploud\Files\";
                string folderShow = @$"/Uploud/Files/";
                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;

                var absolutePath = _hostingEnvironment.WebRootPath + @"\" + folder + file.FileName;
                if (System.IO.File.Exists(absolutePath))
                {
                    fileName = DateTime.Now.Ticks.ToString() + "__" + file.FileName;
                }
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folderShow + fileName,
                    Status = true,
                };
            }
            return null;
        }
        public UploadDto UploadProfile(IFormFile file)
        {
            if (file != null && file.ContentType != "")
            {

                string folder = @"Uploud\Profile\";
                string folderShow = @"/Uploud/Profile/";
                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                var absolutePath = _hostingEnvironment.WebRootPath + @"\" + folder + file.FileName;
                if (System.IO.File.Exists(absolutePath))
                {
                    fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                }

                using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                {

                    image.Mutate(x => x.Resize(500, 500));
                    image.Save(String.Format(@"wwwroot\Uploud\Profile\{0}", fileName));

                }
                return new UploadDto()
                {

                    FileNameAddress = folderShow + fileName,
                    Status = true,
                };
            }
            return null;
        }
        public class UploadDto
        {
            public long Id { get; set; }
            public bool Status { get; set; }
            public string FileNameAddress { get; set; }
            public string FileNameAddressResize { get; set; }
        }
    }
}
