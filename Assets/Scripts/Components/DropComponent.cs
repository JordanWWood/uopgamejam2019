using System;
using UnityEngine;

public class DropComponent : MonoBehaviour{
    [HideInInspector]
    public DateTime expires;
    [HideInInspector]
    public Vector3 position;

    public DropType dropType;
    public float pickupDistance;

    public void Destroy() {
        Destroy(this.gameObject);
    }
}

[Serializable]
public enum DropType {
    COIN,
    WEAPON,
    ITEM
}