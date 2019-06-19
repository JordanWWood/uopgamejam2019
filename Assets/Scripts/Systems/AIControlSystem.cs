using Unity.Entities;
using UnityEngine;

public class AIControlSystem : ComponentSystem
{
    private struct AIFilter
    {
        public Transform Transform;
        public AIActorComponent AiActorComponent;
        public HealthComponent HealthComponent;
        public Rigidbody Rigidbody;
    }

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<AIFilter>())
        {
            if (entity.HealthComponent.health <= 0)
            {
                entity.AiActorComponent.room.activeAI.Remove(entity.AiActorComponent.gameObject);
                GameObject.Destroy(entity.AiActorComponent.gameObject);
                
                continue;
            }
            
            //TODO
        }
    }
}