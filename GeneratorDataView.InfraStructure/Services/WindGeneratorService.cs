using GeneratorDataView.Domain.Models;
using GeneratorDataView.InfraStructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorDataView.InfraStructure.Services
{
    public class WindGeneratorService : IGenerator
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public Generation Generation { get; set; }

        public double CalculateTotal(ReferenceData referenceData)
        {
            double factor = Location == "Offshore" ? referenceData.Factors.ValueFactor.Low : referenceData.Factors.ValueFactor.High;
            return Generation.Days.Sum(day => day.Energy * day.Price * factor);
        }

    }
}
