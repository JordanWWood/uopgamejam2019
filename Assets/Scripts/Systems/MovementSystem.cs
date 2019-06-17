using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MovementSystem : ComponentSystem {
    private struct MovementFilter {
        public Transform Transform;
        public MovementComponent MovementComponent;
    }
    
    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<MovementFilter>())
        {
            entity.Transform.position += new Vector3(-entity.MovementComponent.direction.y * entity.MovementComponent.speed, 
                                            0, 
                                            entity.MovementComponent.direction.x * entity.MovementComponent.speed);

            Debug.Log(entity.MovementComponent.direction);
        }
    }
}
