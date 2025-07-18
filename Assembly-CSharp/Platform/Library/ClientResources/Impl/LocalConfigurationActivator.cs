using System;
using System.IO;
using System.Linq;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientLogger.API;
using Platform.Library.ClientResources.API;
using Platform.Library.ClientUnityIntegration;
using Platform.Library.ClientUnityIntegration.API;
using Platform.System.Data.Statics.ClientConfigurator.API;
using Platform.System.Data.Statics.ClientConfigurator.Impl;
using Platform.System.Data.Statics.ClientYaml.API;
using UnityEngine;

namespace Platform.Library.ClientResources.Impl
{
	public class LocalConfigurationActivator : UnityAwareActivator<ManuallyCompleting>
	{
                [Inject]
                public static ConfigurationService ConfigurationService { get; set; }

                [Inject]
                public new static EngineService EngineService { get; set; }

                protected override void Activate()
                {
                        LocalConfiguration.rootConfigNode = new ConfigTreeNodeImpl(string.Empty);
                        SetLoadingStopTimeout();
                        Complete();
                }

                private void SetLoadingStopTimeout()
                {
                        WWWLoader.DEFAULT_TIMEOUT_SECONDS = 60;
                }

		private void HandleError(string errorMessage)
		{
			LoggerProvider.GetLogger(this).Error(errorMessage);
			HandleError();
		}

		private void HandleError(string errorMessage, Exception e)
		{
			LoggerProvider.GetLogger(this).Error(errorMessage, e);
			HandleError();
		}

		private void HandleError()
		{
			Engine engine = EngineService.Engine;
			Entity entity = engine.CreateEntity("LocalConfigurationLoadingError");
			engine.ScheduleEvent<InvalidLocalConfigurationErrorEvent>(entity);
		}

		private string[] GetProfiles()
		{
			ConfigurationProfileElement[] components = GetComponents<ConfigurationProfileElement>();
			if (components.Count() == 0)
			{
				return null;
			}
			string[] array = new string[components.Count()];
			for (int i = 0; i < components.Count(); i++)
			{
				array[i] = components[i].ProfileElement;
			}
			return array;
		}
	}
}
