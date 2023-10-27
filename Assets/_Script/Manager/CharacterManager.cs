using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterManager : MonoBehaviour
{
    [Header("lock on function")]
    public Transform lockOnTransform;
    public GameObject lockOnUIelement;
    [Header("Combat Colliders")]
    public BoxCollider backStabBoxCollider;
    public BoxCollider riposteCollider;
    [FormerlySerializedAs("backStabCollider")] public CriticalDamageCollider[] criticalDamageColliders;

    [Header("Combat Flags")] 
    public bool canBeRiposted;
    protected virtual void Awake()
    {
        criticalDamageColliders = GetComponentsInChildren<CriticalDamageCollider>();
        AssignCriticalDamageColliers();
    }

    private void AssignCriticalDamageColliers()
    {
        backStabBoxCollider = criticalDamageColliders[0].GetComponent<BoxCollider>();
        riposteCollider = criticalDamageColliders[1].GetComponent<BoxCollider>();

    }
}
