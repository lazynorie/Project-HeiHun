using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterManager : MonoBehaviour
{
    [Header("lock on function")]
    public Transform lockOnTransform;
    public GameObject lockOnUIelement;
    [Header("Combat Colliders")]
    public CriticalDamageCollider backStabBoxCollider;
    public CriticalDamageCollider riposteCollider;
    [FormerlySerializedAs("backStabCollider")] public CriticalDamageCollider[] criticalDamageColliders;

    [Header("Combat Flags")] 
    public bool canBeRiposted;
    public bool isParrying;
    protected virtual void Awake()
    {
        criticalDamageColliders = GetComponentsInChildren<CriticalDamageCollider>();
        AssignCriticalDamageColliers();
    }

    protected void Update()
    {
        
    }

    private void AssignCriticalDamageColliers()
    {
        backStabBoxCollider = criticalDamageColliders[0];
        if (riposteCollider != null)
        {
            riposteCollider = criticalDamageColliders[1];
        }
    }
}
