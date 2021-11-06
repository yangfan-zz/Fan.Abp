using DomainCqrs.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DomainCqrs.Permissions
{
    public class DomainCqrsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(DomainCqrsPermissions.GroupName, L("Permission:DomainCqrs"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DomainCqrsResource>(name);
        }
    }
}