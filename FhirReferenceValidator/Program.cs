using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FhirReferenceValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if ((args == null) || (args.Length != 1) || string.IsNullOrWhiteSpace(args[0]))
                {
                    Log("Please pass the directory containing profiles to validate.");
                    return;
                }

                if (!Directory.Exists(args[0]))
                {
                    Log("Directory does not exist.");
                    return;
                }

                Dictionary<string, Base> fileAndFileContents = LoadProfiles(args[0], "*.xml", SearchOption.AllDirectories);

                if ((fileAndFileContents == null) || (fileAndFileContents.Count == 0))
                {
                    Log("No profiles found");
                    return;
                }



            }
            catch (Exception e)
            {
                Log("Exception occurred");
                Log(e);
            }
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }

        private static void Log(Exception e)
        {
            Console.WriteLine(e);
        }

        private static Dictionary<string, Base> LoadProfiles(string rootPath, string searchFilter, SearchOption searchOption)
        {
            Log("Searching " + Path.GetFullPath(rootPath));
            Log("");

            Dictionary<string, Base> result = new Dictionary<string, Base>();

            IEnumerable<string> files = Directory.GetFiles(rootPath, searchFilter, searchOption);

            foreach (string file in files)
            {
                Log("Loading " + file);
                string fileContents = FileHelper.ReadInputFile(file);

                string rootNodeName = XmlHelper.GetRootNodeName(fileContents);

                Type type = getTypeFromRootNode(rootNodeName);

                FhirXmlParser parser = new FhirXmlParser();

                result.Add(file, parser.Parse(fileContents, type));
            }

            return result;
        }

        private static Type getTypeFromRootNode(string rootNode)
        {
            switch (rootNode)
            {
                case "StructureDefinition": return typeof(StructureDefinition);
                case "ValueSet": return typeof(ValueSet);
                case "OperationDefinition": return typeof(OperationDefinition);
                case "CodeSystem": return typeof(CodeSystem);
                case "ConceptMap": return typeof(ConceptMap);
                default: throw new InvalidDataException("Root node name " + rootNode + " not recognised as profile.");
            }
        }
    }
}
