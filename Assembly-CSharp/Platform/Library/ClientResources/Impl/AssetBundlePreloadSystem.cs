using System.Collections.Generic;
using Assets.platform.library.ClientResources.Scripts.API;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientResources.API;
using Platform.Library.ClientUnityIntegration.API;
using Platform.System.Data.Statics.ClientConfigurator.API;
using Platform.System.Data.Statics.ClientYaml.API;

namespace Platform.Library.ClientResources.Impl
{
	public class AssetBundlePreloadSystem : ECSSystem
	{
		public class PreloadNode : Node
		{
			public AssetReferenceComponent assetReference;

			public PreloadComponent preload;

			public AssetBundlesLoadDataComponent assetBundlesLoadData;
		}

		public class DataBaseNode : Node
		{
			public AssetBundleDatabaseComponent assetBundleDatabase;

			public BaseUrlComponent baseUrl;

                        public AssetBundleDiskCacheComponent assetBundleDiskCache;
                }

		[OnEventFire]
		public void StartPreload(NodeAddedEvent e, SingleNode<PreloadAllResourcesComponent> preload, [JoinAll] DataBaseNode db)
		{
			if (!DiskCaching.Enabled)
			{
				return;
			}
			AssetBundleDatabase assetBundleDatabase = db.assetBundleDatabase.AssetBundleDatabase;
			AssetBundleDiskCache assetBundleDiskCache = db.assetBundleDiskCache.AssetBundleDiskCache;
			List<string> prioritizedAssetsConfigPathList = GetPrioritizedAssetsConfigPathList();
			int num = 100 + prioritizedAssetsConfigPathList.Count;
			List<string> list = new List<string>();
			for (int i = 0; i < prioritizedAssetsConfigPathList.Count; i++)
			{
				AssetReferenceComponent assetReferenceComponent = AssetReferenceComponent.createFromConfig(prioritizedAssetsConfigPathList[i]);
				string assetGuid = assetReferenceComponent.Reference.AssetGuid;
				list.Add(assetGuid);
				AssetBundleInfo assetBundleInfoByGuid = assetBundleDatabase.GetAssetBundleInfoByGuid(assetGuid);
				if (!assetBundleDiskCache.IsCached(assetBundleInfoByGuid))
				{
					int loadingPriority = num - i;
					CreateEntityForPreloadingBundles(assetReferenceComponent, loadingPriority);
				}
			}
			foreach (string rootGuid in assetBundleDatabase.GetRootGuids())
			{
				AssetBundleInfo assetBundleInfoByGuid2 = assetBundleDatabase.GetAssetBundleInfoByGuid(rootGuid);
				if (!list.Contains(rootGuid) && !assetBundleDiskCache.IsCached(assetBundleInfoByGuid2))
				{
					AssetReferenceComponent assetReferenceComponent2 = new AssetReferenceComponent(new AssetReference(rootGuid));
					CreateEntityForPreloadingBundles(assetReferenceComponent2, 0);
				}
			}
		}

		[OnEventComplete]
		public void CompletePreload(UpdateEvent e, SingleNode<PreloadAllResourcesComponent> preload, [JoinAll] ICollection<PreloadNode> loadingRequests)
		{
			if (loadingRequests.Count == 0)
			{
				preload.Entity.RemoveComponent<PreloadAllResourcesComponent>();
			}
		}

		[OnEventFire]
		public void CancelPreload(NodeRemoveEvent e, SingleNode<PreloadAllResourcesComponent> preload, [JoinAll] ICollection<PreloadNode> loadingRequests)
		{
			foreach (PreloadNode loadingRequest in loadingRequests)
			{
				DeleteEntity(loadingRequest.Entity);
			}
		}

		[OnEventComplete]
		public void Complete(AssetBundlesLoadedEvent e, PreloadNode loadingRequest)
		{
			DeleteEntity(loadingRequest.Entity);
		}

		private void CreateEntityForPreloadingBundles(AssetReferenceComponent assetReferenceComponent, int loadingPriority)
		{
			Entity entity = CreateEntity("PreloadBundles");
			entity.AddComponent(assetReferenceComponent);
			entity.AddComponent<PreloadComponent>();
			LoadAssetBundlesRequestComponent loadAssetBundlesRequestComponent = new LoadAssetBundlesRequestComponent();
			loadAssetBundlesRequestComponent.LoadingPriority = loadingPriority;
			LoadAssetBundlesRequestComponent component = loadAssetBundlesRequestComponent;
			entity.AddComponent(component);
		}

                private static List<string> GetPrioritizedAssetsConfigPathList()
                {
                        return new List<string>
                        {
                                "garage/skin/weapon/flamethrower/m0",
                                "garage/skin/weapon/freeze/m0",
                                "garage/skin/weapon/hammer/m0",
                                "garage/skin/weapon/isis/m0",
                                "garage/skin/weapon/railgun/m0",
                                "garage/skin/weapon/ricochet/m0",
                                "garage/skin/weapon/shaft/m0",
                                "garage/skin/weapon/thunder/m0",
                                "garage/skin/weapon/smoky/m0",
                                "garage/skin/weapon/twins/m0",
                                "garage/skin/weapon/vulcan/m0",
                                "garage/skin/tank/dictator/m0",
                                "garage/skin/tank/hornet/m0",
                                "garage/skin/tank/hunter/m0",
                                "garage/skin/tank/mammoth/m0",
                                "garage/skin/tank/titan/m0",
                                "garage/skin/tank/viking/m0",
                                "garage/skin/tank/wasp/m0",
                                "garage/skin/weapon/flamethrower/m1",
                                "garage/skin/weapon/freeze/m1",
                                "garage/skin/weapon/hammer/m1",
                                "garage/skin/weapon/isis/m1",
                                "garage/skin/weapon/railgun/m1",
                                "garage/skin/weapon/ricochet/m1",
                                "garage/skin/weapon/shaft/m1",
                                "garage/skin/weapon/thunder/m1",
                                "garage/skin/weapon/smoky/m1",
                                "garage/skin/weapon/twins/m1",
                                "garage/skin/weapon/vulcan/m1",
                                "garage/skin/tank/dictator/m1",
                                "garage/skin/tank/hornet/m1",
                                "garage/skin/tank/hunter/m1",
                                "garage/skin/tank/mammoth/m1",
                                "garage/skin/tank/titan/m1",
                                "garage/skin/tank/viking/m1",
                                "garage/skin/tank/wasp/m1"
                        };
                }
	}
}
