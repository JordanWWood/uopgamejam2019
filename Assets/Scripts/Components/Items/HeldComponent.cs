using UnityEngine;

public abstract class HeldComponent : MonoBehaviour {
    public abstract void action(Vector3 location, Vector3 forwardVector);
}