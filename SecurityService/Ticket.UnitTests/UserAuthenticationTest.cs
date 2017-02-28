using Ticket.API.Common;
using Ticket.DataAccess.Common;
using Ticket.DataAccess.Security;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ticket.UnitTests
{
    public class UserAuthenticationTests
    {
        BaseClient baseClient = null;
        AsyncRestClient client = null;

        private string apiUrl = "/api/userauthentication";
        private string resource = "";
        private long id = 0;

        public UserAuthenticationTests()
        {
            baseClient = new BaseClient();
            client = baseClient.GetClient(ServiceType.Security);
        }

        /// <summary>
        //// this should return required validation error
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserAuthenticationWithValidationError()
        {
            resource = apiUrl + "/10/10.10.1.6/maonj@cqs.com/1245636";

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (e) =>
            {
                return e.ErrorCode == 1017;
            });
        }

        /// <summary>
        //// this should atleast return validation error when user Authentication already exists
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserAuthenticationWithRequiredValidationError()
        {
            resource = apiUrl + "/10/10.10.1.6/maonj@cqs.com/1245636";

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (e) =>
            {
                return e.ErrorCode == 1017;
            });
        }


        /// <summary>
        /// this should always pass if User Authentication does not exist
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserAuthentication()
        {
            resource = apiUrl + "/56/10.10.1.5/manoj_0005/1245636";

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Data == true);
        }

        /// <summary>
        /// this should always pass if User Verification Code is correct and verification time does not expire
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task VerifyAuthentication()
        {
            resource = apiUrl + "/56/10.10.1.5/b3ijk303";
            
            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Data == true);
        }

        /// <summary>
        /// this should always return validation error if User Verification Code is not correct and verification time has expired
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task VerifyAuthenticationWithValidationError()
        {
            resource = apiUrl + "/56/10.10.1.6/5k48xyzibb7";
            
            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (e) =>
            {
                return e.ErrorCode == 1017;
            });
        }



    }
}
