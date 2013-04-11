using MVC_Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MVC_Client.Controllers
{
    public class PrizeGivingController : Controller
    {

        private string _key = "NYUTJeqEKdbitDyuDAzrDbtmcMZLzs78";



        private ODataMetadata<Tweet>  GetTweets(int skip)
        {
            HttpClient client;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", _key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = client.GetStringAsync("https://corkug.azure-mobile.net/tables/Updates?$inlinecount=allpages&$skip="+skip).Result;
           return JsonConvert.DeserializeObject<ODataMetadata<Tweet>>(data);
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
        // GET: /PrizeGiving/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PrizeGiving/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PrizeGiving/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PrizeGiving/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PrizeGiving/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PrizeGiving/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PrizeGiving/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
