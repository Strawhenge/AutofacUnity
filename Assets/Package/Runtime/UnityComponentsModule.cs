using UnityEngine;
using UnityEngine.AI;

namespace Autofac.Unity
{
    public class UnityComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(x => x.GetGameObject())
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterTypeFromGameObject<Transform>().AsSelf();
            builder.RegisterTypeFromGameObject<Rigidbody>().AsSelf();
            builder.RegisterTypeFromGameObject<Animator>().AsSelf();
            builder.RegisterTypeFromGameObject<NavMeshAgent>().AsSelf();

            base.Load(builder);
        }
    }
}