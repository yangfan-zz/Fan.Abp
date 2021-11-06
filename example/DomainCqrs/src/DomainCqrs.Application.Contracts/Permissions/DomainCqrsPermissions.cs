using Volo.Abp.Reflection;

namespace DomainCqrs.Permissions
{
    public class DomainCqrsPermissions
    {
        public const string GroupName = "DomainCqrs";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(DomainCqrsPermissions));
        }
    }
}