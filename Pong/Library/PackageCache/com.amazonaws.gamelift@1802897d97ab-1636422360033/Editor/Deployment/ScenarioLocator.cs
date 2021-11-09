// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonGameLift.Editor
{
    /// <summary>
    /// Locates the sceanrio deploymemt types and creates their instances.
    /// </summary>
    internal class ScenarioLocator
    {
        public static ScenarioLocator SharedInstance { get; } = new ScenarioLocator();

        internal ScenarioLocator()
        {
        }

        public virtual IEnumerable<DeployerBase> GetScenarios()
        {
            IEnumerable<Type> deployerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetTypes().FirstOrDefault(IsNonProxyDelpoyerType))
                .OfType<Type>();

            DeployerBase[] deployers = deployerTypes
                .Select(deployerType => (DeployerBase)Activator.CreateInstance(deployerType))
                .ToArray();

            Array.Sort(deployers, (item1, item2) => item1.PreferredUiOrder.CompareTo(item2.PreferredUiOrder));
            return deployers;
        }

        // <class-name>Proxy is the pattern for dynamic mocked type
        private static bool IsNonProxyDelpoyerType(Type type)
        {
            return type.IsPublic && type.IsSubclassOf(typeof(DeployerBase)) && !type.Name.EndsWith("Proxy");
        }
    }
}
