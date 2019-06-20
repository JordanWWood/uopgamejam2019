using UnityEngine;

public abstract class AIBehaviourComponent : MonoBehaviour
{
    public abstract void act(Vector3 playerPos);

    public void faceForward()
    {
        
    }
}