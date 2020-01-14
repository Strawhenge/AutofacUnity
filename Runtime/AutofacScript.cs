using System;
using UnityEngine;

namespace Autofac.Unity
{
    public class AutofacScript : MonoBehaviour
    {
        private IContainer container;
        private InformationLogger logInformation;
        private ExceptionLogger logException;

        public void InjectProperties(GameObject gameObject)
        {
            logInformation(this, $"Creating scope for {gameObject}");
            var scope = container.BeginLifetimeScope(gameObject);

            foreach (var monoBehaviour in gameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                logInformation(this, $"Injecting properties for {monoBehaviour}");
                scope.InjectUnsetProperties(monoBehaviour);
            }
        }

        private void Awake()
        {
            container = AutofacUnity.Container;
            logInformation = AutofacUnity.InformationLogger;
            logException = AutofacUnity.ExceptionLogger;

            var gameObjects = gameObject.scene.GetRootGameObjects();
            logInformation(this, $"GameObjects found: {gameObjects.Length}");

            foreach (var gameObject in gameObjects)
            {
                try
                {
                    InjectProperties(gameObject);
                }
                catch (Exception exception)
                {
                    logException(gameObject, exception);
                }
            }
        }
    }
}
