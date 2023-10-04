/* 
created by Jing Yuan Cheng on Oct 3 2023 
This is the base class of interactible items
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]private float radius = 0.6f;
    [Header("Item information")]

    public string interacibleText;
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Interact(PlayerManager playerManager){
        //call when player interacts
    }

}

