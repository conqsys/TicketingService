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
    public class RoleTests
    {
        BaseClient baseClient = null;
        AsyncRestClient client = null;

        private string apiUrl = "/api/role";
        private string rolePermissionUrl = "/api/rolepermission";
        private string resource = "";
        private long id = 0;
        private long rolePermissionId = 0;

        public RoleTests()
        {
            baseClient = new BaseClient();
            client = baseClient.GetClient(ServiceType.Security);
        }

        /// <summary>
        /// this should always pass if description is unique
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostRole()
        {
            var postObj = new
            {
                id = 0,
                version = 0,
                authority = "string",
                description = "string",
                labId = 0,
                roleType = "string",
                createdBy = 0
            };

            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(postObj);

            var result = await client.ExecuteAsync<Role>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            Assert.NotNull(result.Data);
           
            Assert.True(result.Data.Id > 0);

            id = result.Data.Id;
        }

        /// <summary>
        /// this should atleast return validation error description already exists
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostRoleWithValidationError()
        {
            var postObj = new
            {
                id = 0,
                version = 0,
                authority = "string",
                description = "string",
                labId = 0,
                roleType = "string",
                createdBy = 0
            };

            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(postObj);

            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.ErrorCode == 1000;
            });

        }

        /// <summary>
        /// this should return required validation error
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostRoleWithRequiredValidationError()
        {
            var role = new
            {
                id = 0,
                version = 0,                                
                labId = 0,                
                createdBy = 0
            };

            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(role);

            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.MemberNames.Contains("Authority");
            });
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.MemberNames.Contains("Description");
            });
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.MemberNames.Contains("RoleType");
            });
        }

        /// <summary>
        /// /this should return the all roles for authenticated user labid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllRole()
        {
            resource = apiUrl + "/list";

            RestRequest request = baseClient.GetRequest(resource, Method.GET);
            
            var result = await client.ExecuteAsync<List<Role>>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count > 0);
        }

        /// <summary>
        /// / this should always pass if permission does not exist with given roleid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostRolePermission()
        {
            var rolePermission = new
            {
                id = 0,
                roleID = id,
                permissionID = 1,
                createdBy = 2,
                createdDate = "2016-12-30T12:23:46.790Z"
            };

            resource = rolePermissionUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(rolePermission);

            var result = await client.ExecuteAsync<RolePermission>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            Assert.NotNull(result.Data);
            Assert.True(rolePermission.permissionID == result.Data.PermissionID);
            Assert.True(rolePermission.roleID == result.Data.RoleID);
            Assert.True(result.Data.Id > 0);

            rolePermissionId = result.Data.Id;
        }

        /// <summary>
        /// / this should always pass 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PutRolePermission()
        {
            var rolePermission = new
            {
                id = rolePermissionId,
                roleID = id,
                permissionID = 3
            };

            resource = rolePermissionUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.PUT);
            request.AddJsonBody(rolePermission);

            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data == true);
        }

        /// <summary>
        ///// this should atleast return validation error when permissionid does not exit.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostRolePermissionWithValidationError()
        {
            var rolePermission = new
            {
                roleID = id,
                permissionID = 11,
                createdBy = 2,
                createdDate = "2016-12-30T12:23:46.790Z"
            };

            resource = rolePermissionUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(rolePermission);

            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (e) =>
            {
                return e.ErrorCode == 1018;
            });
        }

        /// <summary>
        /// /// this should return the all permissions for given roleid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllRolesPermission()
        {
            resource = rolePermissionUrl + "/list";

            RestRequest request = baseClient.GetRequest(resource, Method.GET);
            
            var result = await client.ExecuteAsync<List<RolePermission>>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Count > 0);
        }


        /// <summary>
        /// /this should return the single Labprovider
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<Role> GetRoleById()
        {
            resource = apiUrl + "/" + id;
            
            RestRequest request = baseClient.GetRequest(resource, Method.GET);
            
            var result = await client.ExecuteAsync<Role>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Id == id);

            return result.Data;
        }

        /// <summary>
        //// this should always update the record with specific roleid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PutRole()
        {
            var role = await GetRoleById();
            
            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.PUT);
            request.AddJsonBody(role);

            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            Assert.NotNull(result.Data);
            Assert.True(result.Data == true);
        }
    }
}
