using UnityEngine;
using UnityEngine.AI;

namespace Autofac.Unity
{
    public class UnityComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypeFromGameObject<Animator>().AsSelf();
            builder.RegisterTypeFromGameObject<Transform>().AsSelf();
            builder.RegisterTypeFromGameObject<NavMeshAgent>().AsSelf();
            builder.RegisterTypeFromGameObject<Rigidbody>().AsSelf();

            base.Load(builder);
        }
    }
}