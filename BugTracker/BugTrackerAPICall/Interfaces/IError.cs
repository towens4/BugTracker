using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Interfaces
{
    public interface IError
    {
        public Guid ErrorId { get; set; }
        public Guid ApplicationId { get; set; }
        public string Exception { get; set; }
        public string FileLocation { get; set; }
        public string MethodName { get; set; }
        public string FileLine { get; set; }
        public string ErrorDetails { get; set; }
        public bool Resolved { get; set; }
    }
}
