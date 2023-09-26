using BugTrackerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Helper
{
    public class ErrorFactory
    {
        public static List<Error> CreateTempErrorList(Application dbApp, Guid newApplicationId, List<ErrorPostModel> errorPostModels)
        {
            

            return errorPostModels.Select(postModel => CreateTempError(postModel, dbApp, newApplicationId)).ToList();
        }

        public static Error CreateTempError(ErrorPostModel postModel, Application dbApp, Guid newApplicationId)
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

        
    }
}
