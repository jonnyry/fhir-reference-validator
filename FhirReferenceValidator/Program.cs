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
                    Logger.LogError("Please pass the directory containing profiles to validate.");
                    return;
                }

                if (!Directory.Exists(args[0]))
                {
                    Logger.LogError("Directory does not exist.");
                    return;
                }

                Dictionary<string, Base> profiles = LoadProfiles(args[0], "*.xml", SearchOption.AllDirectories);

                if ((profiles == null) || (profiles.Count == 0))
                {
                    Logger.LogError("No profiles found");
                    return;
                }

                FhirReferenceValidator validator = new FhirReferenceValidator(profiles);
                validator.Validate();
            }
            catch (Exception e)
            {
                Logger.LogError("Exception occurred");
                Logger.Log(e);
            }
        }

        private static Dictionary<string, Base> LoadProfiles(string rootPath, string searchFilter, SearchOption searchOption)
        {
            Logger.Log("Searching " + Path.GetFullPath(rootPath));
            Logger.Log("");

            Dictionary<string, Base> result = new Dictionary<string, Base>();

            IEnumerable<string> files = Directory.GetFiles(rootPath, searchFilter, searchOption);

            foreach (string file in files)
            {
                Logger.Log("Loading " + file);
                string fileContents = FileHelper.ReadInputFile(file);

                string rootNodeName = XmlHelper.GetRootNodeName(fileContents);

                Type type = getTypeFromRootNode(rootNodeName);

                FhirXmlParser parser = new FhirXmlParser();

                result.Add(file, parser.Parse(fileContents, type));
            }

            Logger.Log("");

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
