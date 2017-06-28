using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMotion : MonoBehaviour {

    // Use this for initialization
    
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.realtimeSinceStartup;
        float quater =Mathf.Sin(t*2.0f)*30;
        transform.rotation =
            Quaternion.AngleAxis(quater, Vector3.forward);
      
    }
}
