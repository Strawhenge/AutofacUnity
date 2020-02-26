using System;

namespace Autofac.Unity
{
    internal static class Context
    {
        private static IContainer container;
        private static InformationLogger logInformation = Logger.EmptyInformationLogger;
        private static ExceptionLogger logException = Logger.UnityConsoleExceptionLogger;

        public static IContainer Container
        {
            get => container ?? throw new InvalidOperationException("Autofac container has not been set");
            set => container = value;
        }

        public static InformationLogger LogInformation
        {
            get => logInformation ?? Logger.EmptyInformationLogger;
            set => logInformation = value;
        }

        public static ExceptionLogger LogException
        {
            get => logException ?? Logger.EmptyExceptionLogger;
            set => logException = value;
        }
    }
}
