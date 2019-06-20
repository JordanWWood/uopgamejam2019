using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Random = System.Random;

public class AISpawnSystem : ComponentSystem
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
            foreach (var room in  GetEntities<RoomFilter>())
            {
                if (room.RoomComponent.hasBeenActive) continue;

                var sub = player.Transform.position - room.Transform.position;
                if (Vector3.SqrMagnitude(sub) > 12*12) continue;
                if (room.RoomComponent.depth == 0) continue;
                
                var amount = (float) room.RoomComponent.depth / 2;
                var chance = (int) (amount * 100);
                if (chance > 100) chance /= room.RoomComponent.depth;

                foreach (var point in room.RoomComponent.aiSpawnPoints) {
                    var r = new Random();
                    if (chance % 100 != 0 && NextBool(r, chance)) 
                        room.RoomComponent.activeAI.Add(spawnRandomEntity(point.transform.position, room.RoomComponent));
                    
                    if (amount < 1) continue;
                    for (int i = 0; i < (int) amount; i++)
                        room.RoomComponent.activeAI.Add(spawnRandomEntity(point.transform.position, room.RoomComponent));
                    
                }
                
                room.RoomComponent.hasBeenActive = true;
            }
        }
    }

    private GameObject spawnRandomEntity(Vector3 location, RoomComponent roomComponent) {
        var random = UnityEngine.Random.Range(0, _aiData.AIData[0].AIPrefabs.Length);
        GameObject gameObject = GameObject.Instantiate(_aiData.AIData[0].AIPrefabs[random], location, Quaternion.identity);
        gameObject.GetComponent<AIActorComponent>().room = roomComponent;

        return gameObject;
    }
    
    private static bool NextBool(Random r, int truePercentage = 50) {
        return r.NextDouble() < truePercentage / 100.0;
    }
}