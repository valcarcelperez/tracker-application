using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackerApplication.Contracts;
using TrackerApplication.Contracts.Models;

namespace TrackerApplication.Client
{
    public class TrackerApiClientConfig
    {
        public string BaseAddress { get; set; }
        public TimeSpan Timeout { get; set; }
    }

    public class TrackerApiClient : IDisposable
    {
        private readonly HttpClient _client;
        private bool disposed = false;
        private readonly TrackerApiClientConfig _config;
        private JsonSerializerOptions _jsonSerializerOptions;

        public TrackerApiClient(TrackerApiClientConfig config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(config.BaseAddress)
            };

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        ~TrackerApiClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                }

                disposed = true;
            }
        }

        public async Task<ServiceResponse<TrackerData[]>> RetrieveAllAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/tracker/retrieve");            
            var response = await _client.SendAsync(request);            
            response.EnsureSuccessStatusCode();            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponse<TrackerData[]>>(content, _jsonSerializerOptions);
        }

        public Task<ServiceResponse> SaveTrackerData1Async(Contracts.Models.TrackerDataFormat1.TrackerData1 trackerData1)
        {
            return ExecuteQueryAsync<ServiceResponse, Contracts.Models.TrackerDataFormat1.TrackerData1>(trackerData1, "api/tracker/saveTrackerData1");
        }

        public Task<ServiceResponse> SaveTrackerData2Async(Contracts.Models.TrackerDataFormat2.TrackerData2 trackerData2)
        {
            return ExecuteQueryAsync<ServiceResponse, Contracts.Models.TrackerDataFormat2.TrackerData2>(trackerData2, "api/tracker/saveTrackerData2");
        }

        public Task<ServiceResponse> SaveTrackerData3Async(Contracts.Models.TrackerDataFormat3.TrackerData3 trackerData3)
        {
            return ExecuteQueryAsync<ServiceResponse, Contracts.Models.TrackerDataFormat3.TrackerData3>(trackerData3, "api/tracker/saveTrackerData3");
        }

        public async Task<TResponse> ExecuteQueryAsync<TResponse, TRequest>(TRequest request, string requestUri)
            where TResponse : ServiceResponse
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);
            SetRequestContent(httpRequest, request);

            var response = await _client.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(content, _jsonSerializerOptions);
        }

        private void SetRequestContent<TRequest>(HttpRequestMessage httpRequest, TRequest request)
        {
            if (request == null)
            {
                return;
            }

            string contentAsText = JsonSerializer.Serialize(request);
            httpRequest.Content = new StringContent(contentAsText, null, "application/json");
        }
    }
}
