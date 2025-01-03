using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace WPF_POS
{
    public class IOManager
    {
        private string path = @".\data";
        private string extention = ".json";

        private void CreateFolder()
        {
            Directory.CreateDirectory(path);
        }

        private bool IsPathExisted()
        {
            return File.Exists(path);
        }

        private string GetFullPath(string fileName)
        {
            return string.Format($"{path}\\{fileName}{extention}");
        }

        public void Write(string fileName, Object obj)
        {
            if(!IsPathExisted()) //if path doesn't exist
            {
                CreateFolder(); //create path
            }

            string fullPath = GetFullPath(fileName);
            StreamWriter streamWriter = new StreamWriter(fullPath);
            string content = JsonConvert.SerializeObject(obj);
            streamWriter.Write(content);
            streamWriter.Close();
        }

        public T Read<T>(string fileName)
        {
            string fullPath = GetFullPath(fileName);
            if(!File.Exists(fullPath))
            {
                return default(T);
            }

            //string content = File.ReadAllText(fullPath);
            StreamReader streamReader = new StreamReader(fullPath);
            string content = streamReader.ReadToEnd();
            streamReader.Close();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}