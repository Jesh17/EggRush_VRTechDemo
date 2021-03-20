using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/GrabBehavior")]
public class OnGrabBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, FlockAgent agentMaster, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 grabMove;
        grabMove = agentMaster.transform.position - agent.transform.position;

        return grabMove;
    }
}
