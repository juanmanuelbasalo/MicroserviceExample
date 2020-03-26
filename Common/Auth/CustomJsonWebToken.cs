using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Auth
{
    public class CustomJsonWebToken
    {
        public string Token { get; set; }
        public long Expire { get; set; }
    }
}
