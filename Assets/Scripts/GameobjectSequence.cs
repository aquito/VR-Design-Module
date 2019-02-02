using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectSequence : MonoBehaviour
{
   //

   public List<GameObject> objectsThatNeedFinding; 
   public Dictionary<GameObject, bool> objectsThatNeedFindingStatus; // array to store objects and whether they have been found

    GameObject winObject;

    AudioSource audioSource;
   
   void Start()
   {

    objectsThatNeedFindingStatus = new Dictionary<GameObject, bool>();
    // objectsThatNeedFinding = new List<GameObject>();
    audioSource = GetComponent<AudioSource>();
    winObject = GameObject.Find("WinObject");

    foreach(GameObject obj in objectsThatNeedFinding)
    {
        objectsThatNeedFindingStatus.Add(obj, false);
    }

   } 

   public void MarkObjectAsFound(GameObject obj) // checks if the object is in the dictionary and if it's value is false changes it to true to mark object off the list
   {
       foreach(GameObject objectToBeFound in objectsThatNeedFindingStatus.Keys) 
       {
           if(objectToBeFound != null)
           {
               if(obj == objectToBeFound && objectsThatNeedFindingStatus[obj] == false)
                {
                    objectsThatNeedFindingStatus[obj] = true;
                    Debug.Log(obj.name + " found!");
                }

           }
           else
           {
               Debug.Log("Found Object slot(s) empty - drag objects from the scene to the slots exposed in Inspector!");
           }
           
       }

       CheckIfAllObjectsHaveBeenFound(); 

   }

   public void CheckIfAllObjectsHaveBeenFound()
   {

        if(objectsThatNeedFindingStatus.ContainsValue(false))
        {
            Debug.Log("Not all objects have been found.");
        }
        else
        {
            Debug.Log("All objects found!");
            audioSource.Play();
            // winObject.Win(); // activate chosen object upon win condition

        }
       
   }

}
