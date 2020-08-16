using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using SearchAnalytics.Models;
using Newtonsoft.Json;
using System.Web.Services.Description;

namespace SearchAnalytics.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        // Method that keeps track of number of clicks per item.
        // Input Parameters: ItemId - Integer - TrackId of the media
        // The method sends a GET request with TrackId to fetch the content data from iTunes database.
        // Return: Boolean - True denotes success
        public bool ClickSearch(int ItemId)
        {
            SearchResult sobj = SearchResult.GetInstance();
            SearchItem LookupItem = sobj.GetItem(ItemId);
            if(LookupItem == null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://itunes.apple.com/lookup?id=" + ItemId.ToString());
                    try
                    {
                        var responseTask = client.GetAsync(client.BaseAddress);
                        responseTask.Wait();
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();
                            JsonResults ApiResults = JsonConvert.DeserializeObject<JsonResults>(readTask.Result);
                            LookupItem = ApiResults.Results[0];
                            LookupItem.Clicks++;
                            sobj.Append(LookupItem);

                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "Potential Exception - " + e.Message; 
                        return false;
                    }
                }
            }
            else
            {
                sobj.Increment(LookupItem.Id);
            }
            
            
            return true;
        }


        // Method that searches the iTunes API for a given input string.
        // Input Parameters: SearchString - String - Spaces are replaced with + symbol
        // The method fetches top 50 search results from the iTunes API and Deserializes it into JSON object and sends to View.
        // Return: View with Deserialized JSON object containing Result Count and Results
        public ActionResult Search(string searchString)
        {
            JsonResults ApiResults = new JsonResults();
            searchString = searchString.Replace(" ", "+");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://itunes.apple.com/search?term=" + searchString);
                try 
                {
                    var responseTask = client.GetAsync(client.BaseAddress);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        ApiResults = JsonConvert.DeserializeObject<JsonResults>(readTask.Result);
                        ApiResults.Results = ApiResults.Results.FindAll(item => item.Id != 0).ToList();
                    }
                }
                catch(Exception e)
                {
                    ViewBag.Message = "Error occured while fetching from iTunes API. Potential exception - " + e.Message;
                    return View("Exception");
                }
                
            }
            return View(ApiResults);
        }

        // Method that returns the Click Count View
        // Returns: View with a dictionary of Integer and SearchItem object.
        public ActionResult CheckClicks()
        {
            SearchResult sobj = SearchResult.GetInstance();
            return View(sobj.GetResults());
        }
    }
}