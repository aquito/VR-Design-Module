using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectMonitor : MonoBehaviour
{
    
     GameObjectSequence objectSequence;

     AudioSource audioSource;
    
    
    void Start()
    {
        objectSequence = GameObject.Find("GameObjectSequence").GetComponent<GameObjectSequence>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {

        audioSource.Play();

        Debug.Log(other.gameObject.name + " hit " + this.name);
        
        objectSequence.MarkObjectAsFound(other.gameObject);

        
        
    }
}
