using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyAnimationHandler enemyAnimationHandler;
    
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
    }

    
    
    

   
}
