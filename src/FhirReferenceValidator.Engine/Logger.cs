using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FhirReferenceValidator.Engine
{
    internal class Logger
    {
        private Action<string> _logWriter;
        private List<string> _errors = new List<string>(); 

        public Logger(Action<string> logWriter)
        {
            _logWriter = logWriter;
        }

        public void Log(string message)
        {
            _logWriter(message);
        }

        public void LogError(string message)
        {
            _errors.Add(message);
            _logWriter("ERROR > " + message);
        }

        public void WriteCollectedErrors()
        {
            if (_errors.Count == 0)
                return;

            _logWriter("");
            _logWriter("--------------------------------------------------");
            _logWriter("ERRORS OCCURRED:");

            foreach (string error in _errors)
                _logWriter(" " + error);

            _logWriter("--------------------------------------------------");
        }
    }
}
