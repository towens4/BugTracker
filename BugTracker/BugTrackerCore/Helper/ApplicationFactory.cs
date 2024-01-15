
using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Helper
{
    public class ApplicationFactory
    {
        public static IApplication CreateApplication(string userId, string applicationName)
        {
            return new ApplicationViewModel()
            {
                UserId = userId,
                ApplicationName = applicationName,
                ApplicationId = new Guid(userId),
            };
        }

        public static List<IApplication> CreateApplicationList(List<string> applicationNames, string userId)
        {
            /*List<Application> applicationList = new List<Application>();

            applicationNames.ForEach(name => applicationList.Add(CreateApplication(userId, name)));*/


            

            return applicationNames.Select(name => CreateApplication(userId, name)).ToList(); ;
        }
    }
}
