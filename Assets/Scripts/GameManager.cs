using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public List<GameObject> trackObjects;
    private List<bool> objectCleared;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // monitor for collisions from balldetection script
        // if ball has hit floor, get ball starting position and return it there with negative sound
        // if ball has hit track object (and is not grabbed at that moment by user), flag it as true in the trackObject list    
    }


}
