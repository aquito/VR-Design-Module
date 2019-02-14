using Mimesys.Unity.Multicam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveController : MonoBehaviour {

    MimesysCameraClient mimesysClient = null;

	// Use this for initialization
	void Start ()
    {
        mimesysClient = GetComponent<MimesysCameraClient>();
        if (mimesysClient == null)
        {
            Debug.LogError("LiveController: could not fine MimesysCameraClient. Please attach this script to the MimesysCameraClient");
        }
        else
        {
            mimesysClient.onConnected.AddListener(OnConnected);
            mimesysClient.onDisconnected.AddListener(OnDisconnected);
        }
        StartCoroutine(DoSession());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnConnected()
    {
        Debug.Log("Got a connected event!");
    }

    private void OnDisconnected()
    {
        Debug.Log("Got a disconnected event");
    }

    IEnumerator DoSession()
    {
        if (mimesysClient != null)
        {
            yield return new WaitForSeconds(3f);
            Debug.Log("Connecting...");
            mimesysClient.Connect();
        }
    }
}
