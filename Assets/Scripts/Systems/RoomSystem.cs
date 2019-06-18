using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class RoomSystem : ComponentSystem
{
    private struct ExistingRoomFilter
    {
        public RoomComponent RoomComponent;
    }
    
    private struct RoomData
    {
        [ReadOnly] public ComponentArray<RoomDataComponent> RoomComponents;
    }

    [Inject] private RoomData _roomData;

    private bool first = true;
    private Dictionary<Vector3, GameObject> _currentRooms = new Dictionary<Vector3, GameObject>();
    
    protected override void OnUpdate()
    {
        if (first)
        {
            GameObject firstRoom = createRoom(0, new Vector3());
            populate(firstRoom.GetComponent<RoomComponent>(), _roomData.RoomComponents[0].genDepth, 0);
            
            first = false;
        }
    }
    private void populate(RoomComponent component, int depth, int iteration) {
        if (iteration >= depth) return;
        
        foreach (var point in component.spawnPoints)
        {
            var position = point.transform.position;
            if (_currentRooms.ContainsKey(position)) continue;
            populate(rollRoom(position, component.position), depth, iteration + 1);
        }
    }

    private GameObject createRoom(int index, Vector3 location)
    {
        var room = _roomData.RoomComponents[0].rooms[index];
        room.GetComponent<RoomComponent>().position = location;
        room = GameObject.Instantiate(room, location, Quaternion.identity);
        _currentRooms.Add(location, room);
        return room;
    }

    private RoomComponent rollRoom(Vector3 newPos, Vector3 oldPos)
    {
        int rand = Random.Range(0, _roomData.RoomComponents.Length - 1);
        GameObject gameObject = createRoom(rand, newPos);
        RoomComponent newRoomComponent = gameObject.GetComponent<RoomComponent>();

        bool fits = false;
        foreach (var newPoint in newRoomComponent.spawnPoints)
        {
            if (newPoint.transform.position != oldPos) continue;
            
            Debug.Log("Pos matches previous room, tile fits");
            return newRoomComponent;
        }

        GameObject.Destroy(gameObject);
        return rollRoom(newPos, oldPos);
    }
}
