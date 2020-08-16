using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchAnalytics.Models
{
    // A singleton design class for storing all the clicked results.
    public class SearchResult
    {
        // A dictionary to store SearchItem as Value and its TrackId as the Key
        private Dictionary<int,SearchItem> results = new Dictionary<int,SearchItem>();

        private SearchResult() { }

        private static SearchResult _instance;

        public static SearchResult GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SearchResult();
            }
            return _instance;
        }

        // Method to add new entries to the dictionary
        public void Append(SearchItem obj)
        {
            this.results[obj.Id] = obj;
        }

        // Method to increment the Click Count of SearchItem
        public bool Increment(int id)
        {
            if (this.results.ContainsKey(id))
            {
                this.results[id].Clicks++;
                return true;
            }
            return false;
        }

        // Method to return the Item if the item is already in the dictionary
        public SearchItem GetItem(int id)
        {
            if (this.results.ContainsKey(id))
            {
                return this.results[id];
            }
            else
            {
                return null;
            }
        }

        // Method to return the dictionary
        public Dictionary<int,SearchItem> GetResults()
        {
            return this.results;
        }

    }
}