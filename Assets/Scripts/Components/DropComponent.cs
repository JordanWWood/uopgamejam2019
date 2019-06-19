using System;
using UnityEngine;

public class DropComponent : MonoBehaviour{
    [HideInInspector]
    public DateTime expires;
    [HideInInspector]
    public Vector3 position;

    public void Destroy() {
        Destroy(this.gameObject);
    }
}