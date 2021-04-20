using System;
using System.Net.Http;

namespace RequestBlaster.Models
{
    public enum SendMethod
    {
        Sequential,
        Concurrent
    }

    public class RequestConfiguration
    {
        public HttpMethod RequestType => HttpMethod.Get; // TODO: Allow the user to pick this in the UI.
        public string RequestEndpointUrl { get; set; }
        public string RequestBodyJson { get; set; }
        public string RequestCount { get; set; }
        public SendMethod SendMethod => SendMethod.Concurrent;


        public HttpRequestMessage IndividualRequestMessage => RequestType == HttpMethod.Get ?
            new HttpRequestMessage
        {
            RequestUri = new Uri(RequestEndpointUrl),
            Method = RequestType
        }
            : new HttpRequestMessage
        {
            RequestUri = new Uri(RequestEndpointUrl),
            Method = RequestType,
            Content = new StringContent(RequestBodyJson)
        };
    }
}
