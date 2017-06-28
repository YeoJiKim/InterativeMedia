using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1030514309胡远琪
public class Rotate : MonoBehaviour {
    
    private Vector3 oldPos;

    // Use this for initialization
    void Start () {
        oldPos = transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color/*(255, 0, 0)*/(Random.Range(0.0f,0.3f), Random.Range(0.8f, 1.0f), Random.Range(0.2f, 0.6f));
        //sr.color = new Color(20,200,20);
    }
	
	// Update is called once per frame
	void Update () {
        float 当前时刻_秒 = Time.realtimeSinceStartup;
        transform.rotation =
            Quaternion.AngleAxis(-当前时刻_秒*300.0f, Vector3.forward);
        transform.position = new Vector3(oldPos.x,oldPos.y,-9);
    }
}
