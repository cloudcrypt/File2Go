using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2GClient {
    public class Search4File {
        private static List<string> filePaths;
        private static string fileName;

        private static void searchDrive(DriveInfo d) {
            searchDirectory(d.Name);
        }
        
        private static void searchDirectory(string d) {
            try {
                // check top level
                foreach (var file in Directory.GetFiles(d, fileName)) {
                    filePaths.Add(file);
                }
                foreach (var dir in Directory.GetDirectories(d)) {
                    bool ignore = !(dir.Contains("C:\\Windows") ||
                                    dir.Contains("C:\\Program Files") ||
                                    dir.Contains("C:\\Program Files (x86)") ||
                                    dir.Contains("C:\\ProgramData") ||
                                    dir.Contains("C:\\Logs") ||
                                    dir.Contains("\\$RECYCLE.BIN") ||
                                    dir.Contains("\\$Recycle.Bin") ||
                                    dir.Contains("\\System Volume Information") ||
                                    dir.Contains("\\Recovery") ||
                                    dir.Contains("\\Documents and Settings") ||
                                    dir.Contains("\\Users\\Default") ||
                                    dir.Contains("\\Users\\All Users") ||
                                    dir.Contains("\\Users\\Default.migrated") ||
                                    dir.Contains("\\Users\\Public") ||
                                    dir.Contains("\\AppData"));
                    if (ignore) {
                        searchDirectory(dir); // search sub directories
                    }      
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public static string Search(string fn) {
            filePaths = new List<string>();
            fileName = fn;

            foreach(var d in DriveInfo.GetDrives()) {
                searchDrive(d);
            }

            string results = "";
            foreach(var s in filePaths) {
                results += (s + "\n");
            }
            return results;
        }
    }
}
