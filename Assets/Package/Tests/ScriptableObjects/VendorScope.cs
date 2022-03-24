using Autofac.Unity.Tests.Services;
using UnityEngine;

namespace Autofac.Unity.Tests.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Vendor Scope")]
    public class VendorScope : ScopeConfiguration
    {
        protected override void Configure(ContainerBuilder builder)
        {
            builder
                .RegisterType<VendorInventory>()
                .As<Inventory>()
                .InstancePerLifetimeScope();
        }
    }
}
