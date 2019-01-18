using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour
{
    private Transform ballAtStart;

    private Vector3 ballStartPosition;

    AudioSource audioSource;
    void Start()
    {
        ballAtStart = gameObject.GetComponent<Transform>();
        ballStartPosition = ballAtStart.position;
        Debug.Log("Ball start position recorded at " + ballAtStart.position);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void ResetPosition()
    {
        // Debug.Log("Ball was at position " + gameObject.GetComponent<Transform>().position + " before resetting to " + ballAtStart.position);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.position = ballStartPosition;
        audioSource.Play();
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

}
