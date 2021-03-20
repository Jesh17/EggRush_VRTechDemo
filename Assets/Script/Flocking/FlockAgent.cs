using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(SphereCollider))]
public class FlockAgent : MonoBehaviour
{
    public Transform handGrab = null;
    public List<FlockAgent> inHandAgents = new List<FlockAgent>();
    private Flock myFlock;
    SphereCollider agentCollider;

    private XRGrabInteractable grabInteractable = null;
    public Rigidbody rb;
    public bool isMaster = false;
    public float D;
    public Vector3 move = Vector3.zero;
    ExampleListener eventListener;

    public SphereCollider AgentCollider { get { return agentCollider; } }

    float baseGrip = 0.01f;
    float gripVal = 0f;
    float gripTime = 0f;
    bool grabbing = false;
    int flockCap;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        myFlock = GameObject.Find("Flock").GetComponent<Flock>();
        eventListener = GameObject.Find("ExampleListener").GetComponent<ExampleListener>();

        flockCap = myFlock.grabCap;

        grabInteractable.onHoverEntered.AddListener(SetHand);
        grabInteractable.onHoverExited.AddListener(ClearHand);
        grabInteractable.onSelectEntered.AddListener(SetMaster);
        grabInteractable.onSelectExited.AddListener(ClearMaster);
        eventListener.gripHandler.OnValueChange += GetGripValue;
    }

    void OnDestroy()
    {
        grabInteractable.onHoverEntered.RemoveListener(SetHand);
        grabInteractable.onHoverExited.RemoveListener(ClearHand);
        grabInteractable.onSelectEntered.RemoveListener(SetMaster);
        grabInteractable.onSelectExited.RemoveListener(ClearMaster);
        eventListener.gripHandler.OnValueChange -= GetGripValue;
    }

    void Start()
    {
        agentCollider = GetComponent<SphereCollider>();
    }

    public void Move(Vector3 velocity)
    {
        //transform.forward = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
        
    }

    void SetHand(XRBaseInteractor interactor)
    {
        handGrab = interactor.transform;
        Debug.Log("Hover has started.");
    }

    void ClearHand(XRBaseInteractor interactor)
    {
        handGrab = null;
        Debug.Log("Hover has ended.");
    }

    void SetMaster(XRBaseInteractor interactor)
    {
        if (myFlock.agentMaster == null)
        {
            grabbing = true;
            myFlock.agentMaster = this;
            isMaster = true;
            Debug.Log("Master set.");
        }

    }

    void ClearMaster(XRBaseInteractor interactor)
    {
        grabbing = false;
        gripTime = 0f;
        if (isMaster)
        {
            if (inHandAgents.Count > 0)
            {
                int tempCount = inHandAgents.Count;
                for (int i = 0; i < tempCount; i++)
                {

                    inHandAgents[0].rb.useGravity = true;
                    inHandAgents.Remove(inHandAgents[0]);
                    //Debug.Log("Removed object: " + inHandAgents[i].gameObject);
                }
            }
            rb.useGravity = true;
            myFlock.grabCap = flockCap;
            myFlock.agentMaster = null;
            isMaster = false;
            Debug.Log("Master clear.");
        }

    }

    

    void Update()
    {

        if (grabbing)
        {
            if (gripVal > baseGrip)
            {
                gripTime += Time.deltaTime;
                if (gripVal > 0.09f)
                {
                    myFlock.grabCap = ((int)(myFlock.grabCap * ((gripTime) * 75f)));
                    Debug.Log("Grab cap: " + myFlock.grabCap);
                    Debug.Log("time: " + gripTime * 75f);
                    grabbing = false;
                }
            }
        }
    }

    void GetGripValue(XRBaseController controller, float val)
    {
        gripVal = val;
        //Debug.Log("My grip is:" + val);
    }
}
