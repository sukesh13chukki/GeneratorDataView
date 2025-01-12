using GeneratorDataView.Domain.Models;
using GeneratorDataView.InfraStructure.Interfaces;
using GeneratorDataView.InfraStructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorDataView.Client.Helpers
{
    public static class CalculateHelper
    {
        public static List<GeneratorTotal> CalculateTotalGeneration(IEnumerable<IGenerator> generators, ReferenceData referenceData)
        {
            return generators.Select(generator => new GeneratorTotal
            {
                Name = generator.Name,
                Total = generator.CalculateTotal(referenceData)
            }).ToList();
        }

        public static List<MaxEmission> CalculateMaxDailyEmissions(IEnumerable<IGenerator> generators, ReferenceData referenceData)
        {
            var emissions = new List<MaxEmission>();

            var allDates = generators
                .SelectMany(g => g.Generation?.Days.Select(d => d.Date) ?? Enumerable.Empty<DateTime>())
                .Distinct();

            foreach (var date in allDates)
            {
                var maxEmission = generators
                     .Where(g => g.CalculateDailyEmission(date, referenceData).HasValue)
                    .Select(g => new
                    {
                        g.Name,
                        Emission = g.CalculateDailyEmission(date, referenceData)
                    })
                    .Where(e => e.Emission.HasValue)
                    .OrderByDescending(e => e.Emission)
                    .FirstOrDefault();

                if (maxEmission != null)
                {
                    emissions.Add(new MaxEmission
                    {
                        Name = maxEmission.Name,
                        Date = date,
                        Emission = maxEmission.Emission.Value
                    });
                }
            }

            return emissions;
        }

        public static List<HeatRate> CalculateActualHeatRates(IEnumerable<IGenerator> generators)
        {
            return generators.OfType<CoalGeneratorService>().Select(generator => new HeatRate
            {
                Name = generator.Name,
                HeatRateNo = generator.CalculateHeatRate()
            }).ToList();
        }
    }
}
