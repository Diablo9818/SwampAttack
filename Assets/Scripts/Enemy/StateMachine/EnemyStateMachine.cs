using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Player _target;
    private State _currentState;

    public State currentState => _currentState;

    private void Start()
    {
        _target = GetComponent<Enemy>().GetTarget();
        ResetState(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }

        var nextState = _currentState.GetNextState();

        if (nextState != null) 
        {
            Transit(nextState);
        }

    }

    private void Transit(State nextState)
    {
        if(_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.EnterState(_target);
        }
    }

    private void ResetState(State startState)
    {
        _currentState = startState;

        if(_currentState != null)
        {
            _currentState.EnterState(_target);
        }
    }
}
