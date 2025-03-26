using System.Linq.Expressions;
using DeliveryService.Delivery.Domain.Entities.BaseTypes;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;

namespace DeliveryService.Delivery.Domain.Entities.Abstractions
{
    /// <summary>
    /// Описания общих методов для всех репозиториев.
    /// </summary>
    /// <typeparam name="T"> Тип Entity для репозитория. </typeparam>    
    public interface IRepository<T> where T : BaseEntity
    { 
        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>       
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking, Expression<Func<T, bool>>? filter = null, string? includes = null);

        /// <summary>
        /// возвращает последовательность элементов типа T, отфильтрованных по указанному предикату.
        /// </summary>        
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// возвращает 1 элемент типа T, отфильтрованных по указанному предикату.
        /// </summary>        
        Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>       
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken, string? includes = null);

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        void Update(T entity);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(T entity); 
    }
}
