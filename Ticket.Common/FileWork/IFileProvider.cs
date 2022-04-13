using Microsoft.AspNetCore.Http;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Common.FileWork
{
   public interface IFileProvider
    {
        void GetFileAsync(string fileName, string baseDirectory);

        ResultUploadFileDto AddFileImageQuestion(IFormFile file, string baseDirectory, int width, int Height);
        ResultUploadFileDto AddFileImage(IFormFile file, string baseDirectory,string fileName, int width, int Height);
          ResultUploadFileDto AddFile(IFormFile file, string baseDirectory);

        void DeleteFileAsync(string fileName, string baseDirectory);
    }
}
