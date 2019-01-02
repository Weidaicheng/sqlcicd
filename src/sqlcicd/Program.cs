using System;
using sqlcicd.Configuration;

namespace sqlcicd
{
    class Program
    {
        static void Main(string[] args)
        {
            // args[0] is repository path
            Singletons.Path = args[0];
        }
    }
}
