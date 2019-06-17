using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RoomSystem : ComponentSystem {
    private struct RoomData
    {
        public int Length;
        public ComponentArray<RoomComponent> RoomComponents;
    }

    [Inject] private RoomData _roomData;
    
    protected override void OnUpdate()
    {
        //TODO
    }
}
