using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//1030514309胡远琪
public class End : MonoBehaviour {

    // Use this for initialization
   
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
       
        RoleMove1.life = 0;

    }
}
