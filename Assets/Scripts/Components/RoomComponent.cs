using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    [HideInInspector]
    public Vector3 position;
    public GameObject[] spawnPoints;
    public int depth;
}