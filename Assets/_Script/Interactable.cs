/* 
created by Jing Yuan Cheng on Oct 3 2023 
This is the base class of interactible items
*/

using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour
{
    public InteractEvent onItemIteract;
    [Header("Item name")]
    public string interactableItemName;
    [TextAreaAttribute]
    [Header("Item information")]
    public string interacibleText;
    [Header("Detection radius")]
    [SerializeField]private float radius = 0.6f;

    protected InteractableUI interactableUI;
    protected SphereCollider interactiveCollider;

    protected virtual void Awake()
    {
        interactiveCollider = GetComponent<SphereCollider>();
        interactableUI = FindObjectOfType<InteractableUI>();
    }

    protected virtual void Start()
    {
        interactiveCollider.radius = radius;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Interact(PlayerManager playerManager){
        //call when player interacts
        onItemIteract.Invoke(this);
        Debug.Log("You've interacted with " + interactableItemName.ToString());
    }
}

[System.Serializable]
public class InteractEvent : UnityEvent<Interactable>{}

