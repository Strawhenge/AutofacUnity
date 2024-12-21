using Autofac.Builder;
using UnityEngine;

namespace Autofac.Unity
{
    public static class Extensions
    {
        public static ILifetimeScope GetScope(this IComponentContext context) =>
            context.Resolve<ILifetimeScope>();

        public static bool IsRootScope(this IComponentContext context) =>
            context.GetScope().Tag is "root";

        public static GameObject GetGameObject(this IComponentContext context) =>
            context.Resolve<GameObject>();

        public static T ResolveFromGameObject<T>(this IComponentContext context) where T : Object =>
            context.GetGameObject().GetComponent<T>();

        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle>
            RegisterTypeFromGameObject<T>(this ContainerBuilder builder) where T : Object =>
            builder.Register(x => x.ResolveFromGameObject<T>());

        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterTypeFromScene<T>(
            this ContainerBuilder builder) where T : Object =>
            builder.Register(x => Object.FindObjectOfType<T>()).InstancePerLifetimeScope();
    }
}