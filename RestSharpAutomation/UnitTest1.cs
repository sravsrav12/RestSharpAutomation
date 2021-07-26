using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpAutomation.ModelClass;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace RestSharpAutomation
{
    [TestClass]
    public class UnitTest1
    {
        private string getUrl = "https://rahulshettyacademy.com/maps/api/place/get/json?key=qaclick123&place_id=22d7a25ebb6bf4923f1d6a505a5d9b46";
        [TestMethod]
        public void TestResthsharpGET()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            IRestResponse restResponse = restClient.Get(restRequest);
            Console.WriteLine(restResponse.IsSuccessful);
            Console.WriteLine(restResponse.StatusCode);
            Console.WriteLine(restResponse.ErrorMessage);
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("statusCode :" + restResponse.StatusCode);
                Console.WriteLine("statusCode :" + (int)restResponse.StatusCode);
                Console.WriteLine("Response Content :" + restResponse.Content);
            }
            else
            {
                Console.WriteLine(restResponse.ErrorMessage);
                Console.WriteLine(restResponse.ErrorException);
            }


        }
        [TestMethod]
        public void TestRestsharpGETinXMLformat()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/xml");
            IRestResponse restResponse = restClient.Get(restRequest);
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("statusCode :" + restResponse.StatusCode);
                Console.WriteLine("statusCode :" + (int)restResponse.StatusCode);
                Console.WriteLine("Response Content :" + restResponse.Content);
            }
            else
            {
                Console.WriteLine(restResponse.ErrorMessage);
                Console.WriteLine(restResponse.ErrorException);
            }

        }
        [TestMethod]
        public void TestRestsharpGETJson_Deserialize()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse<List<JsonRootObject>> restResponse = restClient.Get<List<JsonRootObject>>(restRequest);
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("StatusCode" + (int)restResponse.StatusCode);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                Console.WriteLine("StatusCode" + restResponse.StatusCode);
                Console.WriteLine("StatusCode" + restResponse.Data.Count);
                List<JsonRootObject> data = restResponse.Data;
                JsonRootObject jsonRootObjects = data.Find((x) =>
             {
                 return x.language == "French-IN";
             });
                Assert.AreEqual("French-IN", jsonRootObjects.language);
                Console.WriteLine(jsonRootObjects.language);

            }
        }
        [TestMethod]
        public void TestRestSharpGET_XML_Deserialize()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/xml");
            //create object of deserializer
            var dotNetxmlDeserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();
            //use normal get method
            IRestResponse restResponse = restClient.Get(restRequest);
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("StatusCode" + (int)restResponse.StatusCode);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                Console.WriteLine("StatusCode" + restResponse.StatusCode);
                LaptopDetails data = dotNetxmlDeserializer.Deserialize<LaptopDetails>(restResponse);
                //  Console.WriteLine("size of list" + data.Laptop.count);
                //x anonymous function
                //Laptop laptop = data.Laptop.Find((x) =>
                // {
                //   return x.Id.Equals("1", StringComparison.OrdinalIgnoreCase);
                // });
                // Assert.AreEqual("French-IN", laptop.language);
                // Console.WriteLine(laptop.language);

            }
        }
        [TestMethod]
        public void TestGetwithExecute()
        {
            // 2 executes one uisng type<> parameter, one as normal.
            //one using tyope<> parameter atomatically deserilize
            IRestClient restClient = new RestClient();
            //Rest Request contains properties Method, End point url
            IRestRequest restRequest = new RestRequest() {
                Method = Method.GET,
                Resource = getUrl
            };
            IRestResponse restResponse = restClient.Execute<List<JsonRootObject>>(restRequest);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
        }
        [TestMethod]
        public void TestGetWithjsonusing_Helperclass()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept","application/json" }
            };
            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformGetRequest(getUrl, headers);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);

            IRestResponse<JsonRootObject> restResponse1 = restClientHelper.PerformGetRequest
                <JsonRootObject>(getUrl, headers);
            Console.WriteLine(restResponse1.Content);
            Assert.IsNotNull(restResponse1.Data, "Content is NUll/Empty");

        }
        private Random random = new Random();
        private string posturl = "https://rahulshettyacademy.com/maps/api/place/get/json?key=qaclick123&place_id=22d7a25ebb6bf4923f1d6a505a5d9b46";
        [TestMethod]
        public void TestRestSharpPost_WithJson()
        {
            int accuracy = random.Next(1000);
            string jsonData = "{" +
                                    "\"location\": { " +
                                     "\"lat\": -38.383494," +
                                     "\"lng\": 33.427362" +
                                                 "}," +
                                     "\"accuracy\": " + accuracy + ", " +
                                     "\"name\": \"Frontline house\"," +
                                     "\"phone_number\": \"(+91) 983 893 3937\"," +
                                     "\"address\": \"29, side layout, cohen 09\"," +
                                     "\"types\": [" +
                                     "\"shoe park\"," +
                                     "\"shop\"" +
                                      "]," +
                                     "\"website\": \"http://google.com\"," +
                                     "\"language\": \"French-IN\"" +
                                       "}";
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = posturl,

            };
            //Heder to define request
            restRequest.AddHeader("Content-type", "application/json");
            //Header to define in which format application should give response
            restRequest.AddHeader("Accept", "application/json");
            //attach jsonbody with request, there is a method ADdjsonody
            restRequest.AddJsonBody(jsonData);
            IRestResponse restResponse = restClient.Post(restRequest);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);
        }
        //POst with Helper class
        [TestMethod]
        public void TestRestSharpPost_WithHelperClass()
        {
            int accuracy = random.Next(1000);
            string jsonData = "{" +
                                    "\"location\": { " +
                                     "\"lat\": -38.383494," +
                                     "\"lng\": 33.427362" +
                                                 "}," +
                                     "\"accuracy\": " + accuracy + ", " +
                                     "\"name\": \"Frontline house\"," +
                                     "\"phone_number\": \"(+91) 983 893 3937\"," +
                                     "\"address\": \"29, side layout, cohen 09\"," +
                                     "\"types\": [" +
                                     "\"shoe park\"," +
                                     "\"shop\"" +
                                      "]," +
                                     "\"website\": \"http://google.com\"," +
                                     "\"language\": \"French-IN\"" +
                                       "}";
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = posturl,

            };
            //Heder to define request
            restRequest.AddHeader("Content-type", "application/json");
            //Header to define in which format application should give response
            restRequest.AddHeader("Accept", "application/json");
            //Set dataformat property to invoke default serilizer to convert the object into json and attach it with the request
            restRequest.RequestFormat = DataFormat.Json;
            //GetLaptopObject() created model class to load complex payload
           // restRequest.AddBody(GetLaptopObject());
            IRestResponse restResponse = restClient.Post(restRequest);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                { "Accept","application/json"}
            };
            RestClientHelper restClientHelper = new RestClientHelper();
            //GetLaptopObject() created model class to load complex payload
            // restClientHelper.PerformPostRequest<JsonRootObject>(posturl,headers,GetLaptopObject() DataFormat.Json);
        }
        private string puturl = "";
        [TestMethod]
        public void TestPUTwithRestSharp()
        {
            //post to create record
            //put to update
            //Get using id and fetch the record and validate
            int accuracy = random.Next(1000);
            string jsonData = "{" +
                                    "\"location\": { " +
                                     "\"lat\": -38.383494," +
                                     "\"lng\": 33.427362" +
                                                 "}," +
                                     "\"accuracy\": " + accuracy + ", " +
                                     "\"name\": \"Frontline house\"," +
                                     "\"phone_number\": \"(+91) 983 893 3937\"," +
                                     "\"address\": \"29, side layout, cohen 09\"," +
                                     "\"types\": [" +
                                     "\"shoe park\"," +
                                     "\"shop\"" +
                                      "]," +
                                     "\"website\": \"http://google.com\"," +
                                     "\"language\": \"French-IN\"" +
                                       "}";
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                { "Accept","application/json"}
            };
            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(posturl, headers, jsonData, RestSharp.DataFormat.Json);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            jsonData = "{" +
                                   "\"location\": { " +
                                    "\"lat\": -38.383494," +
                                    "\"lng\": 33.427362" +
                                                "}," +
                                    "\"accuracy\": " + accuracy + ", " +
                                    "\"name\": \"Frontline house\"," +
                                    "\"phone_number\": \"(+91) 983 893 3953\"," +
                                    "\"address\": \"29, side layout, cohen 09\"," +
                                    "\"types\": [" +
                                    "\"shoe park\"," +
                                    "\"shop\"" +
                                     "]," +
                                    "\"website\": \"http://google.com\"," +
                                    "\"language\": \"French-IN\"" +
                                      "}";
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = puturl
            };
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(jsonData);
            IRestResponse<JsonRootObject> response = restClient.Put<JsonRootObject>(restRequest);
            Assert.AreEqual(200, (int)restResponse.StatusCode);

            headers = new Dictionary<string, string>()
            {
                { "Accept","application/json"}
            };
            response = restClientHelper.PerformGetRequest<JsonRootObject>(getUrl, headers);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
        }
        //Put using helper class
        [TestMethod]
        public void TestPUTwithRestSharp_Helperclass()
        {
            //post to create record
            //put to update
            //Get using id and fetch the record and validate
            int accuracy = random.Next(1000);
            string jsonData = "{" +
                                    "\"location\": { " +
                                     "\"lat\": -38.383494," +
                                     "\"lng\": 33.427362" +
                                                 "}," +
                                     "\"accuracy\": " + accuracy + ", " +
                                     "\"name\": \"Frontline house\"," +
                                     "\"phone_number\": \"(+91) 983 893 3937\"," +
                                     "\"address\": \"29, side layout, cohen 09\"," +
                                     "\"types\": [" +
                                     "\"shoe park\"," +
                                     "\"shop\"" +
                                      "]," +
                                     "\"website\": \"http://google.com\"," +
                                     "\"language\": \"French-IN\"" +
                                       "}";
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                { "Accept","application/json"}
            };
            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(posturl, headers, jsonData, RestSharp.DataFormat.Json);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            jsonData = "{" +
                                   "\"location\": { " +
                                    "\"lat\": -38.383494," +
                                    "\"lng\": 33.427362" +
                                                "}," +
                                    "\"accuracy\": " + accuracy + ", " +
                                    "\"name\": \"Frontline house\"," +
                                    "\"phone_number\": \"(+91) 983 893 3953\"," +
                                    "\"address\": \"29, side layout, cohen 09\"," +
                                    "\"types\": [" +
                                    "\"shoe park\"," +
                                    "\"shop\"" +
                                     "]," +
                                    "\"website\": \"http://google.com\"," +
                                    "\"language\": \"French-IN\"" +
                                      "}";
            var restResponse2 = restClientHelper.PerformPutRequest<JsonRootObject>(puturl, headers, jsonData, DataFormat.Json);
            Assert.AreEqual(200, (int)restResponse2.StatusCode);

            headers = new Dictionary<string, string>()
            {
                { "Accept","application/json"}
            };
            var response = restClientHelper.PerformGetRequest<JsonRootObject>(getUrl, headers);
            Assert.AreEqual(200, (int)response.StatusCode);
        }
        private string deleteEndPoint = "https://rahulshettyacademy.com/maps/api/place/get/json?key=qaclick123&place_id=22d7a25ebb6bf4923f1d6a505a5d9b46";
        [TestMethod]
        public void TestDElete_RestSHarp()
        {
            //Post to create a record
            //Call the delete end point to delete the record – 200ok
            // Call the delete end point – 404 not found
            int accuracy = random.Next(1000);
            string jsonData = "{" +
                                    "\"location\": { " +
                                     "\"lat\": -38.383494," +
                                     "\"lng\": 33.427362" +
                                                 "}," +
                                     "\"accuracy\": " + accuracy + ", " +
                                     "\"name\": \"Frontline house\"," +
                                     "\"phone_number\": \"(+91) 983 893 3937\"," +
                                     "\"address\": \"29, side layout, cohen 09\"," +
                                     "\"types\": [" +
                                     "\"shoe park\"," +
                                     "\"shop\"" +
                                      "]," +
                                     "\"website\": \"http://google.com\"," +
                                     "\"language\": \"French-IN\"" +
                                       "}";
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                { "Accept","application/json"}
            };
            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(posturl, headers, jsonData, RestSharp.DataFormat.Json);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = deleteEndPoint 
            };
            restRequest.AddHeader("Accept","*/*");
            IRestResponse response = restClient.Delete(restRequest);
            Assert.AreEqual(200, (int)response.StatusCode);
            //In postman it is automatically adding an header while deleting the request
            //it is adding header("Accept","*/*"), add this header with delete request
            //*/* simply means that any data of whatever mimetype is accepted

            response = restClient.Delete(restRequest);
            Assert.AreEqual(404, (int)response.StatusCode);
        }
        [TestMethod]
        public void TestDElete_RestSHarp_HelperClass()
        {
            //Post to create a record
            //Call the delete end point to delete the record – 200ok
            // Call the delete end point – 404 not found
            int accuracy = random.Next(1000);
            string jsonData = "{" +
                                    "\"location\": { " +
                                     "\"lat\": -38.383494," +
                                     "\"lng\": 33.427362" +
                                                 "}," +
                                     "\"accuracy\": " + accuracy + ", " +
                                     "\"name\": \"Frontline house\"," +
                                     "\"phone_number\": \"(+91) 983 893 3937\"," +
                                     "\"address\": \"29, side layout, cohen 09\"," +
                                     "\"types\": [" +
                                     "\"shoe park\"," +
                                     "\"shop\"" +
                                      "]," +
                                     "\"website\": \"http://google.com\"," +
                                     "\"language\": \"French-IN\"" +
                                       "}";
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                { "Accept","application/json"}
            };
            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(posturl, headers, jsonData, RestSharp.DataFormat.Json);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            headers = new Dictionary<string, string>()
            {
                
                { "Accept","*/*"}
            };
            IRestResponse response = restClientHelper.PerformDeleteRequest(deleteEndPoint + accuracy, headers);
            Assert.AreEqual(200, (int)response.StatusCode);
            //In postman it is automatically adding an header while deleting the request
            //it is adding header("Accept","*/*"), add this header with delete request
            //*/* simply means that any data of whatever mimetype is accepted

            response = restClientHelper.PerformDeleteRequest(deleteEndPoint , headers);
            Assert.AreEqual(404, (int)response.StatusCode);
        }
        private string searchUrl = "";
        [TestMethod]
        public void Testusing_QueryParameter()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = searchUrl
            };
            restRequest.AddHeader("Accept","application/json");
            //2 ways to declare parameters, usiing Addparameter
            //Add Parameter takes 3 arguments Param1 -Name, Param2- Value, Param3 - Parameter Type
            restRequest.AddParameter("id","3",ParameterType.QueryString);
            //AddQueryParameter is the another way
            restRequest.AddQueryParameter("id","1");
            restRequest.AddQueryParameter("laptopname","Alienware M17");
            var response = restClient.Get<Laptop>(restRequest);
            //HttpStatusCode is an Enum type Consisting all status codes, coming from namespace System.net
            Assert.AreEqual(System.Net.HttpStatusCode.OK,response.StatusCode);
           // Assert.AreEqual("Alienware",response.Data.BrandName);
        }
        //RestSharp has in built different types of Authentication mechanism
        //Using HttpBasicAuthenticator
        private string secureUrl = "";
        [TestMethod]
        public void TestSecureGet()
        {
            IRestClient restClient = new RestClient();
            //HttpBasicAuthenticator takes two params username & pass.
            //Authenticator automatically encoded user name & Password to send  with request
            restClient.Authenticator = new HttpBasicAuthenticator("admin", "welcome");
            IRestRequest restRequest = new RestRequest()
            {
                Resource = secureUrl
            };
            //In RestClient class they is a property Authenticator to send authentication info

            //Token BAsed Authentication mechanism

        }

        /* int accuracy = random.Next(1000);
         string jsonData = "{" +
                            "\"environment\": { " +
                            "\"clientTypeId\": 5," +
                            "\"languageCode\": \"en\"" +
                                            "}," +
                            "\"userName\": \"{{userName}}\"," +
                            "\"password\": \"{{password}}\"," +
                            "\"sessionProductId\": \"{{ productID}}\"," +
                            "\"numLaunchTokens\": 1," +
                            "\"marketType\": \"{{mmarket}}\"" +
                           "}"; */


        static string hostName = ConfigurationManager.AppSettings["hostName"];
        string userName = ConfigurationManager.AppSettings["userName"];
        string password = ConfigurationManager.AppSettings["password"];
        string productId = "";
        string mmarket = "";
        string guid = ConfigurationManager.AppSettings["guid"];
        private JsonRoot GetJsonRootObject()
        {
            JsonRoot jsonObject = new JsonRoot();

            jsonObject.userName = userName;
            jsonObject.password = password;
            jsonObject.sessionProductId = productId;
            jsonObject.marketType = mmarket;
            jsonObject.numLaunchTokens = random.Next(1000);
            Environment environment = new Environment();
            environment.clientTypeId = random.Next(1000);
            environment.languageCode = "en";
            Tokens tokens = new Tokens();
            
            
            return jsonObject;
        }
        private Packet GetPacketObject()
        {
            Packet packet = new Packet();
            packet.packetType = random.Next(1000);
            packet.payload = "";
            packet.useFilter = true;
            packet.isBase64Encoded = false;
            Balances balances = new Balances();
            Financials financials = new Financials();
            return packet;
        }
        [TestMethod]
        public void TestRestSharpPostEndPoint_WithHelperClass()
        {

            string postEndpoint = hostName + "/v1/accounts/login/real";
            try
            {
                Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"X-CorrelationId","{{$guid}}" +guid },
                {"X-Forwarded-For","{{$guid}}" +guid},
                {"X-Clienttypeid", "5" },
                {"Content-Type","application/json" }
            };
                RestClientHelper restClientHelper = new RestClientHelper();
                //GetLaptopObject created model class to attach complex payload/body with Post request
                IRestResponse<JsonRoot> restResponse = restClientHelper.PerformPostRequest<JsonRoot>
                (postEndpoint, headers, GetJsonRootObject(), DataFormat.Json);
                Assert.AreEqual(200, (int)restResponse.StatusCode);

                //fecting usertoken, added tokens class in JsonRoot class, Declared Tokens class object in GetJsonRootObject.
                string userToken = restResponse.Data.tokens.userToken;
                int moduleID = random.Next(1000);
                int productId = random.Next(1000);
                int clientID = random.Next(1000);
                int serverID = random.Next(1000);
                string refreshpostEndPoint = hostName + "/v1/games/module/{{$moduleID}}/client/{{$clientID}}/play" +moduleID +clientID;
                Dictionary<string, string> headers1 = new Dictionary<string, string>()
            {
                {"Authorization:", "Bearer" + userToken},
                {"X-CorrelationId","93D10259-30F8-4339-B456-3F30A43F65A2" },
                {"X-Route-ProductId",""+ productId },
                {"X-Route-ModuleId",""+ moduleID },
                {"Content-Type","application/json" }
            };

                RestClientHelper restClientHelper1 = new RestClientHelper();
                //GetLaptopObject created model class to attach complex payload/body with Post request
                IRestResponse<Packet> restResponse1 = restClientHelper.PerformPostRequest<Packet>
                (refreshpostEndPoint, headers, GetPacketObject(), DataFormat.Json);
                Assert.AreEqual(200, (int)restResponse1.StatusCode);

                bool isNumericLoyalty = restClientHelper.IsNumeric(restResponse1.Data.balances.loyaltyBalance);
                Assert.IsTrue(isNumericLoyalty, "User Balance is not Numeric");
                bool totalInAccount = restClientHelper.IsNumeric(restResponse1.Data.balances.totalInAccountCurrency);
                Assert.IsTrue(totalInAccount, "User Account Balance is not Numeric");
                bool payoutAmount = restClientHelper.IsNumeric(restResponse1.Data.financials.payoutAmount);
                Assert.IsTrue(payoutAmount, "User Pay Out Amount is not NUmeric");
                bool betAmount = restClientHelper.IsNumeric(restResponse1.Data.financials.betAmount);
                Assert.IsTrue(betAmount, "User Bet Amount is not Numeric");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to validate the player balance");
            }

        }



    }
}













































































































































































































































