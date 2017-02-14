using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.MyHelpers
{
    public class FilesHelper
    {
    
    public static string RelativePath
    {
        get
        {
           
                return System.Configuration.ConfigurationManager.AppSettings["relativePath"];
         }
      }
    
    
    
    }

}