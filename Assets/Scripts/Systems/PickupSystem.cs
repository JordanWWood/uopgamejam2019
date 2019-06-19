using Unity.Entities;
using UnityEngine;

public class PickupSystem : ComponentSystem {
    private struct DropFilter {
        public Transform Transform;
        public DropComponent DropComponents;
    }
    
    private struct PlayerFilter {
        public Transform Transform;
        public PlayerComponent PlayerComponent;
        public GoldComponent GoldComponent;
    }
    
    protected override void OnUpdate() {
        PlayerFilter player = GetEntities<PlayerFilter>()[0];
        foreach (var pickup in GetEntities<DropFilter>()) {
            Vector3 sub = player.Transform.position - pickup.Transform.position;
            if(Vector3.SqrMagnitude(sub) <= pickup.DropComponents.pickupDistance*pickup.DropComponents.pickupDistance) {
                switch (pickup.DropComponents.dropType) {
                    case DropType.COIN:
                        player.GoldComponent.score++;
                        pickup.DropComponents.Destroy();
                        break;
                }
            }
        }
    }
}