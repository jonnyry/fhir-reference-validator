using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FhirReferenceValidator
{
    internal class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void LogError(string message)
        {
            Console.WriteLine("ERROR > " + message);
            Console.WriteLine("");
        }

        public static void Log(Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
