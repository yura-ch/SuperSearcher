using SuperSearcher.Core.SearchStrategies;
using SuperSearcher.Core.SearchStrategies.OnlineSearchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSearcher.Core
{
    public static class SearcherFactory
    {
        public static ISearcher GetOnlineSearcher()
        {
            return new NewYorkTimesSearcher();
        }

        public static ISearcher GetFileSystemSearcher()
        {
            return new FileSystemSearcher();
        }
    }
}
