using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObject : MonoBehaviour
{
   
    
    public GameObject lightToBeActivated;
    public GameObject doorToBeMoved;
    MovingDoor movingDoor;

   
    void Start()
    {
        if(lightToBeActivated != null) // check that light has been defined in the inspector
        {
            lightToBeActivated.SetActive(false); // set light off to begin with
        }
         else
        {
            Debug.Log("Light from win object missing!"); 
        }


        if(doorToBeMoved != null) // check that door has been defined in the inspector
        {
            movingDoor = doorToBeMoved.GetComponent<MovingDoor>(); // get access to script on door
        }
         else
        {
            Debug.Log("Door from win object missing!"); 
        }
        
    }

    public void Win()
    {

        if(lightToBeActivated != null) // check that light has been defined in the inspector
        {
            lightToBeActivated.SetActive(true); // setting light on
        }
       
        if(movingDoor != null) 
        {
            movingDoor.enabled = true;  // activating move script on door
             Debug.Log("Opening door!");
        }

    }
}
