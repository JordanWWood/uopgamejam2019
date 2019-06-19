using System;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class DropSystem : ComponentSystem {
    private struct DropFilter
    {
        public DropComponent DropComponents;
    }
    
    private struct DropData
    {
        [ReadOnly] public ComponentArray<DropDataComponent> dropData;
    }
    
    private struct PlayerFilter {
        public Transform Transform;
        public PlayerComponent PlayerComponent;
    }

    [Inject] private DropData _dropData;
    
    protected override void OnUpdate() {
        foreach (var drop in GetEntities<DropFilter>()) {
            Debug.Log($"loop {drop.DropComponents.GetComponent<DropComponent>().expires}");
            if (drop.DropComponents.GetComponent<DropComponent>().expires < DateTime.Now) {
                drop.DropComponents.Destroy();
            }
        }
        if (Input.GetKey(KeyCode.F9)) //TODO REMOVE
        {
            for (int i = 0; i < 1; i++) {
                DropComponent c = GameObject.Instantiate(
                    _dropData.dropData[0].DropPrefabs[0],
                    GetEntities<PlayerFilter>()[0].Transform.position + new Vector3(0, 2, 0),
                    Quaternion.identity
                ).gameObject.GetComponent<DropComponent>();

                c.expires = DateTime.Now.AddSeconds(30);
            }
        }
    }
}