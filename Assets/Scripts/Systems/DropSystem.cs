using System;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropSystem : ComponentSystem {
    private struct DropFilter
    {
        public DropComponent DropComponents;
    }
    
    private struct PlayerFilter {
        public Transform Transform;
        public PlayerComponent PlayerComponent;
    }
    
    private struct DropData
    {
        [ReadOnly] public ComponentArray<DropDataComponent> dropData;
    }
    [Inject] private DropData _dropData;
    

    
    protected override void OnUpdate() {
        foreach (var drop in GetEntities<DropFilter>()) {
            if (drop.DropComponents.GetComponent<DropComponent>().expires < DateTime.Now) {
                drop.DropComponents.Destroy();
            }
        }
        while (_dropData.dropData[0].CoinsToSpawn.Count > 0)
        {
            Tuple<int, Vector3> spawnInfo = _dropData.dropData[0].CoinsToSpawn.Pop();

            for (int i = spawnInfo.Item1; i > 0; i--) {
                DropComponent c = GameObject.Instantiate(
                    _dropData.dropData[0].CoinPrefab,
                    spawnInfo.Item2 + new Vector3(Random.Range(.0f, .1f), 1, Random.Range(.0f, .1f)),
                    Quaternion.Euler(
                        new Vector3(
                            Random.Range(-6f, 6f), 
                            Random.Range(-6f, 6f),
                            Random.Range(-6f, 6f)
                            )
                        )
                ).gameObject.GetComponent<DropComponent>();

                c.expires = DateTime.Now.AddSeconds(20);
            }
        }
    }
}