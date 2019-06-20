using UnityEngine;
using UnityEngine.AI;

public class SkeletonBehaviourComponent : AIBehaviourComponent
{
    public override void act(Vector3 playerPos)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = playerPos; 
        
        Debug.Log($"Act: Skeleton {playerPos}");
    }
}