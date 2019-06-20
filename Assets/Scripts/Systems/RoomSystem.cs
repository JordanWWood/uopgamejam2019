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
        
        List<RoomComponent> populateList = new List<RoomComponent>();
        foreach (var point in component.spawnPoints)
        {
            var position = point.transform.position;
            bool duplicate = false;
            foreach (var currentRoom in _currentRooms)
            {
                if (Vector3.Distance(currentRoom.Key, position) < 20)
                {
                    duplicate = true;
                    break;
                }
            }
            if(duplicate) continue;

            var yay = rollRoom(position, component.position, iteration + 1);
            if (yay == null) return;
            populateList.Add(yay);
        }

        foreach (var entity in populateList)
            populate(entity, depth, iteration + 1);
    }

    private GameObject createRoom(int index, Vector3 location)
    {
        var room = _roomData.RoomComponents[0].rooms[index];
        room.GetComponent<RoomComponent>().position = location;
        room = GameObject.Instantiate(room, location, Quaternion.identity);
        _currentRooms.Add(location, room);
        return room;
    }

    
    private RoomComponent rollRoom(Vector3 newPos, Vector3 oldPos, int depth, int cycles = 0)
    {
        if (cycles > 10) return null;
        
        int rand = Random.Range(0, _roomData.RoomComponents[0].rooms.Length);

        GameObject gameObject = createRoom(rand, newPos);
        RoomComponent newRoomComponent = gameObject.GetComponent<RoomComponent>();
        newRoomComponent.depth = depth;

        foreach (var newPoint in newRoomComponent.spawnPoints)
        {
            if (Vector3.Distance((newPoint.transform.position), oldPos) > 10)
                continue;

            return newRoomComponent;
        }

        Debug.Log("Room destroyed");
        _currentRooms.Remove(gameObject.transform.position);
        GameObject.Destroy(gameObject);
        return rollRoom(newPos, oldPos, depth, cycles + 1);
    }
}
