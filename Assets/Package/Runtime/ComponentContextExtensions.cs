using UnityEngine;

namespace Autofac.Unity
{
    public static class ComponentContextExtensions
    {
        public static ILifetimeScope GetScope(this IComponentContext context) =>
            context.Resolve<ILifetimeScope>();

        public static bool IsRootScope(this IComponentContext context) =>
            context.GetScope().Tag is "root";

        public static GameObject GetGameObject(this IComponentContext context) =>
            context.Resolve<GameObject>();

        public static T ResolveFromGameObject<T>(this IComponentContext context) where T : Object =>
            context.GetGameObject().GetComponent<T>();
    }
}