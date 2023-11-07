using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FogWall : MonoBehaviour
{
    public BoxCollider fogWallCollider;
    
    private void Awake()
    {
        fogWallCollider = GetComponent<BoxCollider>();
        fogWallCollider.enabled = false;
    }
    
    public void EnableFogWall()
    {
        gameObject.SetActive(true);
    }
    public void DisableFogWall()
    {
        gameObject.SetActive(false);
    }
}
