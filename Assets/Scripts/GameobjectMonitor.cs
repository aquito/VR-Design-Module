using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectMonitor : MonoBehaviour
{
    
     GameObjectSequence objectSequence;
    
    
    void Start()
    {
        objectSequence = GameObject.Find("GameObjectSequence").GetComponent<GameObjectSequence>();
    }

    private void OnCollisionEnter(Collision other) {

        Debug.Log(other.gameObject.name + " hit " + this.name);
        
        objectSequence.MarkObjectAsFound(other.gameObject);
        
    }
}
