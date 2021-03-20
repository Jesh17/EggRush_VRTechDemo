using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    public abstract Vector3 CalculateMove (FlockAgent agent, FlockAgent agentMaster, List<Transform> context, Flock flock);
}
