using BugTrackerAPICall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Helper
{
    public class IdHolderFactory
    {
        public static IdHolderModel CreateIdHolder(string userId, Guid applicationId)
        {
            return new IdHolderModel()
            {
                UserId = userId,
                ApplicationId = applicationId
            };
        }
    }
}
