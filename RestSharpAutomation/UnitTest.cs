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
    public class UnitTest
    {
        private Random random = new Random();
        static string hostName = ConfigurationManager.AppSettings["hostName"];
        string userName = ConfigurationManager.AppSettings["userName"];
        string password = ConfigurationManager.AppSettings["password"];
        string productId = "";
        string mmarket = "";
        string guid = ConfigurationManager.AppSettings["guid"];
       
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
                int productID = random.Next(1000);
                int clientID = random.Next(1000);
                int serverID = random.Next(1000);
                string refreshpostEndPoint = hostName + "/v1/games/module/{{$moduleID}}/client/{{$clientID}}/play" + moduleID + clientID;
                Dictionary<string, string> headers1 = new Dictionary<string, string>()
            {
                {"Authorization:", "Bearer" + userToken},
                {"X-CorrelationId","93D10259-30F8-4339-B456-3F30A43F65A2" },
                {"X-Route-ProductId",""+ productID },
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

    }
}













































































































































































































































