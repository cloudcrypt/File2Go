using F2GWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace F2GWeb.Models
{
    public class User
    {
        public string email { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string hash { get; set; }
        public DateTime created { get; set; }

        public List<File> files { get; set; }
        public List<Client> clients { get; set; } 

        public User() { }

        public User(string email, string password)
        {
            this.email = email;
            hash = getHash(password);
        }

        public User(RegisterViewModel vm)
        {
            email = vm.Email;
            fname = vm.fname;
            lname = vm.lname;
            hash = getHash(vm.Password);
            created = DateTime.Now;
        }

        public User(LoginViewModel vm)
        {
            email = vm.Email;
            hash = getHash(vm.Password);
        }

        private static string getHash(string pswd)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(pswd);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
