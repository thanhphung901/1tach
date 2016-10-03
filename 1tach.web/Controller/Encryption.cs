
namespace Controller
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// The encryption.
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// The get md 5 hash data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMd5HashData(string data)
        {
            MD5 md5 = MD5.Create();
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();

            foreach (byte t in hashData)
            {
                returnValue.Append(t.ToString());
            }
            return returnValue.ToString();
        }

        /// <summary>
        /// The get sha 1 hash data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSha1HashData(string data)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();
            
            foreach (byte t in hashData)
            {
                returnValue.Append(t.ToString());
            }
            
            return returnValue.ToString();
        }

        /// <summary>
        /// The validate md 5 hash data.
        /// </summary>
        /// <param name="inputData">
        /// The input data.
        /// </param>
        /// <param name="storedHashData">
        /// The stored hash data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ValidateMd5HashData(string inputData, string storedHashData)
        {
            string getHashInputData = this.GetMd5HashData(inputData);

            if (string.CompareOrdinal(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The validate sha 1 hash data.
        /// </summary>
        /// <param name="inputData">
        /// The input data.
        /// </param>
        /// <param name="storedHashData">
        /// The stored hash data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ValidateSha1HashData(string inputData, string storedHashData)
        {
            string getHashInputData = this.GetSha1HashData(inputData);

            if (string.CompareOrdinal(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}