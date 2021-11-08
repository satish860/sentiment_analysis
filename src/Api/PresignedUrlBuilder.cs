using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public class PresignedUrlBuilder
    {
        private string bucketName;
        private RegionEndpoint region;

        public PresignedUrlBuilder(string bucketName, RegionEndpoint region)
        {
            this.bucketName = bucketName;
            this.region = region;
        }

        /// <summary>
        /// Get the Presigned URL for the amazon Bucket name
        /// </summary>
        /// <param name="amazonS3Client">S3 client.</param>
        /// <param name="duration">Duration.</param>
        /// <param name="bucketName">Bucket Name.</param>
        /// <param name="objectKey">Key which is unique for the bucket.</param>
        /// <returns>URL of the Presigned Url.</returns>
        public Uri GetPreSignedURL(string objectKey,int duration)
        {
            AmazonS3Client amazonS3Client = new AmazonS3Client(region);
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                Expires = DateTime.UtcNow.AddHours(duration)
            };
            var uri = amazonS3Client.GetPreSignedURL(request);
            return new Uri(uri);
        }
    }
}
