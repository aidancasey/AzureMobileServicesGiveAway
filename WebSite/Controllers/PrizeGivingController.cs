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

        private ODataMetadata<AudienceFeedback> GetFeedBackResponses(int skip)
        {
            HttpClient client;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", _key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = client.GetStringAsync("https://irishazureheads.azure-mobile.net/tables/Entry?$inlinecount=allpages&$skip=" + skip).Result;
            return JsonConvert.DeserializeObject<ODataMetadata<AudienceFeedback>>(data);
        }


        private List<AudienceFeedback> GetCorkOnlyResponses()
        {
            HttpClient client;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", _key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // filer data on server ...

            var data = client.GetStringAsync("https://irishazureheads.azure-mobile.net/tables/CorkEntry").Result;
            return JsonConvert.DeserializeObject<List<AudienceFeedback>>(data);
        }


        private List<AudienceFeedback> GetDublinResponses()
        {
            ODataMetadata<AudienceFeedback> data = GetFeedBackResponses(0);

            long countTweets = data.Count.Value;


            List<AudienceFeedback> allResponses = data.Results.ToList();

            // filter data on client

            while (allResponses.Count < data.Count)
            {

                data = GetFeedBackResponses(allResponses.Count);
                allResponses.AddRange(data.Results.ToList());
            }


            //Filter in the client .. inefficient

            return allResponses.FindAll(z => z.City == "Dublin");


        }


        private List<AudienceFeedback> GetCorkResponses()
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
