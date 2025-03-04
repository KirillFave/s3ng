using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProductService.Domain;
using SharedLibrary.ProductService.Minio;

namespace ProductService.Infrastructure
{
    public class S3FileStorage : IFileStorage
    {
        private readonly IAmazonS3 _s3Client;
        private readonly S3Options _minioOptions;

        public S3FileStorage(IAmazonS3 s3Client, IOptions<S3Options> minioOptions)
        {
            _s3Client = s3Client;
            _minioOptions = minioOptions.Value;
        }

        public async Task DeleteFileAsync(string imageUrl)
        {
            // Парсим имя объекта из URL
            var uri = new Uri(imageUrl);
            var objectName = uri.AbsolutePath.TrimStart('/').Replace($"{_minioOptions.BucketName}/", "");
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _minioOptions.BucketName,
                Key = objectName
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var key = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using var stream = file.OpenReadStream();

            var putRequest = new PutObjectRequest
            {
                BucketName = _minioOptions.BucketName,
                Key = key,
                InputStream = stream
            };

            await _s3Client.PutObjectAsync(putRequest);

            return $"{_s3Client.Config.ServiceURL}/{_minioOptions.BucketName}/{key}";
        }
    }
}
