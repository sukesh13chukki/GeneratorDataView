using GeneratorDataView.Client.Helpers;
using GeneratorDataView.Domain.Models;
using GeneratorDataView.InfraStructure.Interfaces;
using GeneratorDataView.InfraStructure.Services;
using System.Xml.Serialization;

namespace GeneratorDataView.Client.FileUtility
{
    // Observer class
    public class FileProcessor
    {
        public void ProcessFile(string filePath)
        {
            string outputFilePath = filePath.Replace("01-Basic.xml", "Output\\_output.xml");
           
         
            string referenceDataFilePath = filePath.Replace("01-Basic.xml", "Reference\\ReferenceData.xml");

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GenerationReport));
                GenerationReport generationReport = new GenerationReport();
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var deserializedObject = serializer.Deserialize(fileStream);
                    if (deserializedObject != null)
                    {
                        generationReport = (GenerationReport)deserializedObject;
                    }
                    else
                    {
                        Console.WriteLine("Deserialization failed.");
                    }

                }
                var referenceDataSerializer = new XmlSerializer(typeof(ReferenceData));
                ReferenceData referenceData = null;
                using (FileStream fileStream = new FileStream(referenceDataFilePath, FileMode.Open))
                {
                    var deserializedObject = serializer.Deserialize(fileStream);
                    if (deserializedObject != null)
                    {
                        referenceData = (ReferenceData)deserializedObject;
                    }
                    else
                    {
                        Console.WriteLine("ReferenceData Deserialization failed.");
                    }
                }

                // Create generators list
                var generators = new List<IGenerator>();
                if(generationReport != null) { 
                generators.AddRange(generationReport.Wind.WindGenerators.Select(w => new WindGeneratorService { Name = w.Name, Location = w.Location, Generation = w.Generation }));
                generators.AddRange(generationReport.Gas.GasGenerators.Select(g => new GasGeneratorService { Name = g.Name, EmissionsRating = g.EmissionsRating, Generation = g.Generation }));
                generators.AddRange(generationReport.Coal.CoalGenerators.Select(c => new CoalGeneratorService { Name = c.Name, EmissionsRating = c.EmissionsRating, TotalHeatInput = c.TotalHeatInput, ActualNetGeneration = c.ActualNetGeneration, Generation = c.Generation }));
            }
                // Process data

                if (referenceData != null)
                {
                    var totals = CalculateHelper.CalculateTotalGeneration(generators, referenceData);
                    var maxEmissions = CalculateHelper.CalculateMaxDailyEmissions(generators, referenceData);
                    var heatRates = CalculateHelper.CalculateActualHeatRates(generators);

                    // Generate output object
                    var output = new GenerationOutput
                    {
                        Totals = totals,
                        MaxEmissionGenerators = maxEmissions,
                        ActualHeatRates = heatRates
                    };

                    // Serialize output object to XML
                    using (var fileStream = new FileStream(outputFilePath, FileMode.Create))
                    {
                        using (var writer = new StreamWriter(fileStream))
                        {
                            var outputSerializer = new XmlSerializer(typeof(GenerationOutput));
                            outputSerializer.Serialize(writer, output);
                        }
                    }


                    Console.WriteLine("Processing complete. Output saved to " + outputFilePath);
                }
                else
                {
                    Console.WriteLine("Error processing file. Reference data not found.");
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }
    }
}
