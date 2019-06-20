using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    // Preset variables
    public GameObject[] spawnPoints;
    public GameObject[] aiSpawnPoints;
    public GameObject[] doors;
    
    // Runtime variables
    [HideInInspector] public Vector3 position;
    [HideInInspector] public bool hasBeenActive = false;
    [HideInInspector] public List<GameObject> activeAI;
    [HideInInspector] public bool doorsClosed = false;
    public int depth;
}