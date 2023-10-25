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
    public BackStabCollider backStabCollider;
    protected virtual void Awake()
    {
        backStabCollider = GetComponentInChildren<BackStabCollider>();
        backStabBoxCollider = backStabCollider.GetComponent<BoxCollider>();
    }
}
