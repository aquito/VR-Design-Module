using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectSequence : MonoBehaviour
{
   //

   public Dictionary<GameObject, bool> objectsThatNeedFinding; // array to store objects and whether they have been found

    GameObject winObject;

    AudioSource audioSource;
   
   void Start()
   {


    objectsThatNeedFinding = new Dictionary<GameObject, bool>();
    audioSource = GetComponent<AudioSource>();
    winObject = GameObject.Find("WinObject");

   } 

   public void MarkObjectAsFound(GameObject obj) // checks if the object is in the dictionary and if it's value is false changes it to true to mark object off the list
   {
       foreach(GameObject objectToBeFound in objectsThatNeedFinding.Keys) 
       {
           if(objectToBeFound != null)
           {
               if(obj == objectToBeFound && objectsThatNeedFinding[obj] == false)
                {
                    objectsThatNeedFinding[obj] = true;
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

        if(objectsThatNeedFinding.ContainsValue(false))
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
