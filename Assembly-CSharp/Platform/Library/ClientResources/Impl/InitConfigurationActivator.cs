using System;
using System.IO;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientLogger.API;
using Platform.Library.ClientResources.API;
using Platform.Library.ClientUnityIntegration;
using Platform.Library.ClientUnityIntegration.API;
using Platform.System.Data.Statics.ClientYaml.API;
using Tanks.ClientLauncher.API;
using UnityEngine;

namespace Platform.Library.ClientResources.Impl
{
	public class InitConfigurationActivator : UnityAwareActivator<ManuallyCompleting>
	{
                [Inject]
                public static YamlService yamlService { get; set; }

                [Inject]
                public new static EngineService EngineService { get; set; }

                public static bool LauncherPassed { get; set; }

                protected override void Activate()
                {
                        if (LauncherPassed)
                        {
                                Complete();
                                return;
                        }

                        InitConfiguration.Config = new InitConfiguration
                        {
                                Host = "*ip*",
                                AcceptorPort = "5050",
                                ConfigVersion = "master-48606",
                                BundleDbVersion = "master-48606",
                                ResourcesUrl = "http://*ip*:8080/resources",
                                ConfigsUrl = "http://*ip*:8080/config",
                                UpdateConfigUrl = "http://*ip*:8080/update/{BuildTarget}.yml",
                                StateFileUrl = "http://*ip*:8080/state/_state.yml"
                        };

                        Complete();
                }

	}
}
