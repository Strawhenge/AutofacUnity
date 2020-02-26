using UnityEngine;

namespace Autofac.Unity
{
    public class AutofacScript : MonoBehaviour
    {
        private void Awake()
        {
            AutofacUnity.InjectPropertiesForGameObjects(
                gameObject.scene.GetRootGameObjects());
        }
    }
}
