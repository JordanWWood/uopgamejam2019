using System;
using Unity.Collections;
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
    
    private struct DropData
    {
        [ReadOnly] public ComponentArray<DropDataComponent> dropData;
    }
    [Inject] private DropData _dropData;

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<AIFilter>())
        {
            if (entity.HealthComponent.health <= 0)
            {
                _dropData.dropData[0].CoinsToSpawn.Push(new Tuple<int, Vector3>(20, entity.Transform.position));
                entity.AiActorComponent.room.activeAI.Remove(entity.AiActorComponent.gameObject);
                GameObject.Destroy(entity.AiActorComponent.gameObject);

                continue;
            }
            
            //TODO
        }
    }
}