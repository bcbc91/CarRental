using System;
using System.Collections.Generic;
using System.Text;
using Core.Utils.Bases;
using Microsoft.Extensions.Configuration;

namespace Core.Utils
{
    public class AppSettingsUtil:AppSettingsUtilBase
    {
        public AppSettingsUtil(IConfiguration configuration):base(configuration)
        {
            
        }
    }
}
