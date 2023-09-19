using BugTrackerUICore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerUICore.Helper
{
    public class ErrorFactory
    {
        public static ErrorViewModel CreateErrorViewModel(BugTrackerAPICall.Models.ErrorViewModel error)
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
        public static List<ErrorViewModel> CreateErrorList(List<BugTrackerAPICall.Models.ErrorViewModel> errorViewModels)
        {
           return errorViewModels.Select(error => CreateErrorViewModel(error)).ToList();
        }
    }
}
