using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class AISystem : ComponentSystem
{
    private struct PlayerFilter
    {
        public Transform Transform;
        public PlayerComponent PlayerComponent;
    }

    private struct RoomFilter
    {
        public Transform Transform;
        public RoomComponent RoomComponent;
    }
    
    private struct AiData
    {
        [ReadOnly] public ComponentArray<AIDataComponent> AIData;
    }

    [Inject] private AiData _aiData;
    
    protected override void OnUpdate()
    {
        foreach (var player in GetEntities<PlayerFilter>())
        {
            var rooms = GetEntities<RoomFilter>();
            Debug.Log(rooms.Length);
            foreach (var room in rooms)
            {
                if (room.RoomComponent.hasBeenActive) continue;
                if (Vector3.Distance(player.Transform.position, room.Transform.position) > 12.5) continue;
                room.RoomComponent.hasBeenActive = true;
                Debug.Log("Room active");
            }
        }
    }
}