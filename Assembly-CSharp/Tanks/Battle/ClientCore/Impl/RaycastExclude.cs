using System;
using System.Collections.Generic;
using System.Linq;
using Tanks.Battle.ClientCore.API;
using UnityEngine;

namespace Tanks.Battle.ClientCore.Impl
{
	public struct RaycastExclude : IDisposable
	{
		private int[] initialLayers;

		private IEnumerable<GameObject> gameObjects;

		public RaycastExclude(IEnumerable<GameObject> gameObjects)
		{
			initialLayers = null;
			this.gameObjects = gameObjects;
			if (gameObjects != null)
			{
				ExcludeGameObjectsFromRaycast();
			}
		}

		public void Dispose()
		{
			if (gameObjects != null)
			{
				ReturnGameObjectsLayers();
			}
		}

		private void ExcludeGameObjectsFromRaycast()
		{
			int num = 0;
			initialLayers = new int[gameObjects.Count()];
			IEnumerator<GameObject> enumerator = gameObjects.GetEnumerator();
			while (enumerator.MoveNext())
			{
				GameObject current = enumerator.Current;
				initialLayers[num++] = current.layer;
				current.layer = Layers.EXCLUSION_RAYCAST;
			}
		}

		private void ReturnGameObjectsLayers()
		{
			int num = 0;
			IEnumerator<GameObject> enumerator = gameObjects.GetEnumerator();
			while (enumerator.MoveNext())
			{
				GameObject current = enumerator.Current;
				current.layer = initialLayers[num++];
			}
		}
	}
}
