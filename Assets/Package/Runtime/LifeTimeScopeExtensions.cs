using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Autofac.Unity
{
    static class LifeTimeScopeExtensions
    {
        public static void InjectUnsetPropertiesForGameObject(this ILifetimeScope scope, GameObject gameObject) =>
            scope.InjectUnsetPropertiesForGameObject(gameObject, Array.Empty<Parameter>());

        public static void InjectUnsetPropertiesForGameObject(
            this ILifetimeScope scope,
            GameObject gameObject,
            IEnumerable<Parameter> parameters)
        {
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
    }
}