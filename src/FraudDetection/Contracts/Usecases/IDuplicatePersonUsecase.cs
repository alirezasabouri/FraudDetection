using FraudDetection.Misc;
using FraudDetection.Models;
using System.Threading.Tasks;

namespace FraudDetection.Contracts.Usecases
{
    public interface IDuplicatePersonUsecase
    {
        /// <summary>
        /// Compare two provided persons for smiliarities and return matching rank
        /// </summary>
        /// <returns>Matching rank expressed in percentage 0.0 to 1.0</returns>
        Task<Result<decimal>> FindSimilarityRankAsync(Person person1, Person person2);
    }
}
