using Autofac.Unity.Tests.Services;

namespace Autofac.Unity.Tests
{
    public static class DependencyInjection
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Inventory>().AsSelf();

            builder.RegisterType<TimeAccessor>().AsSelf().SingleInstance();

            AutofacUnity.SetContainer(builder.Build());
        }
    }
}
