namespace IAM.Mappers
{
    /// <summary>
    /// Маппер ролей
    /// </summary>
    public static class RoleMapper
    {
        /// <summary>
        /// Смаппить контрактную proto роль
        /// </summary>
        /// <param name="roleType">proto роль</param>
        /// <returns>Роль бизнес области</returns>
        /// <exception cref="ArgumentOutOfRangeException">При маппинге неизвестной роли</exception>
        public static SharedLibrary.IAM.Enums.RoleType Map(s3ng.Contracts.IAM.RoleType roleType)
        {
            return roleType switch
            {
                s3ng.Contracts.IAM.RoleType.User => SharedLibrary.IAM.Enums.RoleType.User,
                s3ng.Contracts.IAM.RoleType.Admin => SharedLibrary.IAM.Enums.RoleType.Admin,
                _ => throw new ArgumentOutOfRangeException(nameof(roleType)),
            };
        }
    }
}
