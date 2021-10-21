using FraudDetection.Contracts.Usecases;
using FraudDetection.Misc;
using FraudDetection.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetection.Usecases
{
    public class PersonUsecase : IPersonUsecase
    {
        public Task<Result> CreateAsync(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
