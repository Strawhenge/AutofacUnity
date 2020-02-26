using System;
using UnityEngine;

namespace Autofac.Unity
{
    public delegate void InformationLogger(UnityEngine.Object context, string message);

    public delegate void ExceptionLogger(UnityEngine.Object context, Exception exception);

    public static class Logger
    {
        public static InformationLogger EmptyInformationLogger => (context, message) => { };

        public static ExceptionLogger EmptyExceptionLogger => (context, exception) => { };

        public static InformationLogger UnityConsoleInformationLogger => (context, message) => Debug.Log(message, context);

        public static ExceptionLogger UnityConsoleExceptionLogger => (context, exception) => Debug.LogError(exception, context);
    }
}