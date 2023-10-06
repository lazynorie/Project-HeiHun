/* 
created by Jing Yuan Cheng on Oct 3 2023 
This is the base class of interactible items
*/

using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public InteractEvent onItemIteract;
    
    [TextAreaAttribute]
    [Header("Item name")]
    public string interactableItemName;
    [TextAreaAttribute]
    [Header("Item information")]
    public string interacibleText;
    [Header("Detection radius")]
    [SerializeField]private float radius = 0.6f;
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

