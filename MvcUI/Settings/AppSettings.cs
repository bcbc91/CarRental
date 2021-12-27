using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MvcUI.Settings
{
    public class AppSettings
    {
        public static string Title { get; set; }
        public static string AcceptedImageExtensions { get; set; }
        public static double AcceptedImageMaximumLength { get; set; }
    }
}
