using Unity.Entities;
using UnityEngine;

public class ShootSystem : ComponentSystem {
    private struct ShootFilter
    {
        public Transform Transform;
        public ShootComponent ShootComponent;
    }

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<ShootFilter>()) {
            if (!entity.ShootComponent.isFiring) continue;

            var gun = entity.ShootComponent.gun;
            HeldComponent heldComponent = gun.GetComponent<HeldComponent>();
            
            heldComponent.action(entity.Transform.position, -(entity.ShootComponent.gun.transform.rotation * Vector3.forward));
        }
    }
}