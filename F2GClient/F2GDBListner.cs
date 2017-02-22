using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F2G.Models;
using System.ComponentModel;
using System.Threading;

namespace F2GClient {
    class F2GDBListner {
        volatile bool fileFound = false;
        private string ipAddr;
        private BackgroundWorker bw;
        public event EventHandler FileFound;

        F2GDBListner (string ip) {
            ipAddr = ip;
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
        }

        public void CheckQueue() {
            while (!fileFound) {
                if (!bw.IsBusy) {
                    bw.DoWork += bw_DoWork;
                    bw.RunWorkerAsync();
                }
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e) {
            try {
                using (F2GContext db = new F2GContext()) {
                    Request req = db.Requests.FirstOrDefault(r => r.client.ip == ipAddr);
                    if (req != null) {
                        fileFound = true;
                        OnFileFound(new FileFoundEventArgs { FileName = req.fileName });
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        public class FileFoundEventArgs : EventArgs {
            public string FileName { get; set; }
        }

        public delegate void FileFoundEventHandler(FileFoundEventArgs e);

        protected virtual void OnFileFound(EventArgs e) {
            EventHandler handler = FileFound;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}
