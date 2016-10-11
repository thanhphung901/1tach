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

        public static string Generate_Random_String(int Length)
        {
            string _allowedChars = "abcdefghijk0123456789mnopASXZDCQWERFBTGNYHMUJMIKOLPqrstuvwxyz";
            Random randNum = new Random();
            char[] chars = new char[Length];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < Length; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}