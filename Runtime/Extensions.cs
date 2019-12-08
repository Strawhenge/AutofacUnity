using Autofac.Builder;
using Autofac.Core.Resolving;
using UnityEngine;

namespace Autofac.Unity
{
    public static class Extensions
    {
        public static ILifetimeScope GetScope(this IComponentContext context) =>
            (context as IInstanceLookup)?.ActivationScope;

        public static GameObject GetGameObject(this IComponentContext context) =>
            context.GetScope()?.Tag as GameObject;

        public static T ResolveFromGameObject<T>(this IComponentContext context) where T : Object =>
            context.GetGameObject()?.GetComponent<T>();

        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterTypeFromGameObject<T>(this ContainerBuilder builder) where T : Object =>
            builder.Register(x => x.ResolveFromGameObject<T>());
    }
}
