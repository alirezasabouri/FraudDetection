using Fastenshtein;
using FraudDetection.Contracts.Services;
using System.Text.RegularExpressions;

namespace FraudDetection.Services
{
    public class NameSimilarityCheck : INameSimilarityCheck
    {
        private bool IsInitialOf(string initial, string name)
        {
            return (Regex.IsMatch(initial, "[A-Z]\\.") && name.ToLower()[0] == initial.ToLower()[0]);
        }

        private bool AreEqualsWithTypo(string name1, string name2)
        {
            var levenshteinChecker = new Levenshtein(name1.ToLower());
            return levenshteinChecker.DistanceFrom(name2.ToLower()) < 2;
        }

        public bool AreSimilar(string name1, string name2)
        {
            if (IsInitialOf(name1,name2) || IsInitialOf(name2, name1))
                return true;

            if (AreEqualsWithTypo(name1, name2))
                return true;

            return false;
        }
    }
}
