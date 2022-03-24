using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Autofac.Unity
{
    internal static class Injector
    {
        public static IContainer Container { get; set; }

        public static void InjectUnsetPropertiesForGameObject(
            GameObject gameObject,
            Action<ContainerBuilder> configurationAction,
            IEnumerable<Parameter> parameters)
        {
            AssertContainerIsSet();

            var scope = Container.BeginLifetimeScope(builder =>
            {
                builder
                    .Register(_ => gameObject)
                    .As<GameObject>()
                    .InstancePerLifetimeScope();

                TagDependencies.ExecuteConfigurationActionsForTag(gameObject.tag, builder);
                configurationAction(builder);
            });

            var ignore = gameObject
                .GetComponentsInChildren<AutofacScript>()
                .Where(x => x.gameObject != gameObject)
                .SelectMany(x => x.GetComponentsInChildren<MonoBehaviour>())
                .ToArray();

            foreach (var monoBehaviour in gameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                if (!ignore.Contains(monoBehaviour))
                    scope.InjectUnsetProperties(monoBehaviour, parameters);
            }
        }

        private static void AssertContainerIsSet()
        {
            if (Container == null)
            {
                throw new InvalidOperationException("Autofac container has not been set");
            }
        }
    }
}
