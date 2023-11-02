using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalWallScript : MonoBehaviour
{
    public bool isHit = false;
    public Material defaultMaterial;
    private Material magicalWallMaterial;
    public float alpha;
    public float fadeTimer = 2.5f;
    private MeshCollider meshCollider;
    private MeshRenderer meshRenderer;
    
    private AudioSource audioSource;
    public AudioClip soundClip;

    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        magicalWallMaterial = new Material(defaultMaterial);
        meshRenderer.material = magicalWallMaterial;
    }

    private void Update()
    {
        if (isHit)
        {
            FadeIllusionaryWall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            isHit = true;            
        }
    }

    public void FadeIllusionaryWall()
    {
        alpha = magicalWallMaterial.color.a;
        alpha = alpha - Time.deltaTime / fadeTimer;
        Color fadedWallColor = new Color(1, 1, 1, alpha);
        magicalWallMaterial.color = fadedWallColor;

        if (meshCollider.enabled)
        {
            meshCollider.enabled = false;
            audioSource.PlayOneShot(soundClip);
        }

        if (alpha <=0)
        {
            //collider.enabled = false;
            //gameObject.SetActive(false);
            Destroy(this); 
        }
    }
    
}
