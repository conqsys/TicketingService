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
    public class LabTests
    {
        BaseClient baseClient = null;
        AsyncRestClient client = null;

        private string apiUrl = "/api/lab";
        private string resource = "";
        private long id = 0;

        public LabTests()
        {
            baseClient = new BaseClient();
            client = baseClient.GetClient(ServiceType.Security);
        }

        private dynamic lab = new
        {

            id = 0,
            version = 0,
            address1 = "noida",
            address2 = "noda",
            city = "noida",
            country = "india",
            emailpin = "201301",
            labDesc = "Cqs Testing Lab123",
            labName = "A6",
            state = "UP",
            zip = "201301",
            clientPortal = true,
            locked = true,
            noOfUsers = 18,
            webservices = true,
            noOfClients = 12,
            emailReport = true,
            faxReport = true,
            isPaid = true,
            areaCode = "201301",
            faxNumberforReport = "fax",
            isDeleted = true,
            specimenLabelType = "great",
            createdBy = 0,
            createdDate = "2016-12-30T12:23:46.779Z",
            initialLabAdmin = new
            {
                id = 0,
                version = 0,
                accountExpired = true,
                accountLocked = true,
                authAttempts = 0,
                contact = "cqs lab admin",
                createdBy = "admin",
                createdDate = "2016-12-30T12:23:46.779Z",
                email = "cqsadmin@conqsys.com",
                enabled = true,
                firstName = "Cqs Admin 123",
                labId = 0,
                labclientId = 0,
                lastName = "Taneja",
                password = "conqsys@1",
                passwordExpired = false,
                rolesId = 0,
                userType = "Admin",
                username = "A6",
                datePassword = "2016-12-30T12:23:46.781Z",
                roleIds = new int[] { 14 }
            }
            ,
            labContact = new
            {
                id = 0,
                version = 0,
                contactPersonEmail = "email",
                contactPersonFax = "fax",
                contactPersonPhone = "phone",
                labId = 0,
                contactName = "labcontact"
            }
        };

        /// <summary>
        /// this should always pass if LabName is unique  and Initial Lab Admin is also unique
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostLab()
        {
            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(lab);

            var result = await client.ExecuteAsync<Lab>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            Assert.NotNull(result.Data);
            Assert.True(result.Data.LabName == lab.labName);
            Assert.True(result.Data.Id > 0);

            id = result.Data.Id;
        }


        /// <summary>
        /// this should atleast return validation error Lab already exists
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostLabWithValidationError()
        {
            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(lab);

            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.ErrorCode == 1001;
            });
        }

        /// <summary>
        /// this should return required validation 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostLabWithRequiredValidationError()
        {
            var lab = new
            {
                id = 0,
                version = 0,
                address1 = "noida",
                address2 = "noda",
                city = "noida",
                country = "india",
                emailpin = "201301",
                labDesc = "Cqs Testing Lab123",                
                state = "UP",
                zip = "201301",
                clientPortal = true,
                locked = true,
                noOfUsers = 18,
                webservices = true,
                noOfClients = 12,
                emailReport = true,
                faxReport = true,
                isPaid = true,
                areaCode = "201301",
                faxNumberforReport = "fax",
                isDeleted = true,
                specimenLabelType = "great",
                createdBy = 0,
                createdDate = "2016-12-30T12:23:46.779Z",
                initialLabAdmin = new
                {
                    id = 0,
                    version = 0,
                    accountExpired = true,
                    accountLocked = true,
                    authAttempts = 0,
                    contact = "cqs lab admin",
                    createdBy = "admin",
                    createdDate = "2016-12-30T12:23:46.779Z",
                    email = "cqsadmin@conqsys.com",
                    enabled = true,
                    firstName = "Cqs Admin 123",
                    labId = 0,
                    labclientId = 0,
                    lastName = "Taneja",
                    password = "conqsys@1",
                    passwordExpired = false,
                    rolesId = 0,
                    userType = "Admin",
                    username = "NZ",
                    datePassword = "2016-12-30T12:23:46.781Z",
                    roleIds = new int[] { 14 }
                }
            ,
                labContact = new
                {
                    id = 0,
                    version = 0,
                    contactPersonEmail = "email",
                    contactPersonFax = "fax",
                    contactPersonPhone = "phone",
                    labId = 0,
                    contactName = "labcontact"
                }
            };

            resource = apiUrl;
            
            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(lab);

            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.MemberNames.Contains("LabName");
            });
        }

        /// <summary>
        /// this should atleast return validation error when initial lab admin user name is already exist.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostLabWithAdminSameUserNameExistValidationError()
        {
            var lab = new
            {
                id = 0,
                version = 0,
                address1 = "noida",
                address2 = "noda",
                city = "noida",
                country = "india",
                emailpin = "201301",
                labDesc = "Cqs Testing Lab123",
                labName = "cqs_0005",
                state = "UP",
                zip = "201301",
                clientPortal = true,
                locked = true,
                noOfUsers = 18,
                webservices = true,
                noOfClients = 12,
                emailReport = true,
                faxReport = true,
                isPaid = true,
                areaCode = "201301",
                faxNumberforReport = "fax",
                isDeleted = true,
                specimenLabelType = "great",
                createdBy = 0,
                createdDate = "2016-12-30T12:23:46.779Z"
                ,
                initialLabAdmin = new
                {
                    id = 0,
                    version = 0,
                    accountExpired = true,
                    accountLocked = true,
                    authAttempts = 0,
                    contact = "cqs lab admin",
                    createdBy = "admin",
                    createdDate = "2016-12-30T12:23:46.779Z",
                    email = "cqsadmin@conqsys.com",
                    enabled = true,
                    firstName = "Cqs Admin 123",
                    labId = 0,
                    labclientId = 0,
                    lastName = "Taneja",
                    password = "conqsys@1",
                    passwordExpired = false,
                    rolesId = 0,
                    userType = "Admin",
                    username = "cqsnoida123",
                    datePassword = "2016-12-30T12:23:46.781Z",
                    roleIds = new int[] { 14 }
                }
            };

            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.POST);
            request.AddJsonBody(lab);

            var result = await client.ExecuteAsync<SerializableEntityValidationCodeResult>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.ValidationErrors);
            Assert.Contains(result.Data.ValidationErrors, (error) =>
            {
                return error.ErrorCode == 1001;
            });
        }

        /// <summary>
        //// this should always update the record with specific labid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PutLab()
        {
            var lab = await GetLabById();
            lab.Country = "India";

            resource = apiUrl;

            RestRequest request = baseClient.GetRequest(resource, Method.PUT);
            request.AddJsonBody(lab);

            var result = await client.ExecuteAsync<bool>(request);

            Assert.NotNull(result);
            Assert.Matches(result.Response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            Assert.NotNull(result.Data);
            Assert.True(result.Data == true);

        }

        /// <summary>
        /// /this should return the single Labprovider
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<Lab> GetLabById()
        {
            resource = apiUrl + "/" + id;

            RestRequest request = baseClient.GetRequest(resource, Method.GET);
            
            var result = await client.ExecuteAsync<Lab>(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Id == id);

            return result.Data;
        }

    }
}
