using System;
using System.Linq;
using System.Reflection;
using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Fan.Abp.DependencyInjection
{
    /// <summary>
    /// https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection/blob/master/src/MediatR.Extensions.Microsoft.DependencyInjection/Registration/ServiceRegistrar.cs
    /// </summary>
    public class MediatRCqrsConventionalRegistrar : MediatRConventionalRegistrar
    {
        public override void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            Type[] types = assembly.DefinedTypes
                .Where(
                    type => type is {IsClass: true, IsAbstract: false}
                )
                .Where(type =>
                    typeof(ICommandHandler).GetTypeInfo().IsAssignableFrom(type) ||
                    typeof(IQueryHandler).GetTypeInfo().IsAssignableFrom(type)
                )
                .ToArray();

            if (types.Any())
            {
                AddTypes(services, types);
            }
        }

        public override void AddTypes(IServiceCollection services, params Type[] types)
        {
            types = types.Where(t => !t.IsOpenGeneric()).ToArray();

            var commandHandlers = types.Where(type => typeof(ICommandHandler).GetTypeInfo().IsAssignableFrom(type)).ToArray();
            ConnectImplementationsToTypesClosing(typeof(ICommandHandler<,>), services, commandHandlers, false);

            var queryHandlers = types.Where(type => typeof(IQueryHandler).GetTypeInfo().IsAssignableFrom(type))
                .ToArray();
            ConnectImplementationsToTypesClosing(typeof(IQueryHandler<,>), services, queryHandlers, false);
        }
    }
}
