using System;
using Assets.platform.library.ClientResources.Scripts.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientUnityIntegration;
using Platform.System.Data.Statics.ClientConfigurator.API;
using Platform.System.Data.Statics.ClientYaml.API;

namespace Platform.Library.ClientResources.Impl
{
	public class DiskCachingActivator : UnityAwareActivator<AutoCompleting>
	{
                protected override void Activate()
                {
                        DiskCaching.Enabled = true;
                        DiskCaching.MaximumAvailableDiskSpace = 2671771648L;
                        DiskCaching.ExpirationDelay = 5184000;
                        DiskCaching.CompressionEnambled = false;
                }
	}
}
