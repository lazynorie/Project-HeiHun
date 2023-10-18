using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
   [SerializeField]protected StateMachineManager stateMachineManager;
   
   public abstract State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimationHandler anim);

   protected void AssignStateMachineManager()
   {
      stateMachineManager = GetComponentInParent<StateMachineManager>();
   }
}
