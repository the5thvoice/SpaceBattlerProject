using UnityEngine;
using System.Collections;

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
