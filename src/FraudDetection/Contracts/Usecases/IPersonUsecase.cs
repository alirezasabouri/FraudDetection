using FraudDetection.Misc;
using FraudDetection.Models;
using System.Threading.Tasks;

namespace FraudDetection.Contracts.Usecases
{
    public interface IPersonUsecase
    {
        Task<Result> CreateAsync(Person person);
    }
}
