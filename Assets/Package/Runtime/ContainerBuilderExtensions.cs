using Autofac.Builder;
using UnityEngine;

namespace Autofac.Unity
{
    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle>
            RegisterTypeFromGameObject<T>(this ContainerBuilder builder) where T : Object =>
            builder.Register(x => x.ResolveFromGameObject<T>());

        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterTypeFromScene<T>(
            this ContainerBuilder builder) where T : Object =>
            builder.Register(_ => Object.FindObjectOfType<T>()).InstancePerLifetimeScope();
    }
}