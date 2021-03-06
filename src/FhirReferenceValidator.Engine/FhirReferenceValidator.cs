﻿using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hl7.Fhir.Model.ElementDefinition;

namespace FhirReferenceValidator.Engine
{
    internal class FhirReferenceValidator
    {
        private Logger _logger;

        private static string Hl7BaseStructureDefinitionPrefix = "http://hl7.org/fhir/StructureDefinition/";
        private static string Hl7BaseValueSetPrefix = "http://hl7.org/fhir/ValueSet/";
        private static string BcpValueSetUrl = "http://www.rfc-editor.org/bcp/bcp13.txt";
        private static string Hl7BaseCodeSystemPrefix = "http://hl7.org/fhir/";
        private static string SnomedCodeSystemUrl = "http://snomed.info/sct";

        private Dictionary<string, Base> _profiles;

        private StructureDefinition[] StructureDefinitions
        {
            get
            {
                return _profiles
                    .Values
                    .Where(t => t is StructureDefinition)
                    .Select(t => (StructureDefinition)t)
                    .ToArray();
            }
        }

        private ValueSet[] ValueSets
        {
            get
            {
                return _profiles
                    .Values
                    .Where(t => t is ValueSet)
                    .Select(t => (ValueSet)t)
                    .ToArray();
            }
        }

        private CodeSystem[] CodeSystems
        {
            get
            {
                return _profiles
                    .Values
                    .Where(t => t is CodeSystem)
                    .Select(t => (CodeSystem)t)
                    .ToArray();
            }
        }

        public FhirReferenceValidator(Dictionary<string, Base> profiles, Logger logger)
        {
            _profiles = profiles;
            _logger = logger;
        }

        public void Validate()
        {
            _logger.Log("Starting validation");
            _logger.Log("");

            foreach (string profileFilename in _profiles.Keys)
            {
                Base profile = _profiles[profileFilename];

                if (profile is StructureDefinition)
                    Validate(profileFilename, (StructureDefinition)profile);
                else if (profile is ValueSet)
                    Validate(profileFilename, (ValueSet)profile);
                else
                    continue;

                _logger.Log("");
            }
        }

        private void Validate(string filename, ConceptMap profile)
        {
            _logger.Log("Validating ConceptMap" + filename);
            _logger.LogError("ConceptMap validation not currently supported.");
        }

        private void Validate(string filename, CodeSystem profile)
        {
            _logger.Log("Validating CodeSystem" + filename);
            _logger.LogError("CodeSystem validation not currently supported.");
        }

        private void Validate(string filename, ValueSet valueSet)
        {
            _logger.Log("Validating ValueSet " + filename);

            if (valueSet.Compose == null)
                return;

            if (valueSet.Compose.Include == null)
                return;

            foreach (ValueSet.ConceptSetComponent compose in valueSet.Compose.Include)
            {
                _logger.Log(" Checking CodeSystem > " + compose.System);

                if (!IsCodeSystemReferenceValid(compose.System))
                    _logger.LogError("Could not find codesystem " + compose.System);
            }
        }

        private void Validate(string filename, StructureDefinition structureDefinition)
        {
            _logger.Log("Validating StructureDefinition " + filename);

            if ((structureDefinition.Snapshot == null) || (structureDefinition.Snapshot.Element == null) || (structureDefinition.Snapshot.Element.Count == 0))
                throw new Exception(filename + " has no snapshot component");

            _logger.Log(" Checking Base URL > " + structureDefinition.BaseDefinition);

            if (!IsResourceReferenceValid(structureDefinition.BaseDefinition))
                _logger.LogError("Could not find reference " + structureDefinition.BaseDefinition);

            foreach (ElementDefinition element in structureDefinition.Snapshot.Element)
            {
                // check type element
                foreach (TypeRefComponent typeRef in element.Type)
                {
                    if ((typeRef.Code == "Reference") || (typeRef.Code == "Extension"))
                    {
                        if (!string.IsNullOrWhiteSpace(typeRef.Profile))
                        {
                            _logger.Log(" Checking " + typeRef.Code + " > " + typeRef.Profile);

                            if (!IsResourceReferenceValid(typeRef.Profile))
                                _logger.LogError("Could not find reference " + typeRef.Profile);
                        }

                        if (!string.IsNullOrWhiteSpace(typeRef.TargetProfile))
                        {
                            _logger.Log(" Checking " + typeRef.Code + " > " + typeRef.TargetProfile);

                            if (!IsResourceReferenceValid(typeRef.TargetProfile))
                                _logger.LogError("Could not find reference " + typeRef.TargetProfile);
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
                            _logger.Log(" Checking Valueset > " + reference.Reference);

                            if (!IsValueSetReferenceValue(reference.Reference))
                                _logger.LogError("Could not find valueset " + reference.Reference);
                        }
                    }
                    else if (element.Binding.ValueSet is FhirUri)
                    {
                        FhirUri reference = ((FhirUri)element.Binding.ValueSet);

                        if (!string.IsNullOrWhiteSpace(reference.Value))
                        {
                            _logger.Log(" Checking Valueset > " + reference.Value);

                            if (!IsValueSetReferenceValue(reference.Value))
                                _logger.LogError("Could not find valueset " + reference.Value);
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
