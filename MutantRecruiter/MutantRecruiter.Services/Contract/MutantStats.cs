using System;
using System.Collections.Generic;
using System.Text;

namespace MutantRecruiter.Services.Contract
{
    public class MutantStats
    {
        public MutantStats()
        {
            Mutants = new List<Human>();
        }
        public int CountHumans { get; set; }
        public int CountMutantsDetected { get; set; }
        public decimal DetectedMutantRatio { get { return Convert.ToDecimal(CountMutantsDetected) / Convert.ToDecimal(CountHumans); } }
        public List<Human> Mutants { get; set; }
    }
}
