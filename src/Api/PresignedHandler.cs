using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Amazon.Lambda.APIGatewayEvents;

namespace Api
{
    public class PresignedHandler
    {
        public APIGatewayHttpApiV2ProxyResponse Handler(APIGatewayHttpApiV2ProxyRequest request)
        {
            var bucketName = Environment.GetEnvironmentVariable("BUCKET");
            var region = Environment.GetEnvironmentVariable("REGION");
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Hello, World! Your request was received at  region {region} and for the bucket {bucketName}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }

    }
}
