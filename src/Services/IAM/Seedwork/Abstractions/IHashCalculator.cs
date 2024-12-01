namespace IAM.Seedwork.Abstractions
{
    /// <summary>
    /// Вычислятор хэшей
    /// </summary>
    internal interface IHashCalculator
    {
        /// <summary>
        /// Рассчитать хэш
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns>Хэш</returns>
        internal string Compute(string value);
    }
}
