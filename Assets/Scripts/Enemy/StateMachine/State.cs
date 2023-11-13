using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Target {  get;  set; }

    public void EnterState(Player target)
    {
        if(enabled == false)
        {
            Target = target;
            enabled = true;

            foreach(Transition transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public State GetNextState()
    {
        foreach(Transition transition in _transitions)
        {
            if (transition.DoesNeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }

    public void ExitState()
    {
        if(enabled == true)
        {
            foreach(Transition transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }
}
