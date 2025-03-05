namespace ProductService.Domain
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ИД
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Урл картинки товара
        /// </summary>
        public string? ImageUrl { get; set; }
    }
}
