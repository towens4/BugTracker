using BugTrackerCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Helper
{
    public class ConvertToErrorObject
    {
        public static Error Convert(Exception exception)
        {
            Error error = new Error()
            {
                ErrorId = new Guid(),
                Exception = exception.GetType().ToString(),
                ErrorDetails = exception.Message,
                Resolved = false,
                FileLocation = GetFileName(exception),
                FileLine = GetFileLineNumber(exception),
                MethodName = GetMethodName(exception),

            };

            return error;
        }

        private static string GetFileLineNumber(Exception ex)
        {
            var stackTrace = new StackTrace(ex, true);
            var frame = stackTrace.GetFrame(0);
            return frame.GetFileLineNumber().ToString();
        }

        private static string GetFileName(Exception ex)
        {
            var stackTrace = new StackTrace(ex, true);
            var frame = stackTrace.GetFrame(stackTrace.FrameCount - 1);
            return frame.GetFileName();
        }

        private static string GetMethodName(Exception ex)
        {

            /** Fix Get method Name
             * 
             * 
             * */
            //var stackTrace = new StackTrace(ex, true);
            //var methodName = ex.TargetSite;
            //var methName = methodName == null? null: ex.TargetSite.Name ;
            //var frame = stackTrace.GetFrame(0);
            var methodName = GetCallerMethodName();
            return methodName;
        }

        public static string GetCallerMethodName([CallerMemberName] string method = "")
        {
            return method;
        }
    }
}
