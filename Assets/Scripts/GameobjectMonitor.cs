using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectMonitor : MonoBehaviour
{
    
     GameObjectSequence objectSequence;
    //  get object sequence object
    //  check for collisions between object sequence objects and 'objectFetchDestination' ie the object where fetched objects need to be brought
    //  call MarkObjectAsFound when a legit collision happens, call only once & set isKinematic as true on the sequence object  
    
    void Start()
    {
        objectSequence = GameObject.Find("GameObjectSequence").GetComponent<GameObjectSequence>();
    }

    private void OnCollisionEnter(Collision other) {

        Debug.Log(other.gameObject.name + " hit " + this.name);
        
        objectSequence.MarkObjectAsFound(other.gameObject);
        
    }
}
