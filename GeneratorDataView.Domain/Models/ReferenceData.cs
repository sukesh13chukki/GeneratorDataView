﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeneratorDataView.Domain.Models
{
    [XmlRoot("ReferenceData")]
    public class ReferenceData
    {
        [XmlElement("Factors")]
        public Factors Factors { get; set; }
    }

    public class Factors
    {
        [XmlElement("ValueFactor")]
        public ValueFactor ValueFactor { get; set; }

        [XmlElement("EmissionsFactor")]
        public EmissionsFactor EmissionsFactor { get; set; }
    }

    public class ValueFactor
    {
        [XmlElement("High")]
        public double High { get; set; }

        [XmlElement("Medium")]
        public double Medium { get; set; }

        [XmlElement("Low")]
        public double Low { get; set; }
    }

    public class EmissionsFactor
    {
        [XmlElement("High")]
        public double High { get; set; }

        [XmlElement("Medium")]
        public double Medium { get; set; }
    }
}
