using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Common.FileWork
{
    public interface IFileSystem
    {
        bool FileExists(string path);
        string ReadFileText(string path);
        void WriteFileText(string path, string text);
        void WriteFile(string path, byte[] data);
        void DeleteFile(string path);
        DateTime GetFileLastWriteTime(string path);
        bool DirectoryExists(string path);
        void CreateDirectory(string path);
        IEnumerable<string> EnumerateFiles(string directoryPath);
        void AppendFile(string path, byte[] data);
        void AppendFile(string path, byte[] data, int offset, int count);
        string SetFolderUpload(string categoryName, string format);
    }
}
