using System.Collections.Generic;
using Lobby.ClientUserProfile.API;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Tanks.Battle.ClientCore.API;
using Tanks.Battle.ClientCore.Impl;
using Tanks.Lobby.ClientBattleSelect.API;

namespace Tanks.Lobby.ClientBattleSelect.Impl
{
	public class ScoreTableFlagIndicatorSystem : ECSSystem
	{
		public class FlagIndicatorNode : Node
		{
			public ScoreTableFlagIndicatorComponent scoreTableFlagIndicator;

			public UserGroupComponent userGroup;
		}

		public class UserNode : Node
		{
			public UserGroupComponent userGroup;
		}

		public class CarriedFlagNode : Node
		{
			public TeamGroupComponent teamGroup;

			public BattleGroupComponent battleGroup;

			public FlagInstanceComponent flagInstance;

			public TankGroupComponent tankGroup;
		}

		public class TankNode : Node
		{
			public UserGroupComponent userGroup;

			public TankComponent tank;

			public TankGroupComponent tankGroup;
		}

		[OnEventFire]
		public void ShowFlag(NodeAddedEvent e, [Combine] FlagIndicatorNode flagIndicator, [Context][JoinByUser] TankNode user, [Context][JoinByTank] CarriedFlagNode flag)
		{
			flagIndicator.scoreTableFlagIndicator.SetFlagIconActivity(true);
		}

		[OnEventFire]
		public void HideFlag(NodeRemoveEvent e, CarriedFlagNode flag1, [JoinByTank] TankNode user, [JoinByUser] FlagIndicatorNode flagIndicator, CarriedFlagNode flag2, [JoinByTank] ICollection<CarriedFlagNode> flags)
		{
			if (flags.Count <= 1)
			{
				flagIndicator.scoreTableFlagIndicator.SetFlagIconActivity(false);
			}
		}
	}
}
