using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Helper
{
    public class StringExtractor
    {

        public static string ExtractFilePath(string path, string key)
        {


            int lastIndexOf = path.LastIndexOf('\\');

            if (lastIndexOf == -1)
            {
                return path;
            }

            string fileName = path.Substring(lastIndexOf + 1);

            if (fileName.EndsWith(".cs"))
            {
                return fileName;
            }

            return path;

            /*if (lastIndexOf != -1)
            {
                // Extract the file name with the .cs extension
                string fileName = input.Substring(lastIndexOf + 1);

                // Check if the file has a .cs extension
                if (fileName.EndsWith(".cs"))
                {
                    return fileName;
                }
                else
                {
                    return path;
                }
            }
            else
            {
                return path;
            }*/
        }
    }
}
