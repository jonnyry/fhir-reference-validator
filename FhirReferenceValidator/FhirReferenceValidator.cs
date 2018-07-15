using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hl7.Fhir.Model.ElementDefinition;

namespace FhirReferenceValidator
{
    internal class FhirReferenceValidator
    {
        private static string Hl7BaseStructureDefinitionPrefix = "http://hl7.org/fhir/StructureDefinition/";
        private static string Hl7BaseValueSetPrefix = "http://hl7.org/fhir/ValueSet/";
        private static string BcpValueSetUrl = "http://www.rfc-editor.org/bcp/bcp13.txt";
        private static string Hl7BaseCodeSystemPrefix = "http://hl7.org/fhir/";
        private static string SnomedCodeSystemUrl = "http://snomed.info/sct";

        private Dictionary<string, Base> profiles;

        private StructureDefinition[] StructureDefinitions { get { return profiles.Values.Where(t => t is StructureDefinition).Select(t => (StructureDefinition)t).ToArray(); } }
        private ValueSet[] ValueSets { get { return profiles.Values.Where(t => t is ValueSet).Select(t => (ValueSet)t).ToArray(); } }
        private CodeSystem[] CodeSystems { get { return profiles.Values.Where(t => t is CodeSystem).Select(t => (CodeSystem)t).ToArray(); } }

        public FhirReferenceValidator(Dictionary<string, Base> profiles)
        {
            this.profiles = profiles;
        }

        public void Validate()
        {
            Logger.Log("Starting validation");

            foreach (string profileFilename in profiles.Keys)
            {
                Base profile = profiles[profileFilename];

                if (profile is ConceptMap)
                {
                }
                else if (profile is CodeSystem)
                {
                }
                else if (profile is StructureDefinition)
                    Validate(profileFilename, (StructureDefinition)profile);
                else if (profile is ValueSet)
                    Validate(profileFilename, (ValueSet)profile);
            }
        }

        private void Validate(string filename, ValueSet valueSet)
        {
            Logger.Log("Validating ValueSet " + filename);

            if (valueSet.Compose == null)
                return;

            if (valueSet.Compose.Include == null)
                return;

            foreach (ValueSet.ConceptSetComponent compose in valueSet.Compose.Include)
            {
                Logger.Log(" Checking CodeSystem > " + compose.System);

                if (!IsCodeSystemReferenceValid(compose.System))
                    Logger.LogError("Could not find codesystem " + compose.System);
            }
        }

        private void Validate(string filename, StructureDefinition structureDefinition)
        {
            Logger.Log("Validating StructureDefinition " + filename);

            if ((structureDefinition.Snapshot == null) || (structureDefinition.Snapshot.Element == null) || (structureDefinition.Snapshot.Element.Count == 0))
                throw new Exception(filename + " has no snapshot component");

            Logger.Log(" Checking Base URL > " + structureDefinition.BaseDefinition);

            if (!IsResourceReferenceValid(structureDefinition.BaseDefinition))
                Logger.LogError("Could not find reference " + structureDefinition.BaseDefinition);

            foreach (ElementDefinition element in structureDefinition.Snapshot.Element)
            {
                // check type element
                foreach (TypeRefComponent typeRef in element.Type)
                {
                    if ((typeRef.Code == "Reference") || (typeRef.Code == "Extension"))
                    {
                        if (!string.IsNullOrWhiteSpace(typeRef.Profile))
                        {
                            Logger.Log(" Checking " + typeRef.Code + " > " + typeRef.Profile);

                            if (!IsResourceReferenceValid(typeRef.Profile))
                                Logger.LogError("Could not find reference " + typeRef.Profile);
                        }

                        if (!string.IsNullOrWhiteSpace(typeRef.TargetProfile))
                        {
                            Logger.Log(" Checking " + typeRef.Code + " > " + typeRef.TargetProfile);

                            if (!IsResourceReferenceValid(typeRef.TargetProfile))
                                Logger.LogError("Could not find reference " + typeRef.TargetProfile);
                        }
                    }
                }

                if (element.Binding != null)
                {
                    if (element.Binding.ValueSet is ResourceReference)
                    {
                        ResourceReference reference = ((ResourceReference)element.Binding.ValueSet);

                        if (!string.IsNullOrWhiteSpace(reference.Reference))
                        {
                            Logger.Log(" Checking Valueset > " + reference.Reference);

                            if (!IsValueSetReferenceValue(reference.Reference))
                                Logger.LogError("Could not find valueset " + reference.Reference);
                        }
                    }
                    else if (element.Binding.ValueSet is FhirUri)
                    {
                        FhirUri reference = ((FhirUri)element.Binding.ValueSet);

                        if (!string.IsNullOrWhiteSpace(reference.Value))
                        {
                            Logger.Log(" Checking Valueset > " + reference.Value);

                            if (!IsValueSetReferenceValue(reference.Value))
                                Logger.LogError("Could not find valueset " + reference.Value);
                        }
                    }
                    else
                    {
                        throw new Exception("Valueset reference type " + element.Binding.ValueSet.GetType());
                    }
                }
            }
        }

        public bool IsResourceReferenceValid(string profileUrl)
        {
            if (this.StructureDefinitions.Select(t => t.Url).Any(t => t == profileUrl))
                return true;

            return (profileUrl.StartsWith(Hl7BaseStructureDefinitionPrefix));
        }

        public bool IsValueSetReferenceValue(string valueSetUrl)
        {
            if (this.ValueSets.Select(t => t.Url).Any(t => t == valueSetUrl))
                return true;

            if (valueSetUrl == BcpValueSetUrl)
                return true;

            return (valueSetUrl.StartsWith(Hl7BaseValueSetPrefix));
        }

        public bool IsCodeSystemReferenceValid(string codeSystemUrl)
        {
            if (this.CodeSystems.Select(t => t.Url).Any(t => t == codeSystemUrl))
                return true;

            if (codeSystemUrl == SnomedCodeSystemUrl)
                return true;

            return (codeSystemUrl.StartsWith(Hl7BaseCodeSystemPrefix));
        }
    }
}
