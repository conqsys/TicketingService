using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.API.Common
{
    public class BaseClient
    {
        public string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3aWxsLnNtaXRoIiwianRpIjoiMGU4NmZkM2MtODgxMC00ZDY5LWIzMWEtNDU3YzliMDllOTRjIiwiaWF0IjoxNDg3MTkzODg1LCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoid2lsbC5zbWl0aCIsIklkIjoiMiIsIm5iZiI6MTQ4NzE5Mzg4NSwiZXhwIjoxNDg3MjA0MzM5LCJpc3MiOiJodHRwOi8vYWJjLmNvbSIsImF1ZCI6Ik1vbGVjdWxhckFwcCJ9.gCZI4XsBY5sVwHGKE30db8X0yNu7d1rWK6U-0yd_c7E";

        public AsyncRestClient GetClient(string defaultServiceTypeUrl)
        {
            UrlConfiguration configuration = new UrlConfiguration();
            string restUrl = configuration.GetAppUrl(defaultServiceTypeUrl);

            var client = new AsyncRestClient(restUrl);
            return client;
        }

        public RestRequest GetRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("Authorization", "Bearer " + token);
            return request;
        }
    }
}
