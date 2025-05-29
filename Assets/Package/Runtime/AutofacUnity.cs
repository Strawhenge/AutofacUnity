using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        public static void Configure(Action<ContainerBuilder> buildAction)
        {
            var builder = new ContainerBuilder();
            buildAction(builder);

            Injector.Container = builder.Build();
        }

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(GameObject gameObject) =>
            InjectUnsetPropertiesForGameObject(gameObject, Enumerable.Empty<Parameter>());

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(GameObject gameObject, params Parameter[] parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, parameters as IEnumerable<Parameter>);

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(GameObject gameObject, IEnumerable<Parameter> parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, _ => { }, parameters);

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(GameObject gameObject, Action<ContainerBuilder> configurationAction) =>
         InjectUnsetPropertiesForGameObject(gameObject, configurationAction, Enumerable.Empty<Parameter>());

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(GameObject gameObject, Action<ContainerBuilder> configurationAction, params Parameter[] parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, configurationAction, parameters as IEnumerable<Parameter>);

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(GameObject gameObject, Action<ContainerBuilder> configurationAction, IEnumerable<Parameter> parameters) =>
            Injector.InjectUnsetPropertiesForGameObject(gameObject, configurationAction, parameters);

        public static void ForTag(string tag, Action<ContainerBuilder> configurationAction) =>
            TagDependencies.AddConfigurationActionForTag(tag, configurationAction);
    }
}
