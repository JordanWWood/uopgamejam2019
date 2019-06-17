using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MovementSystem : ComponentSystem {
    private struct MovementFilter {
        public Rigidbody Rigidbody;
        public MovementComponent MovementComponent;
    }
    
    protected override void OnUpdate() {
        foreach (var entity in GetEntities<MovementFilter>())
            entity.Rigidbody.velocity = new Vector3 (-entity.MovementComponent.direction.y, 0,
                                            entity.MovementComponent.direction.x) * entity.MovementComponent.speed;
    }
}
