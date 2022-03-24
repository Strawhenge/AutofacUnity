using UnityEngine;

namespace Autofac.Unity
{
    public abstract class ScopeConfiguration : ScriptableObject
    {
        internal protected abstract void Configure(ContainerBuilder builder);
    }
}