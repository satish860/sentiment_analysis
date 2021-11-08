using Amazon.S3;
using Api;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace API.Test
{
    public class PresignedS3Tests
    {
        [Fact]
        public void Should_be_able_to_Generate_Presigned_url()
        {
            PresignedUrlBuilder builder = new PresignedUrlBuilder("upload1234567890",Amazon.RegionEndpoint.APSouth1);
            var uri = builder.GetPreSignedURL(Guid.NewGuid().ToString(),30);
            Assert.NotNull(uri);
        }
    }
}
