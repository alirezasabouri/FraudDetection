using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetection.Api.Dto
{
    public class PersonSimilarityInputDto
    {
        public PersonInputDto SourcePerson { get; set; }
        public PersonInputDto TargetPerson { get; set; }
    }
}
