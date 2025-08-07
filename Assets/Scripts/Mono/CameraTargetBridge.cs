using Data.Camera;
using Helpers.Network;
using Unity.Transforms;
using UnityEngine;

namespace Mono
{
	public class CameraTargetBridge : MonoBehaviour
	{
		private void Update()
		{
			var clientWorld = WorldHelper.GetClientWorld();
			if (clientWorld == null)
				return;

			var entityManager = clientWorld.EntityManager;
			using var query = entityManager.CreateEntityQuery(typeof(CameraTargetData));
			if (query.IsEmpty)
				return;

			var localTransform = entityManager.GetComponentData<LocalTransform>(query.GetSingletonEntity());
			transform.SetPositionAndRotation(localTransform.Position, localTransform.Rotation);
		}
	}
}
