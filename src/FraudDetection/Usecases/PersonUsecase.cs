using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Usecases;
using FraudDetection.Misc;
using FraudDetection.Models;
using System;
using System.Threading.Tasks;

namespace FraudDetection.Usecases
{
    public class PersonUsecase : IPersonUsecase
    {
        private readonly IPersonRepository _personRepository;

        public PersonUsecase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<Result> CreateAsync(Person person)
        {
            return _personRepository.CreateAsync(person);
        }
    }
}
