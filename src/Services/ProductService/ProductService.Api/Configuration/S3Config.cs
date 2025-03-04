using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using SharedLibrary.ProductService.Options;

namespace ProductService.Api.Configuration
{
    public static class S3Config
    {
        public static void ConfigureS3(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var awsOptions = configuration.GetSection(S3Options.S3).Get<S3Options>();
            if (awsOptions is null)
                throw new ArgumentNullException(nameof(awsOptions));

            serviceCollection.AddSingleton<IAmazonS3>(sp =>
            {
                var config = new AmazonS3Config
                {
                    ServiceURL = awsOptions.ServiceUrl,
                    ForcePathStyle = true //minio spec
                };
                var credentials = new BasicAWSCredentials(awsOptions.AccessKey, awsOptions.SecretKey);
                return new AmazonS3Client(credentials, config);
            });

            // сразу после регистрации клиента можно создать бакет (по желанию)
            using var scope = serviceCollection.BuildServiceProvider().CreateScope();
            var s3Client = scope.ServiceProvider.GetRequiredService<IAmazonS3>();

            EnsureBucketExists(s3Client, awsOptions.BucketName).GetAwaiter().GetResult();
        }

        private static async Task EnsureBucketExists(IAmazonS3 s3Client, string bucketName)
        {
            var buckets = await s3Client.ListBucketsAsync();

            if (buckets.Buckets.All(b => b.BucketName != bucketName))
            {
                await s3Client.PutBucketAsync(new PutBucketRequest
                {
                    BucketName = bucketName
                });
            }
        }
    }
}
