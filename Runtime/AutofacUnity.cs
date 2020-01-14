using System;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        private static IContainer _container;

        internal static IContainer Container => _container ?? throw new InvalidOperationException("Autofac container has not been set");

        internal static InformationLogger InformationLogger { get; private set; } = Logger.EmptyInformationLogger;

        internal static ExceptionLogger ExceptionLogger { get; private set; } = Logger.UnityConsoleExceptionLogger;

        public static void SetContainer(IContainer container) => _container = container;

        public static void LogInformationOutput(InformationLogger logger) => InformationLogger = logger;

        public static void LogExceptionOutput(ExceptionLogger logger) => ExceptionLogger = logger;
    }
}
