using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation
{
    public class RestClientHelper
    {
        //Get RestClient -> Create RestClient and return it
        private IRestClient GetRestClient()
        {
            IRestClient restClient = new RestClient();
            return restClient;
        }
        //Get Rest request -> create a request and return it
        private IRestRequest GetRestRequest(string url, Dictionary<string, string> headers, Method method, 
            object body,DataFormat dataFormat)
        {
            IRestRequest restRequest = new RestRequest()
            {
                Method = method,
                Resource = url
            };
            if (headers != null)
            {
                foreach (string key in headers.Keys)
                {
                    restRequest.AddHeader(key, headers[key]);
                }
            }
            return restRequest;
        }
        //SendRequest -> Send the request using client and return the response
        private IRestResponse SendRequest(IRestRequest restRequest)
        {
            IRestClient restClient = GetRestClient();
            //Use Execute method
            IRestResponse restResponse = restClient.Execute(restRequest);
            return restResponse;
        }
        //SendRequest -> Send the request using client and return the response and deserialize
        //type argument must have public parameterless constructor
        private IRestResponse<T> SendRequest<T>(IRestRequest restRequest)where T : new()
        {
            IRestClient restClient = GetRestClient();
            IRestResponse<T> restResponse = restClient.Execute<T>(restRequest);
        //In case of XML following deserialization, for JSon Deserilization will happen automatically
            if (restResponse.ContentType.Equals("application/xml")) {
                var deserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();
                restResponse.Data = deserializer.Deserialize<T>(restResponse);
            }
            return restResponse;
        }

        public IRestResponse PerformGetRequest(string url,Dictionary<string,string> headers)
        {
            IRestRequest restRequest = GetRestRequest(url,headers,Method.GET,null,DataFormat.None);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        //This request is to get request and deserialize the response
        //for json deserialization is done automatically
        public IRestResponse<T> PerformGetRequest<T>(string url, Dictionary<string, string> headers) where T : new()
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.GET,null,DataFormat.None);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }
        //with type parameter
        //data format to specify whether json, so that deserialization done automaticaalyy
        //Data type of body is object
        public IRestResponse<T> PerformPostRequest<T>(string url, Dictionary<string,string> headers, object body,
            DataFormat dataFormat)where T : new()
        {
            IRestRequest restRequest = GetRestRequest(url,headers,Method.POST,body,dataFormat);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }
        //without type parameter
        public IRestResponse PerformPostRequest(string url, Dictionary<string, string> headers, object body,
           DataFormat dataFormat) 
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.POST, body, dataFormat);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        public IRestResponse<T> PerformPutRequest<T>(string url, Dictionary<string, string> headers, object body,
          DataFormat dataFormat) where T : new()
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.PUT, body, dataFormat);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }
        public IRestResponse PerformPutRequest(string url, Dictionary<string, string> headers, object body,
           DataFormat dataFormat)
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.PUT, body, dataFormat);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        public IRestResponse PerformDeleteRequest(string url, Dictionary<string, string> headers)
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.PUT, null, DataFormat.None);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        public bool IsNumeric(double n)
        {
            bool isNum = false;
             if(n>= 0)
            {
                isNum = true;
            }
            return isNum;
        }
    }
}
