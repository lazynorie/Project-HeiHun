using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnenmyHealthBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField]
    private float timeUntilBarIsHidden;
    private CameraHandler cameraHandler;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        cameraHandler = FindObjectOfType<CameraHandler>();
    }

    public void SetCurrentHealth(int health)
    {
        slider.value = health;
        timeUntilBarIsHidden = 3;
    }

    public void SetMaxHealth(int maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }

    private void Update()
    {
        if (slider == null) return;
        timeUntilBarIsHidden -= Time.deltaTime;
        if (timeUntilBarIsHidden <= 0)
        {
            timeUntilBarIsHidden = 0;
            slider.gameObject.SetActive(false);
        }
        else
        {
            if (!slider.gameObject.activeInHierarchy)
            {
                slider.gameObject.SetActive(true);
            }
        }

        if (slider.value <= 0)
        {
            //Destroy(slider.gameObject);
            gameObject.SetActive(false);
        }
    }

    public void EnableHealthBar()
    {
        slider.gameObject.SetActive(true);
        timeUntilBarIsHidden = 5;
    }

    public void DisableHealthBar()
    {
        slider.gameObject.SetActive(false);
        timeUntilBarIsHidden = 0;
    }
}
