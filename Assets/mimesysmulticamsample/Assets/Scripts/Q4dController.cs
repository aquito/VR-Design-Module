using Mimesys.Unity.Multicam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q4dController : MonoBehaviour {

    public string Q4dUrl = "http://s3-eu-west-1.amazonaws.com/mimesys-repository-artifacts/q4d/TNMC-Solo+Jan.15.q4d";

    private MimesysRecording mimesysRecording = null;
    // Use this for initialization
    void Start () {
        mimesysRecording = GetComponent<MimesysRecording>();
        if (mimesysRecording != null)
            mimesysRecording.DownloadAndPlay(Q4dUrl);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
