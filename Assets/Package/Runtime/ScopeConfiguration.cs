using UnityEngine;

namespace Autofac.Unity
{
    public abstract class ScopeConfiguration : ScriptableObject
    {
        protected internal abstract void Configure(ContainerBuilder builder);
    }
}