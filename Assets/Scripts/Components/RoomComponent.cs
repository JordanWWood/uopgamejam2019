using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] aiSpawnPoints;
    public GameObject[] doors;
    
    [HideInInspector] public Vector3 position;
    [HideInInspector] public bool hasBeenActive = false;
    [HideInInspector] public List<GameObject> activeAI;
    [HideInInspector] public bool doorsClosed = false;
    [HideInInspector] public int depth;
}