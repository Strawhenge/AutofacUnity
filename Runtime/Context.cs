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

       
    }
}
