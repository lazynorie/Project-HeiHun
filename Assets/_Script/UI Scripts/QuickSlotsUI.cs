using UnityEngine;
using UnityEngine.UI;

public class QuickSlotsUI : MonoBehaviour
{
    public Image leftWeaponIcon;
    public Image rightWeaponIcon;

    private void Start()
    {
        
    }

    public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
    {
        //check the weapon is righthand or lefthand  检查武器是左手还是右手
        if (isLeft == false)
        {
            if (weapon.itemIcon != null)
            {
                rightWeaponIcon.sprite = weapon.itemIcon;
                rightWeaponIcon.enabled = true;
            }
            else
            {
                Debug.Log("weapon icon for right hand weapon is not assigned.");
                rightWeaponIcon.sprite = null;
                rightWeaponIcon.enabled = false;
            }
        }
        else
        {
            if (weapon.itemIcon != null)
            {
                leftWeaponIcon.sprite = weapon.itemIcon;
                leftWeaponIcon.enabled = true;
            }
            else
            {
                Debug.Log("weapon icon for left hand weapon is not assigned.");
                leftWeaponIcon.sprite = null;
                leftWeaponIcon.enabled = false;
            }
        }
    }
}
