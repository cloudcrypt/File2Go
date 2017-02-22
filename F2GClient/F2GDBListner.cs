using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2GClient {
    class F2GDBListner {
        private SqlConnection conn = null;
        private string connectionString;
        //private bool connSuccess = false;

        // connection string contains timeout
        F2GDBListner (string connStr) {
            connectionString = connStr;
            //connSuccess = false;
        }

        public async Task<string> CheckQueue(string dbQuery) {
            using (F2GContext db = new F2GContext())
            {
               // Request req = db.Requests.FirstOrDefault(r => r.client.ip == this ip address);
            }
            
            string file = "";
            try {
                conn = await Task.Run(() => {
                    var sql = new SqlConnection();
                    sql.ConnectionString = connectionString;
                    sql.Open();
                    return sql;
                });

                using (SqlCommand command = new SqlCommand(dbQuery, conn)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            // read the requests
                            file = "file.txt";
                        }
                    }
                }
               // connSuccess = true;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                file = "error";
               // connSuccess = false;
            }
            return file;
        }
    }
}
