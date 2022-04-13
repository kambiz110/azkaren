using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Common.FileWork
{
  
    public class PhysicalFileSystem : IFileSystem
    {
        private readonly IHostEnvironment hostEnvironment;

        public PhysicalFileSystem(IHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string ReadFileText(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteFileText(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        public IEnumerable<string> EnumerateFiles(string directoryPath)
        {
            return Directory.EnumerateFiles(directoryPath);
        }

        public DateTime GetFileLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }

        public void WriteFile(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }

        public void AppendFile(string path, byte[] data)
        {
            Stream outStream = File.OpenWrite(path);
            outStream.Seek(0, SeekOrigin.End);
            outStream.Write(data, 0, data.Length);
        }

        public void AppendFile(string path, byte[] data, int offset, int count)
        {
            using (Stream outStream = File.OpenWrite(path))
            {
                outStream.Seek(0, SeekOrigin.End);
                outStream.Write(data, offset, count);
            }
        }
        public string SetFolderUpload(string categoryName, string format)
        {
   
            string path = hostEnvironment.ContentRootPath  +"\\wwwroot\\Upload\\"+ format +"\\"+ categoryName;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}
