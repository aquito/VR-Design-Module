using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject[] trackObjects; // array for track prefabs placed in scene

    private Dictionary<GameObject, bool> objectsCleared; // dictionary where each track object is flagged false until true (=once ball has touched them) 

    bool isTrackCompleted; // flag for checking completion

    public GameObject audioSelector;

    void Start()
    {

      isTrackCompleted = false;

      trackObjects = GameObject.FindGameObjectsWithTag("TrackObject");

        foreach(GameObject trackObject in trackObjects)
        {
            Debug.Log(trackObject.name + " found.");
            objectsCleared[trackObject] = false;
        }   

       // audioSelector = gameObject.FindGameObject("AudioSelector").GetComponent<AudioSelector>();
        // audioSelector = audioSelector.FindObjectOfType<AudioSelector>() as AudioSelector;
    }
    
    
    void Update()
    {
        // monitor for collisions from balldetection script
        // if ball has hit floor, get ball starting position and return it there with negative sound
        // if ball has hit track object (and is not grabbed at that moment by user), flag it as true in the trackObject list    
    }

    public void ProcessCollision(GameObject ball, GameObject other)
    {

        if(other.tag == "EndPoint" && isTrackCompleted)
        {
            TrackCompleted();
            Debug.Log("Track cleared!");
        }
        else
        {
            
        }

             if(trackObjects != null)
            {
                for (int i =0; i < trackObjects.Length ; i++)
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

         for (int i =0; i < trackObjects.Length ; i++)
            {
                if(objectsCleared[obj] == false)
                {
                   objectsCleared[obj] = true;
                   Debug.Log(obj + "cleared by ball");

                    // check if object 
                }
            }   
    }

    void CheckIfAllObjects()  // check if there are falses left in the dictionary
    {
        foreach(KeyValuePair<GameObject, bool> entry in objectsCleared)
        {
            if(entry.Value)
            {
                break;
            } else
            {
                isTrackCompleted = true;
                Debug.Log("All track objectc cleared with the ball");
            }
        }
       
    }

    void TrackCompleted()
    {
        // this is called by 
        audioSelector.PlaySFX("win");
        
    }

}
