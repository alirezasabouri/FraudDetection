using FraudDetection.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetection.Api.Dto
{
    public class PersonCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string IdentificationNumber { get; set; }

        public Person ToModel()
        {
            return new Person
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                IdentificationNumber = this.IdentificationNumber
            };
        }
    }
}
