namespace FraudDetection.Contracts.Services
{
    public interface INameSimilarityCheck
    {
        bool AreSimilar(string name1, string name2);
    }
}
