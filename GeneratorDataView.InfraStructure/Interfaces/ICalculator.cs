using GeneratorDataView.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorDataView.InfraStructure.Interfaces
{
    public interface IGenerator
    {
        string Name { get; }
        Generation Generation { get; }
        double CalculateTotal(ReferenceData referenceData);
        double? CalculateDailyEmission(DateTime date, ReferenceData referenceData) => null;
    }
}
