using System;
using UnityEngine;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        private static IContainer container;

        public static void SetContainer(IContainer container) => AutofacUnity.container = container;

        public static void InjectPropertiesForGameObject(GameObject gameObject)
        {
            AssureContainerIsSet();

            var scope = container.BeginLifetimeScope(gameObject);

            foreach (var monoBehaviour in gameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                scope.InjectUnsetProperties(monoBehaviour);
            }
        }

        private static void AssureContainerIsSet()
        {
            if (container == null)
            {
                throw new InvalidOperationException("Autofac container has not been set");
            }
        }
    }
}
