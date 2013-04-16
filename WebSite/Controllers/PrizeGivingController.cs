using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{

    public class PrizeGivingCOntroller : Controller
    {

    private string _key = "gaZDzSbELKKBORCiOizFHGjGicgURo61";

        private ODataMetadata<Tweet>  GetTweets(int skip)
        {
            HttpClient client;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", _key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = client.GetStringAsync("https://irishazureheads.azure-mobile.net/tables/Updates?$inlinecount=allpages&$skip="+skip).Result;
           return JsonConvert.DeserializeObject<ODataMetadata<Tweet>>(data);
        }

        private ODataMetadata<CrowdFeedback> GetFeedBackResponses(int skip)
        {
            HttpClient client;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", _key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = client.GetStringAsync("https://irishazureheads.azure-mobile.net/tables/Entry?$inlinecount=allpages&$skip=" + skip).Result;
            return JsonConvert.DeserializeObject<ODataMetadata<CrowdFeedback>>(data);
        }


        private List<CrowdFeedback> GetCorkOnlyResponses()
        {
            HttpClient client;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", _key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = client.GetStringAsync("https://irishazureheads.azure-mobile.net/tables/CorkEntry").Result;
            return JsonConvert.DeserializeObject<List<CrowdFeedback>>(data);
        }


        private List<CrowdFeedback> GetDublinResponses()
        {
            ODataMetadata<CrowdFeedback> data = GetFeedBackResponses(0);

            long countTweets = data.Count.Value;


            List<CrowdFeedback> allResponses = data.Results.ToList();

            // loop till we get em all 

            while (allResponses.Count < data.Count)
            {

                data = GetFeedBackResponses(allResponses.Count);
                allResponses.AddRange(data.Results.ToList());
            }


            //Filter in the client .. inefficient

            return allResponses.FindAll(z => z.City == "Dublin");


        }


        private List<CrowdFeedback> GetCorkResponses()
        {
            var allResponses = GetCorkOnlyResponses();
            return allResponses;


        }

        //
        // GET: /PrizeGiving/Index

        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /PrizeGiving/Tweets

        public ActionResult Tweets()
        {

            ODataMetadata<Tweet> tweets = GetTweets(0);

            long countTweets = tweets.Count.Value;


            List<Tweet> allTweets = tweets.Results.ToList();

            // loop till we get em all 

            while (allTweets.Count < tweets.Count)
            {

                tweets = GetTweets(allTweets.Count);
                allTweets.AddRange(tweets.Results.ToList());
            }

            return View(allTweets);
        }


        //
        // GET: /PrizeGiving/Cork

        public ActionResult Cork()
        {

            var data = GetCorkResponses();
            return View(data);
        }


        public ActionResult Dublin()
        {

            var data = GetDublinResponses();
            return View(data);
        }

    }
       
}
