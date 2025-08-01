using Platform.Kernel.ECS.ClientEntitySystem.API;
using UnityEngine;

namespace Tanks.Battle.ClientCore.Impl
{
	public class ImpactEvent : Platform.Kernel.ECS.ClientEntitySystem.API.Event
	{
		private Vector3 localHitPoint;

		private Vector3 force;

		private float weakeningCoeff;

		public Vector3 LocalHitPoint
		{
			get
			{
				return localHitPoint;
			}
			set
			{
				localHitPoint = ((!ValidateImpactData(value)) ? Vector3.zero : value);
			}
		}

		public Vector3 Force
		{
			get
			{
				return force;
			}
			set
			{
				force = ((!ValidateImpactData(value)) ? Vector3.zero : value);
			}
		}

		public float WeakeningCoeff
		{
			get
			{
				return weakeningCoeff;
			}
			set
			{
				weakeningCoeff = ((!PhysicsUtil.IsValidFloat(value)) ? 0f : value);
			}
		}

		public static bool ValidateImpactData(Vector3 force)
		{
			return PhysicsUtil.IsValidVector3(force);
		}
	}
}
