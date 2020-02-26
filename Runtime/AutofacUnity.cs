using System;
using System.Collections.Generic;
using UnityEngine;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        public static void SetContainer(IContainer container) => Context.Container = container;

        public static void LogInformationOutput(InformationLogger logger) => Context.LogInformation = logger;

        public static void LogExceptionOutput(ExceptionLogger logger) => Context.LogException = logger;

        public static void InjectPropertiesForGameObject(GameObject gameObject)
        {
            Context.LogInformation(gameObject, $"Creating scope for {gameObject}");
            var scope = Context.Container.BeginLifetimeScope(gameObject);

            foreach (var monoBehaviour in gameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                Context.LogInformation(gameObject, $"Injecting properties for {monoBehaviour}");
                scope.InjectUnsetProperties(monoBehaviour);
            }
        }

        public static void InjectPropertiesForGameObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                try
                {
                    InjectPropertiesForGameObject(gameObject);
                }
                catch (Exception exception)
                {
                    Context.LogException(gameObject, exception);
                }
            }
        }
    }
}
