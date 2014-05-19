using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.ScriptWiter
{
    public class FileWriter
    {
        List<string> _buffer = new List<string>();
        private string _fileName;
        private StreamWriter _sw;

        public FileWriter(string fileName)
        {
            _fileName = fileName;
        }

        public void Write(string content)
        {
            if(_sw==null)
                _sw = new StreamWriter(_fileName, false, Encoding.UTF8);

            _sw.WriteLine(content);
        }

        public void Close()
        {
            _sw.Close();
        }

        internal void Write(string content, int split=0)
        {
            string path = _fileName;

            if (split != 0)
            {
                FileInfo file = new FileInfo(_fileName);
                path = file.Directory.FullName + "\\" + file.Name.Replace(file.Extension,"") + "-" + split + file.Extension; 
            }

            if (_sw == null)
                _sw = new StreamWriter(path, false, Encoding.UTF8);

            _sw.WriteLine(content);
        }
    }
}
