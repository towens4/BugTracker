using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Interfaces
{
    public interface IEncryptionService
    {
        byte[] EncryptData(string dataToEncrypt);
        string DecryptData(byte[] dataToDecrypt);
    }
}
