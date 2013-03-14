using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Website.Models;

namespace Website.Controllers
{
    public class FeedbackController : ApiController
    {


        // HttpClient client;
        string key = "aSDJRgDQkzEAARLRQAaunUEEiKFYQI60";


        [HttpPost]
        [ActionName("SendFeedback")]
        public HttpResponseMessage PostComplex(Feedback data)
        {
            if (ModelState.IsValid && data != null)
            {
                // Convert any HTML markup in the status text.
                data.Comment = HttpUtility.HtmlEncode(data.Comment);


                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", key);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //  if (ModelState.IsValid)
                // {
                var obj = JsonConvert.SerializeObject(data, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                var request = new HttpRequestMessage(HttpMethod.Post, "https://ugfeedback.azure-mobile.net/tables/Feedback1");
                request.Content = new StringContent(obj, System.Text.Encoding.UTF8, "application/json");

                var foo = client.SendAsync(request).Result;

                if (!foo.IsSuccessStatusCode)
                    throw new HttpResponseException(foo.StatusCode);

                // Create a 201 response.
                var response = new HttpResponseMessage(HttpStatusCode.Created);
            //    {
            //        Content = new StringContent(foo.StatusCode)
            //    };
           //     response.Headers.Location =
            //        new Uri(Url.Link("DefaultApi", new { action = "status", id = id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

    }

    
}
