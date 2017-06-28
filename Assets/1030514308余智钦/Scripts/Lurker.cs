using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lurker : MonoBehaviour {

    // Use this for initialization

    public Color m_CrAlert, m_CrSeeing;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.name == "Role")
        {
            
            GetComponent<SpriteRenderer>().color = m_CrSeeing;
            Chaser.m_BChase = true;
        }
       
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Role")
        {
           
            GetComponent<SpriteRenderer>().color = m_CrSeeing;
            Chaser.m_BChase = true;
        }
       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Role")
        {
            

            GetComponent<SpriteRenderer>().color = m_CrAlert;
            Chaser.m_BChase = false;
        }
       
    }
    

}
