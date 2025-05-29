using UnityEngine;

namespace Autofac.Unity
{
    public class AutofacScript : MonoBehaviour
    {
        [SerializeField] ScopeConfiguration _scopeConfiguration;

        ILifetimeScope _scope;

        void Awake()
        {
            _scope = _scopeConfiguration == null
                ? AutofacUnity.InjectUnsetPropertiesForGameObject(gameObject)
                : AutofacUnity.InjectUnsetPropertiesForGameObject(gameObject, _scopeConfiguration.Configure);
        }

        public void AddToScope(MonoBehaviour script) =>
            _scope.InjectUnsetPropertiesForGameObject(script.gameObject);

        public void AddToScope(GameObject gameObject) =>
            _scope.InjectUnsetPropertiesForGameObject(gameObject);
    }
}