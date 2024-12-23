namespace s3ng.ProductService.Core.Abstractions
{
    /// <summary>
    /// Интерфейс сущности с идентификатором.
    /// </summary>
    /// <typeparam name="IdType"> Тип идентификатора. </typeparam>
    public interface IEntity<IdType>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        IdType Id { get; }
    }
}