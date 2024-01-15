using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Helper
{
    public class ErrorFactory
    {
        public static List<Error> CreateTempErrorList(IApplication dbApp, Guid newApplicationId, List<IErrorPostModel> errorPostModels)
        {
            

            return errorPostModels.Select(postModel => CreateTempError(postModel, dbApp, newApplicationId)).ToList();
        }

        public static Error CreateTempError(IErrorPostModel postModel, IApplication dbApp, Guid newApplicationId)
        {
            return new Error()
            {
                /**
                 * Assigns the error to an app depending if the app name and app id match that of the database application
                 * **/
                ApplicationId = (postModel.ApplicationName == dbApp.ApplicationName && newApplicationId == dbApp.ApplicationId)
                        ? newApplicationId
                        : postModel.ErrorModel.ApplicationId,
                ErrorId = postModel.ErrorModel.ErrorId,
                ErrorDetails = postModel.ErrorModel.ErrorDetails,
                Exception = postModel.ErrorModel.Exception,
                FileLine = postModel.ErrorModel.FileLine,
                MethodName = postModel.ErrorModel.MethodName,
                FileLocation = postModel.ErrorModel.FileLocation,
                Resolved = postModel.ErrorModel.Resolved
            };
        }

        public static IError CreateError(Exception error)
        {
            return new Error()
            {
                //ErrorId = Guid.NewGuid(),
                Exception = error.GetType().ToString(),
                ErrorDetails = error.Message,
                Resolved = false,
                FileLocation = GetFileName(error),
                FileLine = GetFileLineNumber(error),
                MethodName = GetMethodName(error),
            };
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
            var methodName = GetCallerMethodName();
            return methodName;
        }

        public static string GetCallerMethodName([CallerMemberName] string method = "")
        {
            return method;
        }


    }
}
