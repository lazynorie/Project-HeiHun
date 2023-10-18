/*
By Jing Yuan Cheng on Oct 5 2023
this script is to manage the pop up message for interactable items
version 1
*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractableUI : MonoBehaviour
{
     [Header("item name for interactble item")]
     public TextMeshProUGUI text;
     [Header("item infos for item player interacted with")]
     public TextMeshProUGUI itemPickUpText;
     public RawImage imageToDisplay;
    
     [Header("interactable item UI elements")]
     [SerializeField] private GameObject interactableUIGameObject;

     private void Start()
     {
         interactableUIGameObject.SetActive(false);
         PlayerManager.onInteractable += DelegatesTrigger;
     }

     public void ShowItemPickUpText(Interactable interactable)
     {
      itemPickUpText.text = interactable.interactableItemName;
     }

     public void SwitchItemPickUpText(bool _bool)
     {
         interactableUIGameObject.SetActive(_bool);
         if (_bool)
         {
             //pause the game if in single player mode
             //Time.timeScale = 0;
             CustomTime.LocalTimeScale= 0.0f;
         }
     }
     
     public void SetImageForItemPickUp(Item item)
     {
         imageToDisplay.texture = item.itemIcon.texture;
     }

     public void DelegatesTrigger()
     {
         Debug.Log("hi there");
     }
}
