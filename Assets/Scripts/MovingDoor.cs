using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{
   Transform doorTransform;
   Vector3 doorDirection;
   AudioSource audioSource;


void Start()
{
    doorTransform = gameObject.GetComponent<Transform>(); // get the transform component of the door in order to move it
    doorDirection = new Vector3(0, 0, 0.2f); // set door movement speed & direction
    audioSource = gameObject.GetComponent<AudioSource>(); // get audio component in order to play sfx
}


void Update()
{
    audioSource.Play(); // play sound clip as set in the inspector

    if(doorTransform.position.x > -0.45f) // prevent door moving further than this x coordinate
    {
         doorTransform.Translate(doorDirection * Time.deltaTime); // moves door until it completely open
    }

}
   
}
