using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class RefreshTokenRequest
    {
        public string UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}