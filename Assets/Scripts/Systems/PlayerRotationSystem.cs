using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerRotationSystem : ComponentSystem
{
    private struct RotationFilter
    {
        public PlayerRotationComponent RotationComponent;
        public Transform Transform;
    }
    
    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<RotationFilter>())
        {
            entity.Transform.LookAt(new Vector3(entity.RotationComponent.mouseWorldPosition.x, 
                1, entity.RotationComponent.mouseWorldPosition.z));

            entity.Transform.eulerAngles = new Vector3(0, entity.Transform.eulerAngles.y, 0);
        }
    }
}