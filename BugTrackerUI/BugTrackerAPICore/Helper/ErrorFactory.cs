using BugTrackerAPICall.Models;
using BugTrackerUICore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorViewModel = BugTrackerUICore.Models.ViewModels.ErrorViewModel;

namespace BugTrackerUICore.Helper
{
    public class ErrorFactory
    {
        public static ErrorViewModel CreateErrorViewModel(Error error)
        {
            return new ErrorViewModel()
            {
                ApplicationId = error.ApplicationId,
                ErrorDetails = error.ErrorDetails,
                ErrorId = error.ErrorId,
                Exception = error.Exception,
                FileLine = error.FileLine,
                FileLocation = error.FileLocation,
                MethodName = error.MethodName,
                Resolved = error.Resolved
            };
        }
        public static List<ErrorViewModel> CreateErrorList(List<Error> errorViewModels)
        {
           return errorViewModels.Select(error => CreateErrorViewModel(error)).ToList();
        }
    }
}
