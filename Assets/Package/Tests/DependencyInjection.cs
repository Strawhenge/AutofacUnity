using Autofac.Unity.Tests.Services;

namespace Autofac.Unity.Tests
{
    public static class DependencyInjection
    {
        public static void Configure()
        {
            AutofacUnity.Configure(builder =>
            {
                builder.RegisterType<Inventory>().AsSelf();
                builder.RegisterType<TimeAccessor>().AsSelf().SingleInstance();
            });
        }
    }
}
