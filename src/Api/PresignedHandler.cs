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
        private const int Duration = 12;

        public APIGatewayHttpApiV2ProxyResponse Handler(APIGatewayHttpApiV2ProxyRequest request)
        {
            var bucketName = Environment.GetEnvironmentVariable("BUCKET");
            var region = Environment.GetEnvironmentVariable("REGION");
            PresignedUrlBuilder urlBuilder = new PresignedUrlBuilder(bucketName, RegionEndpoint.GetBySystemName(region));
            if (!request.QueryStringParameters.ContainsKey("mail"))
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
            // Hash the Mail ID used to generate the presigned URL.
            var mailId = request.QueryStringParameters["mail"];
            var hash = Hash(mailId);
            var url = urlBuilder.GetPreSignedURL(hash, Duration);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = url.OriginalString,
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }

        private string Hash(string mailId)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(mailId.Trim().ToLower());
            byte[] hash = md5.ComputeHash(inputBytes);

            // Create lower-case hex string
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
