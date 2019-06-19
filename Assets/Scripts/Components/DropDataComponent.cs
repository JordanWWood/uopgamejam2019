using System;
using System.Collections.Generic;
using UnityEngine;

public class DropDataComponent : MonoBehaviour {
    public GameObject CoinPrefab;
    public GameObject[] WeaponPrefabs;
    public GameObject[] ItemPrefabs;
    
    [HideInInspector]
    public Stack<Tuple<int, Vector3>> CoinsToSpawn = new Stack<Tuple<int, Vector3>>();
}