using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Helper
{
    public class CallerMethod
    {
        public static string GetCallerMethodName([CallerMemberName] string method = "")
        {
            return method;
        }
    }
}
