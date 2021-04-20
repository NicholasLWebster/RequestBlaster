using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using RequestBlaster.Models;

namespace RequestBlaster
{
    public class RequestSenderClient
    {
        private readonly HttpClient _httpClient;

        public RequestSenderClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<(IEnumerable<(HttpResponseMessage responseMessage, TimeSpan requestResponseTime)>, TimeSpan totalResponseTime)> SendRequestsAsync(RequestConfiguration requestConfig)
        {
            switch (requestConfig.SendMethod)
            {
                case SendMethod.Sequential:
                    return await SendRequestsSequentiallyAsync();
                case SendMethod.Concurrent:
                    return await SendRequestsConcurrentlyAsync(requestConfig);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<(IEnumerable<(HttpResponseMessage responseMessage, TimeSpan requestResponseTime)>, TimeSpan totalResponseTime)> SendRequestsSequentiallyAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<(IEnumerable<(HttpResponseMessage responseMessage, TimeSpan requestResponseTime)>, TimeSpan totalResponseTime)> SendRequestsConcurrentlyAsync(RequestConfiguration config)
        {
            var requestCount = int.Parse(config.RequestCount);
            var concurrentRequestTasks = new Task<(HttpResponseMessage responseMessage, TimeSpan responseTime)>[requestCount];

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < requestCount; i++)
            {
                concurrentRequestTasks[i] = SendRequestAndTimeResponseAsync(config.IndividualRequestMessage);
            }
            
            var results = await Task.WhenAll(concurrentRequestTasks);
            stopwatch.Stop();

            var ultimateResponse = (results, stopwatch.Elapsed);
            
            return ultimateResponse;
        }

        private async Task<(HttpResponseMessage responseMessage, TimeSpan responseTime)> SendRequestAndTimeResponseAsync(HttpRequestMessage requestMessage)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = await _httpClient.SendAsync(requestMessage);
            stopwatch.Stop();
            
            return (response, stopwatch.Elapsed);
        }
    }
}
