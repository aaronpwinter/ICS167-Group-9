using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public class State
{
    public enum STATE { WANDERING, SEEKING, FLEEING, ATTACKING, CHASING, GATHERING};

    public enum EVENT { ENTER, UPDATE, EXIT};

    protected STATE currentState;
    protected EVENT currentEvent;
    protected GameObject gameObject;
    protected State nextState;
    protected Vector3 startingPosition;

    public State(GameObject gameObject)
    {
        this.gameObject = gameObject;
        currentEvent = EVENT.ENTER;
        startingPosition = gameObject.transform.position;
    }

    public virtual void enter() {currentEvent = EVENT.UPDATE;}
    public virtual void update() {currentEvent = EVENT.UPDATE;}
    public virtual void exit() {currentEvent = EVENT.EXIT;}

    public State process()
    {
        if (currentEvent == EVENT.ENTER) enter();
        if (currentEvent == EVENT.UPDATE) update();
        if (currentEvent == EVENT.EXIT)
        {
            exit();
            return nextState;
        }
        return this;
    }
}