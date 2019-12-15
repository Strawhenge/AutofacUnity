using System;
using UnityEngine;

namespace Autofac.Unity
{
    public class AutofacScript : MonoBehaviour
    {
        public bool EnableLogs;

        private IContainer container;

        public void InjectProperties(GameObject gameObject)
        {
            Log($"creating scope for {gameObject}");
            var scope = container.BeginLifetimeScope(gameObject);

            foreach (var monoBehaviour in gameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                Log($"injecting properties for {monoBehaviour}");
                scope.InjectUnsetProperties(monoBehaviour);
            }
        }

        private void Log(string message)
        {
            if (EnableLogs)
                Debug.Log(message, this);
        }

        private void Awake()
        {
            container = AutofacUnity.Container;

            var gameObjects = gameObject.scene.GetRootGameObjects();
            Log($"GameObjects found: {gameObjects.Length}");

            foreach (var gameObject in gameObjects)
            {
                try
                {
                    InjectProperties(gameObject);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception, gameObject);
                }
            }
        }
    }
}
