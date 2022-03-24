using Autofac.Unity.Tests.Services;

namespace Autofac.Unity.Tests
{
    public static class DependencyInjection
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Inventory>().AsSelf();

            AutofacUnity.SetContainer(builder.Build());
        }
    }
}
