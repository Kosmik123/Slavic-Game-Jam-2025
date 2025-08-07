using Data.Camera;
using Unity.Entities;
using UnityEngine;

namespace Authoring.Camera
{
    public class CameraTargetAuthoring : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;

        private class CameraTargetBaker : Baker<CameraTargetAuthoring>
        {
            public override void Bake(CameraTargetAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CameraTargetData { moveSpeed = authoring.moveSpeed });
            }
        }
    }
}
