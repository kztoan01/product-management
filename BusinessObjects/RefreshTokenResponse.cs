using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class RefreshTokenResponse
    {
         public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}