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



        //get all tweets...
        private ODataMetadata<Tweet>  GetTweets(int skip)
        {
            HttpClient client;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", "AcmqGAyjjQbuPlVrPXJYvmEmNRnolb43");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = client.GetStringAsync("https://twitteraggregator.azure-mobile.net/tables/Updates?$inlinecount=allpages&$skip="+skip).Result;
           return JsonConvert.DeserializeObject<ODataMetadata<Tweet>>(data);
        }

        private ODataMetadata<AudienceFeedback> GetFeedBackResponses(int skip, string city)
        {
            HttpClient client;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", "AcmqGAyjjQbuPlVrPXJYvmEmNRnolb43");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = client.GetStringAsync("https://twitteraggregator.azure-mobile.net/tables/Entry?$inlinecount=allpages&$skip=" + skip + "&city=" + city).Result;
            return JsonConvert.DeserializeObject<ODataMetadata<AudienceFeedback>>(data);
        }



        private List<AudienceFeedback> GetResponses(string cityName)
        {
            ODataMetadata<AudienceFeedback> data = GetFeedBackResponses(0, cityName);

            long countTweets = data.Count.Value;


            List<AudienceFeedback> allResponses = data.Results.ToList();



            while (allResponses.Count < data.Count)
            {

                data = GetFeedBackResponses(allResponses.Count, cityName);
                allResponses.AddRange(data.Results.ToList());
            }
            return allResponses;
         //   return allResponses.FindAll(z => z.City == cityName);


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


        // GET: /PrizeGiving/Cork

        public ActionResult Cork()
        {

            var data = GetResponses("Cork");
            return View(data);
        }

        // GET: /PrizeGiving/Dublin

        public ActionResult Dublin()
        {

            var data = GetResponses("Dublin");
            return View(data);
        }

    }
       
}
