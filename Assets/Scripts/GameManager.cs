using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public List<GameObject> trackObjects;
    private Dictionary<GameObject, bool> objectsCleared;

    void Start()
    {

        // get the GameObject for objectsCleared dict and set them to false

    }

    // Update is called once per frame
    void Update()
    {
        // monitor for collisions from balldetection script
        // if ball has hit floor, get ball starting position and return it there with negative sound
        // if ball has hit track object (and is not grabbed at that moment by user), flag it as true in the trackObject list    
    }

    public void ProcessCollision(GameObject ball, GameObject other)
    {

        if(trackObjects != null)
        {
            for (int i =0; i < trackObjects.Count ; i++)
            {
                if(other == trackObjects[i])
                {
                   CheckObjectOff(other); 
                    // check if all objects set on trackObjects are among objectsCleared
                }
            }
            

        } else
        {
            Debug.Log("You need to add objects to the track objects list in the inspector!");
        }
       
        Debug.Log(ball.name + " hit " + other);
    }

    void CheckObjectOff(GameObject obj) // mark object as true on the list
    {   

         for (int i =0; i < trackObjects.Count ; i++)
            {
                if(objectsCleared[obj] == false)
                {
                   objectsCleared[obj] = true;

                    // check if object 
                }
            }   
    }

    void CheckIfAllObjects()
    {
        // check if there are falses left in the dictionary
    }
}
