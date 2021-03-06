//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitMover : UnitBase
{
    //This class implements moving for other units, does not have an action,
    //  and thus cannot be used alone.

    //Can move a maximum of moveDistance each round
    [SerializeField] private float moveDistance;
    protected Vector3 roundStartLocation;

    private NavMeshAgent myAgent;
    public const float AGENT_SPEED = 10f;
    public const float AGENT_ACC = 100f;
    public const float AGENT_TURN = 1000000f;
    public const float AGENT_STOP_DIST = .5f;

    public override void readyAction()
    {
        stop();
        base.readyAction();
        roundStartLocation = transform.position;
    }

    public override void endTurn()
    {
        stop();
        base.endTurn();
    }

    //Returns whether you can move there (in case out of range or don't have action).
    public virtual bool move(Vector3 loc)
    {
        if (!actionAvailable) return false;
        if (Vector3.Distance(roundStartLocation, loc) > moveDistance) return false;
        //Movement code here :)
        myAgent.SetDestination(loc);

        return true;
    }

    public virtual bool moveRel(Vector3 locRel)
    {
        if (!actionAvailable) return false;
        if (locRel.magnitude > moveDistance) return false;
        //Movement code here :)
        myAgent.SetDestination(locRel+roundStartLocation);

        return true;
    }

    public virtual void stop()
    { //Full stops. No smooth acceleration. To be used when an action is taken, for instance.
        if (myAgent)
        {
            myAgent.ResetPath();
            myAgent.velocity = Vector3.zero;
        }
    }

    public void setMoveDistance(float m)
    {
        moveDistance = m;
    }

    public float getMoveDistance()
    {
        return moveDistance;
    }

    public Vector3 getRoundStartLocation()
    {
        return roundStartLocation;
    }

    protected override void animate()
    {
        base.animate();
        float curSpeed = myAgent.velocity.magnitude;

        myAnimator.SetFloat("Speed_f", curSpeed);
        myAnimator.SetBool("Static_b", curSpeed < .01f);
    }


    protected override void Start()
    {
        base.Start();
        roundStartLocation = transform.position;
        
        //NavMeshAgent settings
        myAgent = gameObject.GetComponent<NavMeshAgent>();
        myAgent.speed = AGENT_SPEED;
        myAgent.acceleration = AGENT_ACC;
        myAgent.angularSpeed = AGENT_TURN;
        myAgent.stoppingDistance = AGENT_STOP_DIST;


    }

    protected override void Update()
    {
        base.Update();
    }
}
