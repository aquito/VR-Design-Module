using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public List<GameObject> trackObjects; // array for track prefabs placed in scene

    private List<GameObject> clearedObjects; // array to fill with objects the ball has cleared
    bool isTrackCompleted; // flag for checking completion

    AudioSource audioSource;

    AudioSource trackObjectAudio;

    public BallPosition ballPosition;

 //   public GameObject audioSelector;

    void Start()
    {

      isTrackCompleted = false;

      audioSource = gameObject.GetComponent<AudioSource>();

      trackObjects = new List<GameObject>();

      // trackObjects = GameObject.FindGameObjectsWithTag("TrackObject");

      clearedObjects = new List<GameObject>();

        foreach(GameObject trackObject in GameObject.FindGameObjectsWithTag("TrackObject"))
        {
            trackObjects.Add(trackObject);
            Debug.Log(trackObject.name + " found.");

            // objectsCleared[trackObject.Value] = false; // broken
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

    public void ProcessCollision(GameObject ball, GameObject theObjectHitbyBall)
    {

        // Debug.Log(ball.name + " hit " + theObjectHitbyBall);

        

        if(theObjectHitbyBall.name == "BaseFloor")
        {
           ballPosition.ResetPosition();
           Debug.Log("Ball reseted to starting position");

        }

        else if(theObjectHitbyBall.tag == "EndPoint" && isTrackCompleted)
        {
            TrackCompleted();
            Debug.Log("Track cleared!");
        }
        else if(!isTrackCompleted)
        {
                for (int i =0; i < trackObjects.Count ; i++)
                {
                    if(theObjectHitbyBall == trackObjects[i])
                    {
                        clearedObjects.Add(theObjectHitbyBall); 

                        trackObjectAudio = theObjectHitbyBall.GetComponent<AudioSource>(); // play audio on trackobject prefab

                        trackObjectAudio.Play();

                        Debug.Log(theObjectHitbyBall + " added to cleared objects list");

                        CheckIfAllObjects();
                    
                    }       
                }
            
            } else
            {
                Debug.Log("You need to add objects to the track objects list in the inspector!");
            }
        } 
        

    

    void CheckIfAllObjects()  // check if there are falses left in the dictionary
    {
       
       
       
    }

    void TrackCompleted()
    {
        // this is called by 
        //audioSelector.PlaySFX("win");
        
    }

}
