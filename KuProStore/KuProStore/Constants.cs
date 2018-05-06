using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace KuProStore
{
    public static class Constants
    {
        public const string NameCookie = "auth";
        public const string BasePath = @"D:\VisualStudioProjects\KuPro\KuProStore\KuProStore";
        public const int ProductsOnPage = 50;
        public static Dictionary<int, string> Options = new Dictionary<int, string>();       
        static Constants()
        {
            Options.Add(1, "Самые новые");
            Options.Add(2, "Самые дешевые");
        }
    }
}