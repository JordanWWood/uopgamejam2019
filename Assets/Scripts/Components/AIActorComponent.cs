using System.Runtime.CompilerServices;
using UnityEngine;

public class AIActorComponent : MonoBehaviour {
    public GameObject[] targets;

    [HideInInspector] public RoomComponent room;
}