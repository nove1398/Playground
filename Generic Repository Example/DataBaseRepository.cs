using System;
using System.Collections.Generic;
using System.Text;

namespace Generic_Repository_Example
{
    public static class DataBaseRepository
    {
        public static List<User> Data = new List<User>()
        {
           new User{ Age = 10, Name = "Test12"},
           new User{ Age = 10, Name = "Test13"},
           new User{ Age = 10, Name = "Test12"},
           new User{ Age = 10, Name = "Test42"},
           new User{ Age = 10, Name = "Test152"},
           new User{ Age = 10, Name = "Test162"},
        };
    }
}