using UnityEngine;

namespace Autofac.Unity
{
    public abstract class AutofacScope : ScriptableObject
    {
        internal protected abstract void Configure(ContainerBuilder builder);
    }
}