using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperSearcher.Core.SearchStrategies
{
    public interface ISearcher
    {
        /// <summary>
        /// Search by query.
        /// </summary>
        /// <param name="query">User entered search query.</param>
        /// <returns>Returns SearchResult object.</returns>
        Task<List<string>> Search(string query, int resultsCount);
    }
}
