using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StationaryUI_7463.Data;
using StationaryUI_7463.Models;

namespace StationaryUI_7463.Controllers
{
    public class StationaryItemsController : Controller
    {

        /*string Baseurl = "https://localhost:5001/";*/
        private StationaryUI_7463Context db = new StationaryUI_7463Context();

        // GET: StationaryItems
        public async Task<ActionResult> Index()
        {
            //Hosted web API REST Service base url
            string Baseurl = "http://stationayshop-dev.us-east-1.elasticbeanstalk.com/";
            List<Stationary> ItemInfo = new List<Stationary>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees  using HttpClient
                 HttpResponseMessage Res = await client.GetAsync("api/Stationary");
                //Checking the response is successful or not which is sent using HttpClient
            if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var IResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                     ItemInfo = JsonConvert.DeserializeObject<List<Stationary>>(IResponse);
                }
                //returning the Product list to view
                return View(ItemInfo);
            }

        }

        // GET: StationaryItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Hosted web API REST Service base url
            string Baseurl = "http://stationayshop-dev.us-east-1.elasticbeanstalk.com/";
            Stationary ItemInfo = null;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Stationary/" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var IResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    ItemInfo = JsonConvert.DeserializeObject<Stationary>(IResponse);
                    return View(ItemInfo);
                }
                //returning the Product list to view
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: StationaryItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StationaryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price,QuantityAvailable,CountryOfProduction")] Stationary stationaryItem)
        {
            string Baseurl = "http://stationayshop-dev.us-east-1.elasticbeanstalk.com/";

            using (var client = new HttpClient())
            {
                //Passing service base url
                var rnd = new Random();
                stationaryItem.Id = rnd.Next();
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.PostAsJsonAsync("/api/Stationary/", stationaryItem);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
 return RedirectToAction("Index");
                }

                else
                {
return View();
                }
                    
                //returning the Product list to view

            }
        }

        // GET: StationaryItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = "http://stationayshop-dev.us-east-1.elasticbeanstalk.com/";
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Stationary/" + id);
                Stationary stationaryItem = null;
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    stationaryItem = JsonConvert.DeserializeObject<Stationary>(PrResponse);
                }
                //HTTP POST

               return View(stationaryItem);
            }
           
        }

        // POST: StationaryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Stationary stationaryItem)
        {
            string Baseurl = "http://stationayshop-dev.us-east-1.elasticbeanstalk.com/";

            Stationary ItemInfo = null;
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage Res = await client.GetAsync("api/Stationary/" + id);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var IResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Product list
                        ItemInfo = JsonConvert.DeserializeObject<Stationary>(IResponse);
                        return View(ItemInfo);
                    }

                    var task = client.PutAsJsonAsync<Stationary>("api/Stationary/" + stationaryItem.Id, stationaryItem);
                    task.Wait();
                    var res = task.Result;
                    if(res.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));

                }
                return RedirectToAction(nameof(Index));


            }
            catch
            {
                return View();
            }

            
        }

        // GET: StationaryItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Hosted web API REST Service base url
            string Baseurl = "http://stationayshop-dev.us-east-1.elasticbeanstalk.com/";
            Stationary ItemInfo = null;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Stationary/" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var IResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    ItemInfo = JsonConvert.DeserializeObject<Stationary>(IResponse);
                    return View(ItemInfo);
                }
                //returning the Product list to view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: StationaryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string Baseurl = "http://stationayshop-dev.us-east-1.elasticbeanstalk.com/";
           
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.DeleteAsync("api/Stationary/" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
                //returning the Product list to view
                
            }
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
