using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour
{
    public Transform ballStartPosition;
    void Start()
    {
        ballStartPosition = gameObject.transform;
    }

}
