using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerUICore.Interfaces
{
    public interface IJwtService
    {
        string GenerateJsonWebToken(string userName, string dataToSend);
    }
}
