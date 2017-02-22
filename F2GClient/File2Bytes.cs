using System;
using System.IO;
using System.Collections.Generic;
using F2G.Models;

namespace F2GClient {
    public class File2Bytes {
        public static List<byte[]> ConvertFile2ByteList(string filePath, int segmentSize) {
            if (filePath.Length > 1) {
                List<byte[]> file = new List<byte[]>();
                filePath = filePath.Substring(0, filePath.Length - 1);
                byte[] f = System.IO.File.ReadAllBytes(filePath);
                int pieces = f.Length / segmentSize;
                int count = 0;
                for (count = 0; count < pieces; count++) {
                    byte[] piece = new byte[segmentSize];
                    Array.ConstrainedCopy(f, count * segmentSize, piece, 0, segmentSize);
                    file.Add(piece);
                }
                byte[] remainder = new byte[f.Length - count * segmentSize];
                Array.ConstrainedCopy(f, count * segmentSize, remainder, 0, remainder.Length);
                file.Add(remainder);
                return file;
            } else {
                return null;
            }
        }


        public static byte[] ConvertFileToBytes(string filePath) {
            if (filePath.Length > 1) {
                try {
                    return System.IO.File.ReadAllBytes(filePath);
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            } 
            return null;
        }

        //public static void SendToServer(string filePath) {
        //    using (F2GContext db = new F2GContext()) {
        //        db.
        //    }
        //}

        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray) {
            try { // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            } catch (Exception _Exception) {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }
    }
}
