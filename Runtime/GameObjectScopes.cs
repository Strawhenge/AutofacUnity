using System;
using UnityEngine;

namespace Autofac.Unity
{
    internal static class GameObjectScopes
    {
        private static IContainer container;

        public static void SetContainer(IContainer container) => GameObjectScopes.container = container;

        public static ILifetimeScope Create(GameObject gameObject) =>
           Create(gameObject, _ => { });

        public static ILifetimeScope Create(GameObject gameObject, Action<ContainerBuilder> configurationAction)
        {
            AssureContainerIsSet();

            return container.BeginLifetimeScope(builder =>
            {
                builder.Register(_ => gameObject).As<GameObject>().InstancePerLifetimeScope();
                configurationAction(builder);
            });
        }

        public static void AssureContainerIsSet()
        {
            if (container == null)
            {
                throw new InvalidOperationException("Autofac container has not been set");
            }
        }
    }
}
