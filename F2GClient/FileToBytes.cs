using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2GClient {
    class FileToBytes {
        private string filePath;
        private int segmentSize;

        FileToBytes(string path, int segmentSize) {
            filePath = path;
            this.segmentSize = segmentSize;
        }

        FileToBytes(int segmentSize) {
            this.segmentSize = segmentSize;
            filePath = "";
        }

        public void setFilePath(string s) {
            filePath = s;
        }

        public List<byte[]> getFile() {
            if (filePath.Length > 1) {
                byte[] file = File.ReadAllBytes(filePath);
                return null;
            } else {
                return null;
            }
        }

    }
}
