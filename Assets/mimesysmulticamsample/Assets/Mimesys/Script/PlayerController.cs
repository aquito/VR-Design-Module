using Mimesys.Unity.Multicam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    MimesysCameraClient mimesysClient = null;

	// Use this for initialization
	void Start () {
        mimesysClient = FindObjectOfType(typeof(MimesysCameraClient)) as MimesysCameraClient;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (mimesysClient != null)
        {
            mimesysClient.HeadPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            mimesysClient.HeadOrientation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        }
	}
}
