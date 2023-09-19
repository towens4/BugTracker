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
            /*var tempErrors = new List<Error>();

            errorPostModels.ForEach(postModel => tempErrors.Add(CreateTempError(postModel, dbApp, newApplicationId)));

            foreach (var item in errorPostModels)
            {
                var tempError = new Error()
                {
                    ApplicationId = (item.ApplicationName == dbApp.ApplicationName && newApplicationId == dbApp.ApplicationId)
                        ? newApplicationId
                        : item.ErrorModel.ApplicationId,
                    ErrorId = item.ErrorModel.ErrorId,
                    ErrorDetails = item.ErrorModel.ErrorDetails,
                    Exception = item.ErrorModel.Exception,
                    FileLine = item.ErrorModel.FileLine,
                    MethodName = item.ErrorModel.MethodName,
                    FileLocation = item.ErrorModel.FileLocation,
                    Resolved = item.ErrorModel.Resolved
                };
                tempErrors.Add(tempError);
            }*/

            return errorPostModels.Select(postModel => CreateTempError(postModel, dbApp, newApplicationId)).ToList();
        }

        public static Error CreateTempError(ErrorPostModel postModel, Application dbApp, Guid newApplicationId)
        {
            return new Error()
            {
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
