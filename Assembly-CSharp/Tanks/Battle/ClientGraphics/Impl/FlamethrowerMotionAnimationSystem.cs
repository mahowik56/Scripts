using Platform.Kernel.ECS.ClientEntitySystem.API;
using Tanks.Battle.ClientCore.API;
using Tanks.Battle.ClientCore.Impl;

namespace Tanks.Battle.ClientGraphics.Impl
{
	public class FlamethrowerMotionAnimationSystem : ECSSystem
	{
		public class InitialFlamethrowerMotionNode : Node
		{
			public FlamethrowerMotionAnimationComponent flamethrowerMotionAnimation;

			public AnimationPreparedComponent animationPrepared;

			public AnimationComponent animation;
		}

		public class ReadyFlamethrowerMotionNode : Node
		{
			public FlamethrowerMotionAnimationComponent flamethrowerMotionAnimation;

			public FlamethrowerMotionAnimationReadyComponent flamethrowerMotionAnimationReady;

			public AnimationPreparedComponent animationPrepared;

			public AnimationComponent animation;

			public TankGroupComponent tankGroup;
		}

		public class IdleReloadingFlamethrowerMotionNode : Node
		{
			public StreamWeaponIdleComponent streamWeaponIdle;

			public WeaponEnergyReloadingStateComponent weaponEnergyReloadingState;

			public WeaponEnergyComponent weaponEnergy;

			public FlamethrowerMotionAnimationComponent flamethrowerMotionAnimation;

			public FlamethrowerMotionAnimationReadyComponent flamethrowerMotionAnimationReady;

			public AnimationPreparedComponent animationPrepared;

			public AnimationComponent animation;
		}

		public class IdleReloadedFlamethrowerMotionNode : Node
		{
			public StreamWeaponIdleComponent streamWeaponIdle;

			public WeaponEnergyFullStateComponent weaponEnergyFullState;

			public WeaponEnergyComponent weaponEnergy;

			public FlamethrowerMotionAnimationComponent flamethrowerMotionAnimation;

			public FlamethrowerMotionAnimationReadyComponent flamethrowerMotionAnimationReady;

			public AnimationPreparedComponent animationPrepared;

			public AnimationComponent animation;
		}

		public class WorkingFlamethrowerMotionNode : Node
		{
			public StreamWeaponWorkingComponent streamWeaponWorking;

			public FlamethrowerMotionAnimationComponent flamethrowerMotionAnimation;

			public FlamethrowerMotionAnimationReadyComponent flamethrowerMotionAnimationReady;

			public AnimationPreparedComponent animationPrepared;

			public AnimationComponent animation;
		}

		public class ActiveTankNode : Node
		{
			public TankActiveStateComponent tankActiveState;

			public TankGroupComponent tankGroup;
		}

		[OnEventFire]
		public void InitFlamethrowerMotion(NodeAddedEvent evt, InitialFlamethrowerMotionNode weapon)
		{
			weapon.flamethrowerMotionAnimation.Init(weapon.animation.Animator);
			weapon.Entity.AddComponent<FlamethrowerMotionAnimationReadyComponent>();
		}

		[OnEventFire]
		public void StartWorkingMotion(NodeAddedEvent evt, WorkingFlamethrowerMotionNode weapon)
		{
			weapon.flamethrowerMotionAnimation.StartWorkingMotion();
		}

		[OnEventFire]
		public void StartIdleMotion(NodeAddedEvent evt, IdleReloadingFlamethrowerMotionNode weapon)
		{
			weapon.flamethrowerMotionAnimation.StartIdleMotion();
		}

		[OnEventFire]
		public void StopMotion(NodeAddedEvent evt, IdleReloadedFlamethrowerMotionNode weapon)
		{
			weapon.flamethrowerMotionAnimation.StopMotion();
		}

		[OnEventFire]
		public void StopMotion(NodeRemoveEvent evt, ActiveTankNode tank, [JoinByTank] ReadyFlamethrowerMotionNode weapon)
		{
			weapon.flamethrowerMotionAnimation.StopMotion();
		}
	}
}
