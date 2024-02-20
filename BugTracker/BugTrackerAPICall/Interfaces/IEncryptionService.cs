using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Interfaces
{
    public interface IEncryptionService
    {
        
        byte[] EncryptData(string dataToEncrypt, string key);
        string DecryptData(byte[] dataToDecrypt, string key);
    }
}
