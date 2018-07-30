using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FhirReferenceValidator.Engine
{
    public class FhirProfileReferenceValidator
    {
        private Logger _logger;

        public void ValidateReferences(string rootDirectory, Action<string> logWriter)
        {
            _logger = new Logger(logWriter);

            if (string.IsNullOrWhiteSpace(rootDirectory))
            {
                _logger.LogError("Please pass the directory containing profiles to validate.");
                return;
            }

            if (!Directory.Exists(rootDirectory))
            {
                _logger.LogError("Directory does not exist.");
                return;
            }

            Dictionary<string, Base> profiles = LoadProfiles(rootDirectory, "*.xml", SearchOption.AllDirectories);

            if ((profiles == null) || (profiles.Count == 0))
            {
                _logger.LogError("No profiles found");
                return;
            }

            FhirReferenceValidator validator = new FhirReferenceValidator(profiles, _logger);
            validator.Validate();

            _logger.WriteCollectedErrors();
        }

        private Dictionary<string, Base> LoadProfiles(string rootPath, string searchFilter, SearchOption searchOption)
        {
            _logger.Log("Searching " + Path.GetFullPath(rootPath));
            _logger.Log("");

            Dictionary<string, Base> result = new Dictionary<string, Base>();

            IEnumerable<string> files = Directory.GetFiles(rootPath, searchFilter, searchOption);

            foreach (string file in files)
            {
                _logger.Log("Loading " + file);
                string fileContents = FileHelper.ReadInputFile(file);

                string rootNodeName = XmlHelper.GetRootNodeName(fileContents);

                Type type = getTypeFromRootNode(rootNodeName);

                FhirXmlParser parser = new FhirXmlParser();

                result.Add(file, parser.Parse(fileContents, type));
            }

            _logger.Log("");

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
