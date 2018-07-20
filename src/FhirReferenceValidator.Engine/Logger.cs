using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FhirReferenceValidator.Engine
{
    internal class Logger
    {
        private static List<string> _errors = new List<string>(); 

        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void LogError(string message)
        {
            _errors.Add(message);
            Console.WriteLine("ERROR > " + message);
            Console.WriteLine("");
        }

        public static void Log(Exception e)
        {
            Console.WriteLine(e);
        }

        public static void WriteCollectedErrors()
        {
            if (_errors.Count == 0)
                return;

            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("ERRORS OCCURRED:");

            foreach (string error in _errors)
                Console.WriteLine(" " + error);

            Console.WriteLine("--------------------------------------------------");
        }
    }
}
