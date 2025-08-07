using Data.Camera;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace System.Client
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
    public partial struct MoveCameraSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CameraTargetData>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (localTransform, cameraData)
                     in SystemAPI.Query<RefRW<LocalTransform>, RefRO<CameraTargetData>>())
            {
                var xInput = Input.GetAxisRaw("Horizontal");
                var yInput = Input.GetAxisRaw("Vertical");

                float3 flatForward = GetFlatDirection(localTransform.ValueRO.Forward());
                float3 flatRight = GetFlatDirection(localTransform.ValueRO.Right());
                float3 movementVector = xInput * flatRight + yInput * flatForward;
                float moveSpeed = cameraData.ValueRO.moveSpeed;

                localTransform.ValueRW.Position += moveSpeed * SystemAPI.Time.DeltaTime * math.normalizesafe(movementVector);
            }
        }

        private static float3 GetFlatDirection(float3 direction)
        {
            direction.y = 0;
            return math.normalize(direction);
        }
    }
}
