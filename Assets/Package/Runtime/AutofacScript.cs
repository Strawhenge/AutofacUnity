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

        public void AddScriptToScope(MonoBehaviour script) =>
            _scope.InjectUnsetProperties(script);

        public void AddGameObjectToScope(MonoBehaviour script) =>
            AddGameObjectToScope(script.gameObject);

        public void AddGameObjectToScope(GameObject gameObject) =>
            _scope.InjectUnsetPropertiesForGameObject(gameObject);
    }
}