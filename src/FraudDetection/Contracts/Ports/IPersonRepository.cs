using FraudDetection.Misc;
using FraudDetection.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetection.Contracts.Ports
{
    public interface IPersonRepository
    {
        Task<Result<IReadOnlyCollection<Person>>> GetAllAsync();

        Task<Result> CreateAsync(Person person);
    }
}
