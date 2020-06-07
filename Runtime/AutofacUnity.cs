using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        public static void SetContainer(IContainer container) => GameObjectScopes.SetContainer(container);

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject) =>
            InjectUnsetPropertiesForGameObject(gameObject, Enumerable.Empty<Parameter>());

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, params Parameter[] parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, parameters as IEnumerable<Parameter>);

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, IEnumerable<Parameter> parameters)
        {
            var scope = CreateScopeForGameObject(gameObject);

            foreach (var monoBehaviour in gameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                scope.InjectUnsetProperties(monoBehaviour, parameters);
            }
        }

        private static ILifetimeScope CreateScopeForGameObject(GameObject gameObject) => GameObjectScopes.Create(gameObject);
    }


}
