using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXmanager : MonoBehaviour
{
    
    public GameObject indicatorParticleFX;

    public GameObject winParticleFX;

    GameManager gameManager;

    public AudioSource audioSource;

    bool hasPlayerWon;

    private void Start() {
        
       gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    public void switchFX()
    {
       
            indicatorParticleFX.SetActive(false);
            winParticleFX.SetActive(true);
            audioSource.Play();

    }
}
