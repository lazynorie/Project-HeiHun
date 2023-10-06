/* 
created by Jing Yuan Cheng on Oct 3 2023 
This is the base class of interactible items
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
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
        Debug.Log("You've interacted with " + interactableItemName.ToString());
    }

}

