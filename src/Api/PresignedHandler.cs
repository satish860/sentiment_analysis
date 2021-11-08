using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Amazon;
using Amazon.Lambda.APIGatewayEvents;

namespace Api
{
    public class PresignedHandler
    {
        public APIGatewayHttpApiV2ProxyResponse Handler(APIGatewayHttpApiV2ProxyRequest request)
        {
            var bucketName = Environment.GetEnvironmentVariable("BUCKET");
            var region = Environment.GetEnvironmentVariable("REGION");
            PresignedUrlBuilder urlBuilder = new PresignedUrlBuilder(bucketName, RegionEndpoint.GetBySystemName(region));
            if(!request.QueryStringParameters.ContainsKey("mail"))
            {
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "mail parameter is required",
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "text/plain" }
                    }
                };
            }
            var url = urlBuilder.GetPreSignedURL(request.QueryStringParameters["mail"], 5);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = url.OriginalString,
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }

    }
}
