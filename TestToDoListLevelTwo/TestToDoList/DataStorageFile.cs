﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestToDoList {
    public class DataStorageFile {
        static string _FileName = System.IO.Path.Combine(Environment.CurrentDirectory, "ToDoList.txt");
        FileInfo _FileInfo = new FileInfo(_FileName);
        MemoryStream _MemoryStream = new MemoryStream();
        List<string> _StringsFromFile = new List<string>();
        public DataStorageFile() {
        }
        public int OpenFile() {
            if (_FileInfo.Exists) {
                if (_FileInfo.Length == 0) {
                    return -1;
                }
                else {
                    // store file to memory
                    using (FileStream _FileStream = File.OpenRead(_FileName)) {
                        _FileStream.CopyTo(_MemoryStream);
                        _FileStream.Close();
                        _MemoryStream.Position = 0;
                        using (var reader = new StreamReader(_MemoryStream, System.Text.Encoding.Default)) {
                            string line;
                            while ((line = reader.ReadLine()) != null) {
                                _StringsFromFile.Add(line);
                            }
                        }

                    }
                    using (FileStream _NewFileStream = File.OpenWrite(_FileName)) {
                        _NewFileStream.SetLength(0);
                        _NewFileStream.Close();
                    }
                }
            }
            else {
                _FileInfo.Create();
            }
            return 0;
        }
        public String GetTheNextStringFromFile() {
            String str = null;
            if (_StringsFromFile.Count == 0){
                return str;
            }
            str = _StringsFromFile[0];
            _StringsFromFile.RemoveAt(0);
            return str;
        }
        public void WriteLineToFile(String str) {
            using (StreamWriter _StreamWriter = new StreamWriter
            (_FileName, true, System.Text.Encoding.Default)) {
                _StreamWriter.WriteLine(str);
            }
        }
    }
}
