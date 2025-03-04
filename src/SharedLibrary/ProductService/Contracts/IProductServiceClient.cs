// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Refit;
using SharedLibrary.ProductService.Dto;

namespace SharedLibrary.ProductService.Contracts
{
    public interface IProductServiceClient
    {
        [Get("/api/products")]
        Task<List<ProductResponseDto>> GetAllAsync();

        [Get("/api/products/{id}")]
        Task<ProductResponseDto> GetByIdAsync(string id);

        [Post("/api/products")]
        [Multipart] // для файлов
        Task<string> CreateAsync([AliasAs("Name")] string name,
                                  [AliasAs("Description")] string description,
                                  [AliasAs("Price")] decimal price,
                                  [AliasAs("Image")] StreamPart? image);

        [Put("/api/products/{id}")]
        [Multipart]
        Task UpdateAsync(string id,
                         [AliasAs("Name")] string name,
                         [AliasAs("Description")] string description,
                         [AliasAs("Price")] decimal price,
                         [AliasAs("Image")] StreamPart? image);

        [Delete("/api/products/{id}")]
        Task DeleteAsync(string id);
    }
}
