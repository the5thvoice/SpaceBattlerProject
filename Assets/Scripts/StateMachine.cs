using UnityEngine;
using System.Collections;

/// <summary>
/// State machine that conrols the current state
/// </summary>
public class StateMachine : MonoBehaviour
{

    public State CurrentState;

    


    public void Update()
    {

        if (CurrentState != null)
        {
            CurrentState.Update();
        }
        
    }

    /// <summary>
    /// sets the current state to the submited state
    /// </summary>
    /// <param name="NewState"></param>
    public void SwitchState(State NewState)
    {

        if(CurrentState != null)
            CurrentState.Exit();

        CurrentState = NewState;

        if (NewState != null)
        {
            CurrentState.Enter();
        }

    }
    

}
