using System;
using System.Collections.Generic;
using System.Text;

namespace Generic_Repository_Example
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}