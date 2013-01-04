﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simplickr.Authentication
{
    public class OAuthAccessToken
    {
        public string FullName { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthTokenSecret { get; set; }
        public string UserNsId { get; set; }
        public string UserName { get; set; }
    }
}
