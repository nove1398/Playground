using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOne
{
    public interface IJwtRefreshTokenGenerator
    {
        string Generate();
    }
}