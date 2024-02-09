using BugTrackerAPICall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string DecryptData(byte[] dataToDecrypt)
        {
            string decryptedData;
            using(Aes aes = Aes.Create())
            {
                decryptedData = DecryptStringFromBytes(dataToDecrypt, aes.Key, aes.IV);
            }

            return decryptedData;
        }

        private string DecryptStringFromBytes(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            string decryptedData;

            using(Aes aesInstance = Aes.Create())
            {
                aesInstance.Key = key;
                aesInstance.IV = iv;

                using(MemoryStream ms = new MemoryStream(dataToDecrypt))
                {
                    using(CryptoStream cryptoStream = new CryptoStream(ms, aesInstance.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using(StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            decryptedData = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedData;
        }

        public byte[] EncryptData(string dataToEncrypt)
        {
            byte[] encryptedData;
            using(Aes aes = Aes.Create())
            {
                encryptedData = EncryptStringToByte(dataToEncrypt, aes.Key, aes.IV);
            }

            return encryptedData;
        }

        private byte[] EncryptStringToByte(string dataToEncrypt, byte[] key, byte[] iv)
        {
            byte[] encryptedData;
            using(Aes aesInstance = Aes.Create())
            {
                aesInstance.Key = key;
                aesInstance.IV = iv;

                using(MemoryStream memory = new MemoryStream())
                {
                    using(CryptoStream cryptoStream = new CryptoStream(memory, aesInstance.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using(StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(dataToEncrypt);
                        }

                        encryptedData = memory.ToArray();
                    }
                }
            }

            return encryptedData;
        }
    }
}
