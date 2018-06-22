using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace TextEditor.BL
{
    public interface IFileManager
    {
        string GetContent(string filePatch);
        string GetContent(string filePatch, Encoding encoding);
        void SaveContent(string content, string filePatch);
        void SaveContent(string content, string filePatch, Encoding encoding);
        int GetSymbolCount(string content);
        bool IsExist(string filePatch);
    }
    public class FileManager:IFileManager
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);
        public bool IsExist (string filePatch)
        {
            bool isExist = File.Exists(filePatch);
            return isExist;
        }
        public string GetContent (string filePatch)
        {
            return GetContent(filePatch, _defaultEncoding);
        }
        public string GetContent (string filePatch, Encoding encoding)
        {
            string content = File.ReadAllText(filePatch, encoding);
            return content;
        }
        public void SaveContent(string content, string filePatch)
        {
           SaveContent(content, filePatch, _defaultEncoding);
        }
        public void SaveContent (string content, string filePatch, Encoding encoding)
        {
            File.WriteAllText(content, filePatch, encoding);
        }
        public int GetSymbolCount (string content)
        {
            int count = content.Length;
            return count;
        }

    }
}
