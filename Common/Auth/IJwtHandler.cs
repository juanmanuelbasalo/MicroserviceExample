using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Auth
{
    public interface IJwtHandler
    {
        CustomJsonWebToken Create(Guid userId);
    }
}
