using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheITBlog.ViewModels
{
    public class MailSettings
    {
        //This is to configure and use smpt server
        //from google for example
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

    }
}
