using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeneratorDataView.Domain.Models
{
    [XmlRoot("GenerationOutput")]
    public class GenerationOutput
    {
        [XmlArray("Totals")]
        [XmlArrayItem("Generator")]
        public List<GeneratorTotal> Totals { get; set; }

        [XmlArray("MaxEmissionGenerators")]
        [XmlArrayItem("Day")]
        public List<MaxEmission> MaxEmissionGenerators { get; set; }

        [XmlArray("ActualHeatRates")]
        [XmlArrayItem("ActualHeatRate")]
        public List<HeatRate> ActualHeatRates { get; set; }
    }
    public class GeneratorTotal
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Total")]
        public double Total { get; set; }
    }

    public class MaxEmission
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Date")]
        public DateTime Date { get; set; }

        [XmlElement("Emission")]
        public double Emission { get; set; }
    }

    public class HeatRate
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("HeatRate")]
        public double HeatRateNo { get; set; }
    }

}
