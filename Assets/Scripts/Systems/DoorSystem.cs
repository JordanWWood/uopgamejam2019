using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class DoorSystem : ComponentSystem
{
    private struct RoomFilter
    {
        public RoomComponent RoomComponent;
    }

    private struct GoldData
    {
        public ComponentArray<GoldComponent> goldComponents;
    }

    [Inject] private GoldData goldData;

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<RoomFilter>())
        {
            if (!entity.RoomComponent.hasBeenActive) continue;
            if (entity.RoomComponent.activeAI.Count > 0 && !entity.RoomComponent.doorsClosed)
            {
                entity.RoomComponent.doorsClosed = true;

                foreach (var door in entity.RoomComponent.doors)
                    SetDoorVerticalPosition(door, 0.7f);
            }

            if (entity.RoomComponent.activeAI.Count <= 0 && entity.RoomComponent.doorsClosed)
            {
                entity.RoomComponent.doorsClosed = false;
                goldData.goldComponents[0].roomsCleared += 1;
                goldData.goldComponents[0].scoreText.GetComponent<Text>().text = $"Cleared: {goldData.goldComponents[0].roomsCleared}";
                
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