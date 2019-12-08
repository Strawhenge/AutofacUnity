using System;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        private static IContainer _container;

        internal static IContainer Container => _container ?? throw new InvalidOperationException("Container has not been set");

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }
    }
}
