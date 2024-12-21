using System;
using System.Collections.Generic;

namespace Autofac.Unity
{
    static class TagDependencies
    {
        static readonly Dictionary<string, List<Action<ContainerBuilder>>> ConfigurationActionsByTag = new();

        public static void AddConfigurationActionForTag(string tag, Action<ContainerBuilder> configurationAction)
        {
            if (!ConfigurationActionsByTag.ContainsKey(tag))
                ConfigurationActionsByTag.Add(tag, new List<Action<ContainerBuilder>>());

            ConfigurationActionsByTag[tag].Add(configurationAction);
        }

        public static void ExecuteConfigurationActionsForTag(string tag, ContainerBuilder builder)
        {
            if (!ConfigurationActionsByTag.TryGetValue(tag, out var configurationActions))
                return;

            foreach (var configurationAction in configurationActions)
                configurationAction(builder);
        }
    }
}