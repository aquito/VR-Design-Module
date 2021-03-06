﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public List<GameObject> trackObjects; // array for track prefabs placed in scene

    private List<GameObject> clearedObjects; // array to fill with objects the ball has cleared
    
    int objectsCleared;
    public bool isTrackCompleted; // flag for checking completion

    AudioSource audioSource;

    AudioSource trackObjectAudio;

    public BallPosition ballPosition;

    GameObject goalObject;

    public FXmanager fxMmanager;

    void Start()
    {

      objectsCleared = 0;

      isTrackCompleted = false;

      audioSource = gameObject.GetComponent<AudioSource>();

      trackObjects = new List<GameObject>();

      goalObject = GameObject.Find("Goal");
      // trackObjects = GameObject.FindGameObjectsWithTag("TrackObject");

      clearedObjects = new List<GameObject>();

        foreach(GameObject trackObject in GameObject.FindGameObjectsWithTag("TrackObject"))
        {
            trackObjects.Add(trackObject);
            Debug.Log(trackObject.name + " found.");

        }         
    }
    

    public void ProcessCollision(GameObject ball, GameObject theObjectHitbyBall)
    {

        Debug.Log(ball.name + " hit " + theObjectHitbyBall);

        

        if(theObjectHitbyBall.name == "BaseFloor" && !isTrackCompleted)
        {
           ballPosition.ResetPosition();
           isTrackCompleted = false;
           clearedObjects.Clear();
           objectsCleared = 0;
           Debug.Log("Ball reseted to starting position");

        }

        else if(theObjectHitbyBall.tag == "EndPoint" && isTrackCompleted)
        {
            TrackCompleted();
            
        }
        else if(!isTrackCompleted)
        {
                for (int i =0; i < trackObjects.Count ; i++)
                {
                    if(theObjectHitbyBall == trackObjects[i])
                    {
                        if(!clearedObjects.Contains(theObjectHitbyBall) && theObjectHitbyBall.transform.parent == null)
                        {
                            clearedObjects.Add(theObjectHitbyBall); 

                            objectsCleared++;

                            trackObjectAudio = theObjectHitbyBall.GetComponent<AudioSource>(); // play audio on trackobject prefab

                            trackObjectAudio.Play();

                            Debug.Log(theObjectHitbyBall + " added to cleared objects list");

                            CheckIfAllObjects(theObjectHitbyBall);
                        }
                    }       
                }
            
            } else if(trackObjects.Count == 0)
            {
                Debug.Log("You need to add objects to the track objects list in the inspector!");
            }
        } 
        

    

    void CheckIfAllObjects(GameObject obj)  // check if there are falses left in the dictionary
    {
       
       for (int i =0; i < clearedObjects.Count ; i++)
       {
           
           if(objectsCleared == trackObjects.Count) // if number of objects cleared equals objects on track, then flag complete
           {
               isTrackCompleted = true;
           }
       }
       
    }

    void TrackCompleted()
    {
        if(goalObject != null)
        {
            trackObjectAudio = goalObject.GetComponent<AudioSource>(); // play audio on trackobject prefab
            trackObjectAudio.Play();
            goalObject.SetActive(false); 
            Debug.Log("Track completed!");

            fxMmanager.switchFX();            // enable particle effect

        } else
        {
            Debug.Log("Goal object missing from the scene!");
        }
        
    }

}
