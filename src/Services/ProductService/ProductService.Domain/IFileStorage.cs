using Microsoft.AspNetCore.Http;

namespace ProductService.Domain
{
    /// <summary>
    /// Работа с файловым хранилищем
    /// </summary>
    public interface IFileStorage
    {
        /// <summary>
        /// Загрузить
        /// </summary>
        /// <param name="file">Файл</param>
        /// <returns>Путь к файлу</returns>
        Task<string> UploadFileAsync(IFormFile file);

        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <param name="imageUrl">Путь к файлу</param>
        Task DeleteFileAsync(string imageUrl);
    }
}
