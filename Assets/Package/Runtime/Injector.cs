using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Autofac.Unity
{
    static class Injector
    {
        public static IContainer Container { get; set; }

        public static void InjectUnsetPropertiesForGameObject(
            GameObject gameObject,
            Action<ContainerBuilder> configurationAction,
            IEnumerable<Parameter> parameters)
        {
            EnsureContainerIsSet();

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
                .GetComponentsInChildren<AutofacScript>(includeInactive: true)
                .Where(x => x.gameObject != gameObject)
                .SelectMany(x => x.GetComponentsInChildren<MonoBehaviour>(includeInactive: true))
                .ToArray();

            var propertiesParameters = parameters.ToArray();
            foreach (var monoBehaviour in gameObject.GetComponentsInChildren<MonoBehaviour>(includeInactive: true))
            {
                if (!ignore.Contains(monoBehaviour))
                    scope.InjectUnsetProperties(monoBehaviour, propertiesParameters);
            }
        }

        static void EnsureContainerIsSet()
        {
            if (Container == null)
                throw new InvalidOperationException("Autofac has not been configured.");
        }
    }
}