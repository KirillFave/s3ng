using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProductService.Domain;
using SharedLibrary.ProductService.Options;

namespace ProductService.Infrastructure
{
    public class S3FileStorage : IFileStorage
    {
        private readonly IAmazonS3 _s3Client;
        private readonly S3Options _s3Options;

        public S3FileStorage(IAmazonS3 s3Client, IOptions<S3Options> s3Options)
        {
            _s3Client = s3Client;
            _s3Options = s3Options.Value;
        }

        public async Task DeleteFileAsync(string imageUrl, CancellationToken ct = default)
        {
            // Парсим имя объекта из URL
            var uri = new Uri(imageUrl);
            var objectName = uri.AbsolutePath.TrimStart('/').Replace($"{_s3Options.BucketName}/", "");
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _s3Options.BucketName,
                Key = objectName
            };

            await _s3Client.DeleteObjectAsync(deleteRequest, ct);
        }

        public async Task<string> UploadFileAsync(IFormFile file, CancellationToken ct = default)
        {
            var key = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using var stream = file.OpenReadStream();

            var putRequest = new PutObjectRequest
            {
                BucketName = _s3Options.BucketName,
                Key = key,
                InputStream = stream
            };

            await _s3Client.PutObjectAsync(putRequest, ct);

            return $"{_s3Client.Config.ServiceURL}/{_s3Options.BucketName}/{key}";
        }
    }
}
