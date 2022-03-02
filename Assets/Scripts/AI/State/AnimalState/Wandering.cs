// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : State
{
    private float activityRange;
    private Vector3 startingPos;

    public Wandering(GameObject _gameObject, float activityRange, Vector3 startingPos)
        : base(_gameObject)
    {
        currentState = STATE.WANDERING;
        this.activityRange = activityRange;
        this.startingPos = startingPos;
    }

    public override void enter()
    {
        base.enter();

    }

    public override void update()
    {
        base.update();
        // activityRange is the same as the moveDistance
        Vector3 movement = new Vector3(Random.Range(-activityRange, activityRange), 0, 
            Random.Range(-activityRange, activityRange)).normalized * (activityRange - 1);
        Vector3 destination = movement + gameObject.transform.position;
        // If the destination is outside of the circle the Wandering behavior should be in
        // then change the destination to a normalized vector, which represents the direction 
        // from the center of the range to the destination
        if (Vector3.Distance(destination, startingPos) > activityRange)
        {
            destination = (destination - startingPos).normalized;
            destination *= (activityRange - 1);
        }

        gameObject.GetComponent<UnitMover>().move(destination);
        //gameObject.GetComponent<UnitMover>().moveRel(movement);
    }
}