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
        public string DecryptData(byte[] dataToDecrypt, string key)
        {
            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            string decryptedData;
            using(Aes aes = Aes.Create())
            {
                decryptedData = DecryptStringFromBytes(dataToDecrypt, byteKey, aes.IV);
            }

            return decryptedData;
        }

        private string DecryptStringFromBytes(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            string decryptedData;
            byte[] decryptedDataStream;
            byte[] newIV = new byte[16];
            /**
             *  FIX Issue: 
             *  Byte list is different when converted from a string back to a byte list
             * */

            using (Aes aesInstance = Aes.Create())
            {
                aesInstance.Key = key;
                aesInstance.IV = newIV;

                using(MemoryStream ms = new MemoryStream(dataToDecrypt))
                {
                    using(CryptoStream cryptoStream = new CryptoStream(ms, aesInstance.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        /*using(MemoryStream decryptedStream = new MemoryStream())
                        {
                            cryptoStream.CopyTo(decryptedStream);
                            decryptedDataStream = decryptedStream.ToArray();
                        }*/
                        using(StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            decryptedData = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedData;
        }

        public byte[] EncryptData(string dataToEncrypt, string key)
        {
            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            byte[] encryptedData;
            using(Aes aes = Aes.Create())
            {
                encryptedData = EncryptStringToByte(dataToEncrypt, byteKey, aes.IV);
            }

            return encryptedData;
        }

        private byte[] EncryptStringToByte(string dataToEncrypt, byte[] key, byte[] iv)
        {
            byte[] encryptedData;
            byte[] newIV = new byte[16];

            using(Aes aesInstance = Aes.Create())
            {
                aesInstance.Key = key;
                aesInstance.IV = newIV;

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
