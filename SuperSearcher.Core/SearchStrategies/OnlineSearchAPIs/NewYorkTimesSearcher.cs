using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuperSearcher.Core.SearchStrategies.OnlineSearchers
{
    class NewYorkTimesSearcher : ISearcher
    {
        private string apiKey;

        public NewYorkTimesSearcher()
        {
            this.apiKey = "hcEZf7sgmXqN3MUPHw9fYxol6Dpl9isl";
        }

        public async Task<List<string>> Search(string query, int resultsCount = 3)
        {
            var client = new HttpClient();
            UriBuilder requestUri = new UriBuilder("https://api.nytimes.com/svc/search/v2/articlesearch.json");
            requestUri.Query = $"?q={query}&api-key={apiKey}";

            List<string> results = new List<string>();

            using (JsonDocument document = await client.GetFromJsonAsync<JsonDocument>(requestUri.Uri))
            {
                JsonElement root = document.RootElement;
                JsonElement docs = root.GetProperty("response").GetProperty("docs");

                int i = 0;
                foreach (JsonElement doc in docs.EnumerateArray())
                {
                    string paragraph = doc.GetProperty("lead_paragraph").ToString();
                    if (paragraph.Length > 0)
                    {
                        results.Add(paragraph);
                        i++;
                    }

                    if (i == resultsCount) break;
                }
            }

            return results;
        }
    }
}
