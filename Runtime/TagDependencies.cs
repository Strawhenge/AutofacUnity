using System;
using System.Collections.Generic;

namespace Autofac.Unity
{
    internal static class TagDependencies
    {
        private static readonly Dictionary<string, List<Action<ContainerBuilder>>> configurationActionsByTag = new Dictionary<string, List<Action<ContainerBuilder>>>();

        public static void AddConfigurationActionForTag(string tag, Action<ContainerBuilder> configurationAction)
        {
            if (!configurationActionsByTag.ContainsKey(tag))
                configurationActionsByTag.Add(tag, new List<Action<ContainerBuilder>>());

            configurationActionsByTag[tag].Add(configurationAction);
        }

        public static void ExecuteConfigurationActionsForTag(string tag, ContainerBuilder builder)
        {
            if (!configurationActionsByTag.ContainsKey(tag)) return;

            foreach (var configurationAction in configurationActionsByTag[tag])
            {
                configurationAction(builder);
            }
        }
    }
}