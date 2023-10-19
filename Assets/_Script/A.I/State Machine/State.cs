using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class State : MonoBehaviour
{
   [Header("State Machine manager")]
   [SerializeField]protected StateMachineManager stateMachineManager;
   
   public abstract State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimationHandler anim);

   protected void AssignStateMachineManager()
   {
      stateMachineManager = GetComponentInParent<StateMachineManager>();
   }
}
