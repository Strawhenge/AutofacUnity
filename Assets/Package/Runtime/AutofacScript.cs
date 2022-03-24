using System;
using UnityEngine;

namespace Autofac.Unity
{
    public class AutofacScript : MonoBehaviour
    {
        void Awake()
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
