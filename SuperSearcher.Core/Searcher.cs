using SuperSearcher.Core.SearchStrategies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperSearcher.Core
{
    public class Searcher
    {
        private ISearcher _searcher;

        public List<string> searchHistory { get; set; }

        public Searcher()
        {
            searchHistory = new List<string>();
        }

        public Searcher (ISearcher searcher) : this()
        {
            this._searcher = searcher;
        }

        public void SetSearcher(ISearcher searcher)
        {
            this._searcher = searcher;
        }

        public async Task<List<string>> Search(string query, int resultsCount = 3)
        {
            searchHistory.Add(query);

            return await _searcher.Search(query, resultsCount);
        }
    }
}
