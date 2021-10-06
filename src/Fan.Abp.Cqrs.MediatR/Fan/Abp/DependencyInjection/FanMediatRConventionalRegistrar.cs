using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.DependencyInjection;

namespace Fan.Abp.DependencyInjection
{
    /// <summary>
    /// https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection/blob/master/src/MediatR.Extensions.Microsoft.DependencyInjection/Registration/ServiceRegistrar.cs
    /// </summary>
    public class FanMediatRConventionalRegistrar : ConventionalRegistrarBase
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

            var commandHandlers = types.Where(type=> typeof(ICommandHandler).GetTypeInfo().IsAssignableFrom(type)).ToArray();
            ConnectImplementationsToTypesClosing(typeof(ICommandHandler<,>), services, commandHandlers, false);

            var queryHandlers = types.Where(type => typeof(IQueryHandler).GetTypeInfo().IsAssignableFrom(type)).ToArray();
            ConnectImplementationsToTypesClosing(typeof(IQueryHandler<,>), services, queryHandlers, false);
        }

        #region AddType

        public override void AddType(IServiceCollection services, Type type)
        {

        }

        public virtual void AddType(IServiceCollection services, Type serviceType, Type implementationType)
        {
            services.AddTransient(serviceType, implementationType);
        }

        public virtual void TryAddType(IServiceCollection services, Type serviceType, Type implementationType)
        {
            services.TryAddTransient(serviceType, implementationType);
        }

        #endregion

        /// <summary>
        /// Helper method use to differentiate behavior between request handlers and notification handlers.
        /// Request handlers should only be added once (so set addIfAlreadyExists to false)
        /// Notification handlers should all be added (set addIfAlreadyExists to true)
        /// </summary>
        /// <param name="openRequestInterface"></param>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <param name="addIfAlreadyExists"></param>
        private void ConnectImplementationsToTypesClosing(Type openRequestInterface, IServiceCollection services, Type[] types, bool addIfAlreadyExists)
        {
            var concretions = new List<Type>();
            var interfaces = new List<Type>();
            foreach (var type in types)
            {
                var interfaceTypes = type.FindInterfacesThatClose(openRequestInterface).ToArray();
                if (!interfaceTypes.Any()) continue;

                if (type.IsConcrete())
                {
                    concretions.Add(type);
                }

                foreach (var interfaceType in interfaceTypes)
                {
                    interfaces.AddIfNotContains(interfaceType);
                }
            }

            foreach (var @interface in interfaces)
            {
                var exactMatches = concretions.Where(x => x.CanBeCastTo(@interface)).ToList();
                if (addIfAlreadyExists)
                {
                    foreach (var type in exactMatches)
                    {
                        AddType(services, @interface, type);
                    }
                }
                else
                {
                    if (exactMatches.Count > 1)
                    {
                        exactMatches.RemoveAll(m => !IsMatchingWithInterface(m, @interface));
                    }

                    foreach (var type in exactMatches)
                    {
                        TryAddType(services, @interface, type);
                    }
                }

                if (!@interface.IsOpenGeneric())
                {
                    AddConcretionsThatCouldBeClosed(@interface, concretions, services);
                }
            }
        }

        private static bool IsMatchingWithInterface(Type handlerType, Type handlerInterface)
        {
            if (handlerType == null || handlerInterface == null)
            {
                return false;
            }

            if (handlerType.IsInterface)
            {
                if (handlerType.GenericTypeArguments.SequenceEqual(handlerInterface.GenericTypeArguments))
                {
                    return true;
                }
            }
            else
            {
                return IsMatchingWithInterface(handlerType.GetInterface(handlerInterface.Name), handlerInterface);
            }

            return false;
        }

        private void AddConcretionsThatCouldBeClosed(Type @interface, List<Type> concretions, IServiceCollection services)
        {
            foreach (var type in concretions
                .Where(x => x.IsOpenGeneric() && x.CouldCloseTo(@interface)))
            {
                try
                {
                    TryAddType(services, @interface, type.MakeGenericType(@interface.GenericTypeArguments));
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
