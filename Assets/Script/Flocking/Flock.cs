using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    public Transform flockParent;
    public FlockAgent agentMaster = null;
    public float reach = 1f;
    public int grabCap = 0;


    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(0f, 5f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    //Utility Vars; gets rid of heavy math at start
    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    void Awake()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareAvoidanceRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        /*for(int i = 0; i < startingCount; i++){
        	FlockAgent newAgent = Instantiate(
        		agentPrefab,
        		Random.insideUnitCircle * startingCount * AgentDensity,
        		Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
        		transform
        		);
        	newAgent.name = "Agent" + i;
        	agents.Add(newAgent);
        }*/
        Transform[] flockChildren = flockParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in flockChildren)
        {
            if (child != flockChildren[0])
            {
                FlockAgent newAgent = child.GetComponent<FlockAgent>();
                agents.Add(newAgent);
            }
        }
    }


    void Update()
    {
        if (agentMaster != null)
        {

            foreach (FlockAgent agent in agents)
            {
                agent.D = Vector3.Distance(agentMaster.transform.position, agent.transform.position);
                

                if (agent.D < reach && agent != agentMaster)
                {
                    List<Transform> context = GetNearbyObjects(agent);
                    agent.move = behavior.CalculateMove(agent, agentMaster, context, this);
                    agent.move *= driveFactor;
                    if (agent.move.sqrMagnitude > squareMaxSpeed)
                    {
                        agent.move = agent.move.normalized * maxSpeed;
                    }

                    if(!agentMaster.inHandAgents.Contains(agent)){
                        agentMaster.inHandAgents.Add(agent);
                    }

                }
                else { agent.rb.useGravity = true; agentMaster.inHandAgents.Remove(agent); }

                if (agent == agentMaster && agentMaster.inHandAgents.Count > 0)
                {
                    agentMaster.inHandAgents = agentMaster.inHandAgents.OrderBy(x => x.D).ToList();
                    for (int i = 0; i < grabCap; i++)
                    {
                        if (i < agentMaster.inHandAgents.Count)
                        {
                            agentMaster.inHandAgents[i].Move(agentMaster.inHandAgents[i].move);
                            agentMaster.inHandAgents[i].rb.useGravity = false;
                            //agentMaster.inHandAgents.Add(agent);
                        }
                    }
                }
            }

        }
    }


    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
