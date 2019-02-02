using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObject : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject objectToBeActivated;

   
    void Start()
    {
        objectToBeActivated.SetActive(false);
    }

    public void Win()
    {

        if(objectToBeActivated != null)
        {
            objectToBeActivated.SetActive(true);
        }
        else
        {
            Debug.Log("Win object missing!");
        }

    }
}
