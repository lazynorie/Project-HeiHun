using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingCollider : MonoBehaviour
{
    public BoxCollider blockingCollider;
    public float blockingEfficiency;
    public CharacterManager shieldOwner;

    private void Awake()
    {
        blockingCollider = GetComponent<BoxCollider>();
        blockingCollider.enabled = false;
        blockingCollider.isTrigger = true;
    }

    private void Start()
    {
        //shieldOwner = GetComponent<DamageCollider>().weaponOwner;
    }

    public void SetDamageAbsorption(WeaponItem weapon)
    {
        if (weapon != null)
        {
            blockingEfficiency = weapon.blockingEfficiency;
        }
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
