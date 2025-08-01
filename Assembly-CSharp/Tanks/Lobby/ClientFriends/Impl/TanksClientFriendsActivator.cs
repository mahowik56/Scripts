using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientUnityIntegration;
using Platform.Library.ClientUnityIntegration.API;
using Tanks.Lobby.ClientFriends.API;

namespace Tanks.Lobby.ClientFriends.Impl
{
	public class TanksClientFriendsActivator : UnityAwareActivator<AutoCompleting>, ECSActivator, Activator
	{
		[Inject]
		public static TemplateRegistry TemplateRegistry { get; set; }

		public void RegisterSystemsAndTemplates()
		{
			TemplateRegistry.Register<FriendsScreenTemplate>();
			TemplateRegistry.Register<FriendSentNotificationTemplate>();
			ECSBehaviour.EngineService.RegisterSystem(new FriendsKeeperSystem());
			ECSBehaviour.EngineService.RegisterSystem(new FriendsBuilderSystem());
			ECSBehaviour.EngineService.RegisterSystem(new FriendsListSystem());
			ECSBehaviour.EngineService.RegisterSystem(new LobbyFriendsScreenSystem());
			ECSBehaviour.EngineService.RegisterSystem(new UserLabelFriendsSystem());
			ECSBehaviour.EngineService.RegisterSystem(new DisplayProfileScreenHeaderSystem());
			ECSBehaviour.EngineService.RegisterSystem(new FriendsActionsOnProfileScreenSystem());
			ECSBehaviour.EngineService.RegisterSystem(new FriendsBattleShowSystem());
			ECSBehaviour.EngineService.RegisterSystem(new FriendsScreenSystem());
			ECSBehaviour.EngineService.RegisterSystem(new InviteFriendsPopupSystem());
			ECSBehaviour.EngineService.RegisterSystem(new FriendsBadgeSystem());
			ECSBehaviour.EngineService.RegisterSystem(new FriendInteractionSystem());
			ECSBehaviour.EngineService.RegisterSystem(new WaitingForInviteAnswerSystem());
		}

		protected override void Activate()
		{
		}
	}
}
