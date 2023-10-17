using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyLocomotionManager : MonoBehaviour
{
    private EnemyManger enemyManger;
    
    [SerializeField]
    public CharacterStats currentTarget;
    [SerializeField] private LayerMask detectionLayer;

    private void Awake()
    {
        enemyManger = GetComponent<EnemyManger>();
    }

    private void Update()
    {
        HandleDetection();
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManger.detectionRadius, detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                //check for team ID
                Vector3 relativeDir = transform.TransformDirection(characterStats.transform.position);
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewbleAngle = Vector3.Angle(targetDirection, transform.forward);
                if (viewbleAngle >enemyManger.minimumDetectionAngle && viewbleAngle <enemyManger.maximumDetectionAngle)
                {
                    currentTarget = characterStats;
                }
                else
                {
                    currentTarget = null;
                }
            }
        }
    }

    public void HandleMoveToTarget()
    {
        
    }
}
