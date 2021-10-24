using Dapper.Contrib.Extensions;
using FraudDetection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDetection.Adapter.Database.Entities
{
    [Table("Persons")]
    internal class PersonEntity
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdentificationNumber { get; set; }

        public static PersonEntity FromModel(Person person)
        {
            return new PersonEntity
            {
                ID = person.ID,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                IdentificationNumber = person.IdentificationNumber
            };
        }

        public Person ToModel()
        {
            return new Person
            {
                ID = this.ID,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                IdentificationNumber = this.IdentificationNumber
            };
        }
    }
}
