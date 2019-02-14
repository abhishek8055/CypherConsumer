using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace CypherConsumer.ApiHelper
{
    public static class ApiConnection
    {
        /// <summary>
        /// Used to post object of generic class<T> to api
        /// </summary>
        /// <param name="Tobject">The generic object</param>
        /// <param name="url">api url</param>
        /// <param name="controllerAndOrAction">The remaining part of api url like action and/or controller</param>
        
        public static T PostDataToApi<T>(T Tobject, string url, string controllerAndOrAction)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    //HTTP POST
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage httpResponseMessage = client.PostAsJsonAsync(url + controllerAndOrAction, Tobject).Result;

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var readTask = httpResponseMessage.Content.ReadAsAsync<T>();
                        readTask.Wait();

                        Tobject = (T)readTask.Result;
                    }
                    else //web api sent error response
                    {
                        //log response status here..

                        Tobject = default(T);
                    }
                }
            }
            catch (Exception e)
            {
                //Log.Fatal("Exception Inside ApiHelper Class in Add Method for Class:" + typeof(T), e);
            }
            return Tobject;

        }

        public static T GetDataFromApi<T>(string url, string controllerAndOrAction)
        {
            T item = default(T);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    //HTTP GET
                    //client
                    var responseTask = client.GetAsync(controllerAndOrAction);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<T>();
                        readTask.Wait();

                        item = readTask.Result;
                    }
                    else //web api sent error response
                    {
                        //log response status here..

                        item = default(T);
                    }
                }
            }
            catch (Exception e)
            {
                //Log.Fatal("Exception Inside ApiHelper Class in GetDataFromApi Method for Class:" + typeof(T), e);
            }
            return item;
        }


    }

    public static class URL
    {
        public static string IISUrl
        {
            get
            {
                return "http://localhost/cypherapi/api/";
            }
        }
        public static string ExpressUrl
        {
            
            get
            {
                return "http://localhost:12314/api/";
            }
        }
    }
}