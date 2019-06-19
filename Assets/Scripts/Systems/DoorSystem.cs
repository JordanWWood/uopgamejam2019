using Unity.Entities;
using UnityEngine;

public class DoorSystem : ComponentSystem
{
    private struct RoomFilter
    {
        public RoomComponent RoomComponent;
    }
    
    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<RoomFilter>())
        {
            if (!entity.RoomComponent.hasBeenActive) continue;
            if (entity.RoomComponent.activeAI.Count > 0 && !entity.RoomComponent.doorsClosed) {
                entity.RoomComponent.doorsClosed = true;

                foreach (var door in entity.RoomComponent.doors)
                  SetDoorVerticalPosition(door, 0.7f);
            }

            if (entity.RoomComponent.activeAI.Count <= 0 && entity.RoomComponent.doorsClosed)
            {
                entity.RoomComponent.doorsClosed = false;
                
                foreach (var door in entity.RoomComponent.doors)
                    SetDoorVerticalPosition(door, 3.1f);
            }
        }
    }

    private void SetDoorVerticalPosition(GameObject door, float y)
    {
        var position = door.transform.position;
        position = new Vector3(position.x, y, position.z);
        door.transform.position = position;
    }
}