using System;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientLogger.API;
using Platform.Library.ClientResources.API;
using Platform.Library.ClientUnityIntegration;
using Platform.Library.ClientUnityIntegration.API;
using Platform.System.Data.Statics.ClientConfigurator.API;
using Platform.System.Data.Statics.ClientYaml.API;

namespace Platform.Library.ClientResources.Impl
{
	public class StartupConfigurationActivator : UnityAwareActivator<AutoCompleting>
	{
                [Inject]
                public new static EngineService EngineService { get; set; }

                protected override void Activate()
                {
                        StartupConfiguration.Config = new StartupConfiguration
                        {
                                InitUrl = "http://127.0.0.1:8080/config/init.yml",
                                StateUrl = "http://127.0.0.1:8080/state/tankixprod_state.yml",
                                CurrentClientVersion = "master-48606"
                        };

                        Complete();
                }

		private void HandleError<T>(string errorMessage, Exception e) where T : Event, new()
		{
			LoggerProvider.GetLogger(this).Error(errorMessage, e);
			Engine engine = EngineService.Engine;
			Entity entity = engine.CreateEntity("StartupConfigLoading");
			engine.ScheduleEvent<T>(entity);
		}
	}
}
