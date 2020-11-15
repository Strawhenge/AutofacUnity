using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Autofac.Unity
{
    public static class AutofacUnity
    {
        public static void SetContainer(IContainer container) => Injector.Container = container;

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject) =>
            InjectUnsetPropertiesForGameObject(gameObject, Enumerable.Empty<Parameter>());

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, params Parameter[] parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, parameters as IEnumerable<Parameter>);

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, IEnumerable<Parameter> parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, _ => { }, parameters);

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, Action<ContainerBuilder> configurationAction) =>
         InjectUnsetPropertiesForGameObject(gameObject, configurationAction, Enumerable.Empty<Parameter>());

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, Action<ContainerBuilder> configurationAction, params Parameter[] parameters) =>
            InjectUnsetPropertiesForGameObject(gameObject, configurationAction, parameters as IEnumerable<Parameter>);

        public static void InjectUnsetPropertiesForGameObject(GameObject gameObject, Action<ContainerBuilder> configurationAction, IEnumerable<Parameter> parameters) =>
            Injector.InjectUnsetPropertiesForGameObject(gameObject, configurationAction, parameters);
    }   
}
