using Autofac.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        private static IContainer container;

        public static void SetContainer(IContainer container) => AutofacUnity.container = container;

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject)
        {
            CreateScopeAndInjectMonobehaviourProperties(gameObject, injectStrategy: InjectMonobehaviourProperties);

            void InjectMonobehaviourProperties(ILifetimeScope scope, MonoBehaviour monoBehaviour)
            {
                scope.InjectUnsetProperties(monoBehaviour);
            }
        }

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, params Parameter[] parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, parameters as IEnumerable<Parameter>);

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, IEnumerable<Parameter> parameters)
        {
            CreateScopeAndInjectMonobehaviourProperties(gameObject, injectStrategy: InjectMonobehaviourProperties);

            void InjectMonobehaviourProperties(ILifetimeScope scope, MonoBehaviour monoBehaviour)
            {
                scope.InjectUnsetProperties(monoBehaviour, parameters);
            }
        }

        private static void CreateScopeAndInjectMonobehaviourProperties(GameObject gameObject, Action<ILifetimeScope, MonoBehaviour> injectStrategy)
        {
            AssureContainerIsSet();

            var scope = CreateScopeForGameObject(gameObject);

            foreach (var monoBehaviour in gameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                injectStrategy(scope, monoBehaviour);
            }
        }

        private static void AssureContainerIsSet()
        {
            if (container == null)
            {
                throw new InvalidOperationException("Autofac container has not been set");
            }
        }

        private static ILifetimeScope CreateScopeForGameObject(GameObject gameObject)
        {
            return container.BeginLifetimeScope(builder =>
            {
                builder.Register(_ => gameObject).As<GameObject>().InstancePerLifetimeScope();
            });
        }
    }
}
