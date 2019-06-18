using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class RoomSystem : ComponentSystem
{
    private bool first = true;
    
    private struct RoomData
    {
        public ComponentArray<RoomComponent> RoomComponents;
    }

    [Inject] private RoomData _roomData;

    protected override void OnStartRunning()
    {
//        var startRoom = _roomData.RoomComponents[0].rooms[0];
//        GameObject.Instantiate(startRoom, new Vector3(), Quaternion.identity);
//        
//        Debug.Log("Instantiate Object");
    }

    protected override void OnUpdate()
    {
        if (first)
        {
            var startRoom = _roomData.RoomComponents[0].rooms[0];
            GameObject.Instantiate(startRoom, new Vector3(), Quaternion.identity);

            Debug.Log("Instantiate Object");

            first = false;
        }
    }
}