using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public class PresignedGenerator
    {
        /// <summary>
        /// Get the Presigned URL for the amazon Bucket name
        /// </summary>
        /// <param name="amazonS3Client">S3 client.</param>
        /// <param name="duration">Duration.</param>
        /// <param name="bucketName">Bucket Name.</param>
        /// <param name="objectKey">Key which is unique for the bucket.</param>
        /// <returns>URL of the Presigned Url.</returns>
        public Uri GenerateUri(AmazonS3Client amazonS3Client,int duration,string bucketName,string objectKey)
        {
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
