using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;
using Platform.Library.ClientUnityIntegration.API;
using Tanks.Battle.ClientCore.Impl;
using Tanks.Lobby.ClientGarage.API;

namespace Tanks.Battle.ClientHUD.Impl
{
	public class ItemButtonAmmunitionSystem : ECSSystem
	{
		public class SlotCooldownStateNode : Node
		{
			public SlotUserItemInfoComponent slotUserItemInfo;

			public ModuleGroupComponent moduleGroup;

			public InventoryCooldownStateComponent inventoryCooldownState;
		}

		public class UserItemCounterNode : Node
		{
			public UserItemCounterComponent userItemCounter;
		}

		[OnEventFire]
		public void InventoryAmmunitionChanged(InventoryAmmunitionChangedEvent e, SingleNode<InventoryAmmunitionComponent> node, [JoinByModule] SingleNode<ItemButtonComponent> hud, [JoinByModule] Optional<UserItemCounterNode> userItemCounter, [JoinByModule] Optional<SlotCooldownStateNode> slotCooldownNode)
		{
			if (!userItemCounter.IsPresent() || (userItemCounter.IsPresent() && userItemCounter.Get().userItemCounter.Count > 0))
			{
				hud.component.ItemAmmunitionCount = node.component.CurrentCount;
				if (slotCooldownNode.IsPresent())
				{
					StartCooldown(slotCooldownNode.Get(), hud.component);
				}
			}
		}

		private void StartCooldown(SlotCooldownStateNode slot, ItemButtonComponent item)
		{
			float timeInSec = (float)slot.inventoryCooldownState.CooldownTime / 1000f - (Date.Now.UnityTime - slot.inventoryCooldownState.CooldownStartTime.UnityTime);
			if (!item.isRage)
			{
				item.StartCooldown(timeInSec, slot.Entity.HasComponent<InventoryEnabledStateComponent>());
			}
			else
			{
				item.StartRageCooldown(timeInSec, slot.Entity.HasComponent<InventoryEnabledStateComponent>());
			}
		}
	}
}
