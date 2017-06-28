using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRotate : MonoBehaviour {

	// Use this for initialization
    float quater = 35;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (RoleMove1.eatenFood == 6) {
            if (quater>=0)
            {
                transform.rotation =
            Quaternion.AngleAxis(quater, Vector3.forward);
                quater-=0.5f;
            }
        }
	}
}
