using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] aiSpawnPoints;
    public GameObject[] doors;
    
    [HideInInspector] public Vector3 position;
    [HideInInspector] public bool hasBeenActive = false;
    public int depth;
}