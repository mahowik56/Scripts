using System;
using System.IO;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientLogger.API;
using Platform.Library.ClientResources.API;
using Platform.Library.ClientResources.Impl;
using Platform.Library.ClientUnityIntegration;
using Platform.Library.ClientUnityIntegration.API;
using Platform.System.Data.Statics.ClientConfigurator.API;
using Platform.System.Data.Statics.ClientYaml.API;
using UnityEngine;

namespace Tanks.ClientLauncher.Impl
{
	public class UpdateConfigurationActivator : UnityAwareActivator<ManuallyCompleting>
	{
                [Inject]
                public static YamlService yamlService { get; set; }

                [Inject]
                public static ConfigurationService configurationService { get; set; }

                protected override void Activate()
                {
                        if (InitConfigurationActivator.LauncherPassed)
                        {
                                Complete();
                                return;
                        }

                        UpdateConfiguration.Config = new UpdateConfiguration
                        {
                                LastClientVersion = "master-48606",
                                DistributionUrl = "",
                                Executable = ""
                        };

                        Complete();
                }

	}
}
