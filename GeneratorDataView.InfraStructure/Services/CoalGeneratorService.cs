using GeneratorDataView.Domain.Models;
using GeneratorDataView.InfraStructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorDataView.InfraStructure.Services
{
    public class CoalGeneratorService : IGenerator
    {
        public string Name { get; set; }
        public double EmissionsRating { get; set; }
        public double TotalHeatInput { get; set; }
        public double ActualNetGeneration { get; set; }
        public Generation Generation { get; set; }

        public double CalculateTotal(ReferenceData referenceData)
        {
            double factor = referenceData.Factors.ValueFactor.Medium;
            return Generation.Days.Sum(day => day.Energy * day.Price * factor);
        }

        public double? CalculateDailyEmission(DateTime date, ReferenceData referenceData)
        {
            double factor = referenceData.Factors.EmissionsFactor.High;
            var day = Generation.Days.FirstOrDefault(d => d.Date == date);
            return day != null ? day.Energy * EmissionsRating * factor : (double?)null;
        }

        public double CalculateHeatRate()
        {
            return ActualNetGeneration == 0 ? 0 : TotalHeatInput / ActualNetGeneration;
        }
    }
}
