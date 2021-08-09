using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.ModelClass
{
    public class RootModelClass
    {
        private static Random random = new Random();
        static string hostName = ConfigurationManager.AppSettings["hostName"];
        static string userName = ConfigurationManager.AppSettings["userName"];
        static string password = ConfigurationManager.AppSettings["password"];
        static string productId = "";
        static string mmarket = "";
        static string guid = ConfigurationManager.AppSettings["guid"];

        public static JsonRoot GetJsonRootObject()
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

        public static Packet GetPacketObject()
        {
            Packet packet = new Packet();
            packet.packetType = 5;
            packet.payload = "";
            packet.useFilter = true;
            packet.isBase64Encoded = false;
            Balances balances = new Balances();
            Financials financials = new Financials();
            return packet;
        }

    }
}
