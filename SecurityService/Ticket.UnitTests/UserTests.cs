using Ticket.API.Common;
using Ticket.DataAccess.Common;
using Ticket.DataAccess.Security;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Ticket.UnitTests
{
    public class UserTests
    {
        BaseClient baseClient = null;
        AsyncRestClient client = null;

        private string apiUrl = "/api/user";
        private string resource = "";
        private long id = 0;
        
        public UserTests()
        {
            baseClient = new BaseClient();
            client = baseClient.GetClient(ServiceType.Security);
        }

        private dynamic sampleUserObject = new
        {
            id = 0,
            version = 0,
            accountExpired = true,
            accountLocked = true,
            authAttempts = 0,
            contact = "",
            email = "a@a.com",
            enabled = true,
            firstName = "manoj@GI",
            labId = 0,
            labclientId = 0,
            lastName = "Tane",
            password = "test",
            passwordExpired = false,
            userType = "some_user_type",
            username = "conji1",
            roleIds = new int[] { 14 }
        };

        private dynamic sampleUserObjectForValidation = new
        {
            id = 0,
            contact = "",
            email = "a@a.com",
            enabled = true,
            labId = 0,
            labclientId = 0,
            userType = "some_user_type",
            username = "conji1",
            roleIds = new int[] { 14 }
        };

        /// <summary>
        //// this should always pass if UserName is unique  
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostUser()
        {
            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(sampleUserObject);

            var result = await client.ExecuteAsync<User>(request);
            
            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            Assert.NotNull(result.Data);
            Assert.True(sampleUserObject.username == result.Data.Email);
            Assert.True(result.Data.Id > 0);

            id = result.Data.Id;
        }

        /// <summary>
        //// this should atleast return validation error when user name is already exists.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostUserWithValidationError()
        {
            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(sampleUserObjectForValidation);

            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.ErrorCode == 1012;
            });
        }

        /// <summary>
        //// this should return required validation error
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostUserWithRequiredValidationError()
        {
            var sampleUserObject = new
            {
                id = 0,
                version = 0,
                accountExpired = true,
                accountLocked = true,
                authAttempts = 0,
                contact = "",
                email = "a@a.com",
                enabled = true,               
                labId = 0,
                labclientId = 0,                                
                passwordExpired = false,
                userType = "some_user_type",                
                roleIds = new int[] { 14 }
            };
            
            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(sampleUserObject);

            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.MemberNames.Contains("FirstName");
            });
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.MemberNames.Contains("LastName");
            });
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.MemberNames.Contains("Username");
            });
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.MemberNames.Contains("Password");
            });
        }

        /// <summary>
        /// /this should return the single User
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<User> GetUserById()
        {
            resource = apiUrl + "/" + id;

            RestRequest request = baseClient.GetRequest(resource, Method.GET);
            var result = await client.ExecuteAsync<User>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Id == id);

            return result.Data;
        }

        /// <summary>
        //// this should always update the record with specific userid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PutUser()
        {
            var user = await GetUserById();
            user.Email = "manoj@conqsys.com";

            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.PUT);
            request.AddJsonBody(user);

            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            Assert.NotNull(result.Data);
            Assert.True(result.Data == true);
        }

        /// <summary>
        /// / this should return the all users for authenticated user labid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllUsers()
        {
            resource = apiUrl + "/list";

            RestRequest request = baseClient.GetRequest(resource, Method.GET);
            
            var result = await client.ExecuteAsync<List<User>>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Count > 0);
        }

        [Fact]
        public async Task ConnectionTest()
        {
            resource = apiUrl + "/list";

            RestRequest request = baseClient.GetRequest(resource, Method.GET);

            for (int i = 0; i < 200; i++)
            {
                var result = await client.ExecuteAsync<List<User>>(request);
                Console.WriteLine("==================================================================================================================================================");
                Console.Write(result.ActualContent);
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------");
            }

            
        }


        /// <summary>
        /// / this should always pass
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DisableUser()
        {
            resource = apiUrl + "/" + id + "/disable";

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
        }

        /// <summary>
        /// / this should always pass
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EnableUser()
        {
            resource = apiUrl + "/" + id + "/enable";

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
        }


        /// <summary>
        /// / this should always pass
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ChangePassword()
        {
            resource = apiUrl + "/" + id + "/changepassword/pwd456";

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
        }

        /// <summary>
        //// this should atleast return validation error when user send the prevoius password.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ChangePasswordWithValidationError()
        {
            resource = apiUrl + "/" + id + "/changepassword/pwd456";

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.ErrorCode == 1008;
            });
        }

        /// <summary>
        /// / this should always pass
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task LogOut()
        {
            resource = apiUrl + "/logout";
            
            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            
            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
        }
    }
}
