using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ESApi.Models.Code.Admin
{
    public class AdminAccountCode
    {
        ESDBEntities db = new ESDBEntities();

        public bool Login(string username, string password)
        {
            
            var user = db.THANHVIENs.Where(tv => tv.TENDANGNHAP == username && tv.MATKHAU == password).SingleOrDefault();
            if (user == null)
                return false;
            return true;
        }

        public void register(string username, string password)
        {
            THANHVIEN t = new THANHVIEN();
            t.TENDANGNHAP = username;
            t.MATKHAU = encryptSHA(password);
            db.THANHVIENs.Add(t);
            db.SaveChanges();
        }

        public string encryptSHA(string data)   // Encode password
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = sha.ComputeHash(encoder.GetBytes(data));
            return System.Text.Encoding.UTF8.GetString(hashedBytes);
        }
    }
}