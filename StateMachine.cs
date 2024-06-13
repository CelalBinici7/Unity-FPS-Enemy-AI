using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

   public PatrolState patrolState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState) 
    {
        //exit the current situation
        if (activeState!= null)
        {
            activeState.Exit();
        }
        //chane to a new state
        activeState = newState; 

        if (activeState != null )
        {
            // Setup new State
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }

    }

    public void Initialise()
    {
       patrolState = new PatrolState();
        ChangeState(patrolState);
    }
   
}
