using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
   public GameManager gameManager;
    
    // look for collisions

    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.tag == "Ball")
        {
            gameManager.ProcessCollision(other.gameObject, this.gameObject);
        }
        // if other is ball, send the game object this script is attached to, to the gamemanager and it will process it according to object type (floor/prop)
    }

    
}
