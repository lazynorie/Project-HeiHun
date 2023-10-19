using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    [SerializeField] private State[] states;
    public IdleState idleState;
    public PursueTargetState pursueTargetState;
    public CombatStanceState combatStanceState;
    public AttackState attackState;
    public AmbushState ambushState;
    
    public void Initialize()
    {
        states = GetComponentsInChildren<State>();
        AssignStates();
    }
    private void AssignStates()
    {
        foreach (State state in states)
        {
            if (state is IdleState)
            {
                idleState = (IdleState)state;
            }
            else if (state is PursueTargetState)
            {
                pursueTargetState = (PursueTargetState)state;
            }
            else if (state is CombatStanceState)
            {
                combatStanceState = (CombatStanceState)state;
            }
            else if (state is AttackState)
            {
                attackState = (AttackState)state;
            }
            else if (state is AmbushState)
            {
                ambushState = (AmbushState)state;
            }
           
        }
    }
    
}
