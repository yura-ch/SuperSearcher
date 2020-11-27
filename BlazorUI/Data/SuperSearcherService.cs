using Microsoft.AspNetCore.Components;
using SuperSearcher.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Data
{
    public class SuperSearcherService
    {
        private Searcher searcher = new Searcher();

        public List<string> searchResults { get; set; }

        public int resultsCount = 3;

        public string[] GetSearchHistory()
        {
            return searcher.searchHistory.Distinct().Take(5).ToArray();
        }

        public void SetCurrentSearcher(string selectedSearcher)
        {
            switch (selectedSearcher)
            {
                case "FileSystem":
                    searcher.SetSearcher(SearcherFactory.GetFileSystemSearcher());
                    break;
                case "NewYorkTimes":
                    searcher.SetSearcher(SearcherFactory.GetOnlineSearcher());
                    break;
                default:
                    break;
            }

            Console.WriteLine("Selected searcher: " + selectedSearcher);
        }

        public async Task Search(string query)
        {
            searchResults = await searcher.Search(query, resultsCount);
        }
    }
}
