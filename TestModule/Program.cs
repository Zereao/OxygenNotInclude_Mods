using System;
using System.Diagnostics;
using SuperHatch;

namespace TestModule
{
    internal class Program
    {
        private static readonly Logger Log = new Logger("test");

        public static void Main(string[] args)
        {
            ConfigParser.GetConfigMapping();
        }
    }
}