using Ticket.API.Common;
using Ticket.DataAccess.Common;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ticket.UnitTests
{
    public class LoginTests
    {
        BaseClient baseClient = null;
        AsyncRestClient client = null;

        private string tokenUrl = "/token";
        
        public LoginTests()
        {
            baseClient = new BaseClient();
            client = baseClient.GetClient(ServiceType.Security);
        }

        /// <summary>
        /// / this should always pass if user set the correct username, password and ipaddress
        /// </summary>
        /// <returns></returns>
        [Fact]
        public  async Task LoginTest()
        {
            var request = new RestRequest(tokenUrl, Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("username", "will.smith");
            request.AddParameter("password", "abc@12345");
            request.AddParameter("ipAddress", "192.168.1.1");
            
            var result = await client.ExecuteAsync<dynamic>(request);
            
            Assert.NotNull(result.Data);
            Assert.NotEmpty((result.Data as dynamic).access_token.Value);
        }

        /// <summary>
        /// // this should atleast return validation error when user does not set the correct username, password and ipaddress
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LoginTestWithValidationErrror()
        {
            var request = new RestRequest(tokenUrl, Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("username", "rahul");
            request.AddParameter("password", "test");
            request.AddParameter("ipAddress", "123.12.14.15");

            var result=await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);
            
            Assert.Null(result.Data);
        }
    }


   
}
