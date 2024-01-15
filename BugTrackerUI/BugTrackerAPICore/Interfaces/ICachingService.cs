using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerUICore.Interfaces
{
    public interface ICachingService
    {
        string GetFromDistributedCache(string key);
        void SetInDistributedCache(string key, string data);
        bool ExistsinCache(string key, string data);
    }
}
