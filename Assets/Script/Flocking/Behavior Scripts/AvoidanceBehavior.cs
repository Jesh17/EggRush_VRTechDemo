﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, FlockAgent agentMaster, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //add all points together and average
        Vector3 avoidanceMove = agentMaster.transform.position;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (agent.transform.position - item.position);
            }
            avoidanceMove += item.position;
        }

        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }
        //Debug.Log("Avoidance move is: " + avoidanceMove);
        return avoidanceMove;
    }

}
