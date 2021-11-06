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
            PresignedGenerator presignedGenerator= new PresignedGenerator();
            var amazons3Client = new AmazonS3Client(Amazon.RegionEndpoint.APSouth1);
            var uri = presignedGenerator.GenerateUri(amazons3Client, 30, "upload1234567890", Guid.NewGuid().ToString());
            Assert.NotNull(uri);
        }
    }
}
