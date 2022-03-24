using System;
using UnityEngine;

namespace Autofac.Unity
{
    public class AutofacScript : MonoBehaviour
    {
        [SerializeField] AutofacScope _scopeConfiguration;

        void Awake()
        {
            try
            {
                if (_scopeConfiguration == null)
                    AutofacUnity.InjectUnsetPropertiesForGameObject(gameObject);
                else
                    AutofacUnity.InjectUnsetPropertiesForGameObject(gameObject, _scopeConfiguration.Configure);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception, this);
                Destroy(gameObject);
            }
        }
    }
}
