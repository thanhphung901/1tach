using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vpro.eshop.cpanel.Components
{
    public class clsFormat
    {
        public string MaHoaMatKhau(string Password)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedDataBytes = md5Hasher.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(Password));
            string EncryptPass = Convert.ToBase64String(hashedDataBytes);
            return EncryptPass;
        }
    }
}