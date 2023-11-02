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
    public RotateState rotateState;
    
    public void Initialize()
    {
        states = GetComponentsInChildren<State>();
        AssignStates();
    }
    private void AssignStates()
    {
        foreach (State state in states)
        {
            switch (state)
            {
                case IdleState:
                    idleState = (IdleState)state;
                    break;
                case PursueTargetState:
                    pursueTargetState = (PursueTargetState)state;
                    break;
                case CombatStanceState:
                    combatStanceState = (CombatStanceState)state;
                    break;
                case AttackState:
                    attackState = (AttackState)state;
                    break;
                case AmbushState:
                    ambushState = (AmbushState)state;
                    break;
                case RotateState :
                    rotateState = (RotateState)state;
                    break;
            }
        }
    }
    
}
