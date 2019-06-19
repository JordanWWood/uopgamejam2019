using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PickupSystem : ComponentSystem {
    private struct DropFilter {
        public Transform Transform;
        public DropComponent DropComponent;
    }
    
    private struct PlayerFilter {
        public Transform Transform;
        public PlayerComponent PlayerComponent;
        public GoldComponent GoldComponent;
    }
    
    protected override void OnUpdate() {
        List<DropComponent> toDelete = new List<DropComponent>();
        PlayerFilter player = GetEntities<PlayerFilter>()[0];
        foreach (var pickup in GetEntities<DropFilter>()) {
//            Vector3 sub = player.Transform.position - pickup.Transform.position;
            if(Vector3.Distance(player.Transform.position, pickup.Transform.position) <= pickup.DropComponent.pickupDistance) {
                switch (pickup.DropComponent.dropType) {
                    case DropType.COIN:
                        player.GoldComponent.score++;
                        toDelete.Add(pickup.DropComponent);
                        break;
                }
            }
        }
        
        foreach (var dropComponent in toDelete) {
            dropComponent.Destroy();
        }
    }
}