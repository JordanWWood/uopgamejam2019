using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviourComponent : AIBehaviourComponent
{
    public override void act(Vector3 playerPos)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = playerPos;
    }
}