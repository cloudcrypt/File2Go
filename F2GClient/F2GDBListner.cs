using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F2G.Models;
using System.ComponentModel;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using F2GClient;

namespace F2GClient {
    public class F2GDBListner {
        volatile bool fileFound = false;
        private string ipAddr;
        private BackgroundWorker bw;
        private Window1 window1;

        public event EventHandler<FileFoundEventArgs> FileFound;


        public F2GDBListner (string ip) {
            ipAddr = ip;
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;

        }

        public F2GDBListner(string ip, Window1 window1) : this(ip)
        {
            this.window1 = window1;
            ipAddr = ip;
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
        }

        public async void CheckQueue() {
            while (!fileFound) {
                var t = await Task.Run(() => checkDB());
                //t.Wait();
            }
        }

        private async Task<bool> checkDB() {
            try {
                using (F2GContext db = new F2GContext()) {
                    //Request req = db.Requests.Include(r => r.User).Include(r => r.client).FirstOrDefault(r => r.client.ip == ipAddr);
                    Request req = db.Requests.FirstOrDefault(r => r.client.ip == ipAddr);
                    if (req != null) {
                        fileFound = true;
                        window1.displayConnection();
                        FileFound(this, new FileFoundEventArgs { RequestData = req });
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return fileFound;
        }

      
        public class FileFoundEventArgs : EventArgs {
            public Request RequestData { get; set; }
            
        }

        //public delegate void FileFoundEventHandler(object sender, FileFoundEventArgs e);

        //protected virtual void OnFileFound(EventArgs e) {
        //    EventHandler handler = FileFound;
        //    if (handler != null) {
        //        handler(this, e);
        //    }
        //}
    }
}
