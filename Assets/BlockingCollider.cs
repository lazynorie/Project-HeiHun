using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingCollider : MonoBehaviour
{
    private BoxCollider blockingCollider;
    public float blockingEffiency;
    public CharacterManager shieldOwner;

    private void Awake()
    {
        blockingCollider = GetComponent<BoxCollider>();
        
    }

    private void Start()
    {
        shieldOwner = GetComponent<DamageCollider>().weaponOwner;
    }

    public void EnableBlockingCollider()
    {
        blockingCollider.enabled = true;
    }

    public void DisableBlockingCollider()
    {
        blockingCollider.enabled = false;
    }
}
