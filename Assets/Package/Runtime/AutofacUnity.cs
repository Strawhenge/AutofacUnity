using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        public static IContainer Container { get; set; }

        public static void Configure(Action<ContainerBuilder> buildAction)
        {
            var builder = new ContainerBuilder();
            buildAction(builder);

            Container = builder.Build();
        }

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(
            GameObject gameObject) =>
            InjectUnsetPropertiesForGameObject(gameObject, Enumerable.Empty<Parameter>());

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(
            GameObject gameObject,
            params Parameter[] parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, parameters as IEnumerable<Parameter>);

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(
            GameObject gameObject,
            IEnumerable<Parameter> parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, _ => { }, parameters);

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(
            GameObject gameObject,
            Action<ContainerBuilder> configurationAction) =>
            InjectUnsetPropertiesForGameObject(gameObject, configurationAction, Enumerable.Empty<Parameter>());

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(
            GameObject gameObject,
            Action<ContainerBuilder> configurationAction,
            params Parameter[] parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, configurationAction, parameters as IEnumerable<Parameter>);

        public static ILifetimeScope InjectUnsetPropertiesForGameObject(
            GameObject gameObject,
            Action<ContainerBuilder> configurationAction,
            IEnumerable<Parameter> parameters)
        {
            EnsureContainerIsSet();

            var scope = Container.BeginLifetimeScope(builder =>
            {
                builder
                    .Register(_ => gameObject)
                    .As<GameObject>()
                    .InstancePerLifetimeScope();

                TagDependencies.ExecuteConfigurationActionsForTag(gameObject.tag, builder);
                configurationAction(builder);
            });
            
            scope.InjectUnsetPropertiesForGameObject(gameObject, parameters);
            return scope;
        }

        public static void ForTag(string tag, Action<ContainerBuilder> configurationAction) =>
            TagDependencies.AddConfigurationActionForTag(tag, configurationAction);

        static void EnsureContainerIsSet()
        {
            if (Container == null)
                throw new InvalidOperationException("Autofac has not been configured.");
        }
    }
}