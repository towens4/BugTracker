using BugTrackerAPICall.APICall;
using BugTrackerAPICall.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHttpMethods(this IServiceCollection services)
        {
            services.AddScoped<IHttpMethods, ApiCallFunctions>();
        }
    }
}
