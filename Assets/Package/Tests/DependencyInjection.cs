using Autofac.Unity.Tests.Services;

namespace Autofac.Unity.Tests
{
    public static class DependencyInjection
    {
        public static void Configure()
        {
            AutofacUnity.Configure(builder =>
            {
                builder.RegisterType<Inventory>().AsSelf().InstancePerLifetimeScope();
                builder.RegisterType<Health>().AsSelf().InstancePerLifetimeScope();

                builder.RegisterType<TimeAccessor>().AsSelf().SingleInstance();
            });
        }
    }
}
