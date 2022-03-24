using System;
using UnityEngine;

namespace Autofac.Unity
{
    public class AutofacScript : MonoBehaviour
    {
        private void Awake()
        {
            foreach (var gameObject in gameObject.scene.GetRootGameObjects())
            {
                try
                {
                    AutofacUnity.InjectUnsetPropertiesForGameObject(gameObject);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception, this);
                    Destroy(gameObject);                    
                }
            }
        }
    }
}
