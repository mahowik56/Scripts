using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientLogger.API;
using Platform.Library.ClientResources.API;
using Platform.Library.ClientUnityIntegration;
using Platform.Library.ClientUnityIntegration.API;
using Platform.System.Data.Statics.ClientConfigurator.API;
using Platform.System.Data.Statics.ClientConfigurator.Impl;
using SharpCompress.Compressor;
using SharpCompress.Compressor.Deflate;
using UnityEngine;

namespace Platform.Library.ClientResources.Impl
{
	public class ConfigurationActivator : UnityAwareActivator<ManuallyCompleting>
	{
                [Inject]
                public static ConfigurationService ConfigurationService { get; set; }

                [Inject]
                public new static EngineService EngineService { get; set; }

                protected override void Activate()
                {
                        ((ConfigurationServiceImpl)ConfigurationService).SetRootConfigNode(LocalConfiguration.rootConfigNode);
                        Complete();
                }

		private void HandleError<T>(string errorMessage) where T : Platform.Kernel.ECS.ClientEntitySystem.API.Event, new()
		{
			LoggerProvider.GetLogger(this).Error(errorMessage);
			HandleError<T>();
		}

		private void HandleError<T>(string errorMessage, Exception e) where T : Platform.Kernel.ECS.ClientEntitySystem.API.Event, new()
		{
			LoggerProvider.GetLogger(this).Error(errorMessage, e);
			HandleError<T>();
		}

                private void HandleError<T>() where T : Platform.Kernel.ECS.ClientEntitySystem.API.Event, new()
                {
                        Entity entity = EngineService.Engine.CreateEntity("RemoteConfigLoading");
                        EngineService.Engine.ScheduleEvent<T>(entity);
                }
	}
}
