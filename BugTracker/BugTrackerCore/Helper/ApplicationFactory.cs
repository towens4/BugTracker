using BugTrackerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Helper
{
    public class ApplicationFactory
    {
        public static Application CreateApplication(string userId, string applicationName)
        {
            return new Application()
            {
                UserId = userId,
                ApplicationName = applicationName,
                ApplicationId = new Guid(userId),
            };
        }

        public static List<Application> CreateApplicationList(List<string> applicationNames, string userId)
        {
            /*List<Application> applicationList = new List<Application>();

            applicationNames.ForEach(name => applicationList.Add(CreateApplication(userId, name)));*/


            

            return applicationNames.Select(name => CreateApplication(userId, name)).ToList(); ;
        }
    }
}
