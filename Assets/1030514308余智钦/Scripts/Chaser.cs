using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {

    // Use this for initialization
    public static bool m_BChase = false;
    public float m_FSpeed = 1.0f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity *= 0.95f;
        GameObject role = GameObject.FindGameObjectWithTag("Player");
      
        if (m_BChase && !RoleMove1.dead && !RoleMove1.eatall)
        {
          
            Vector3 Shift =
                role.transform.position - transform.position;
            Vector3 TgtVel = Shift.normalized;
            Vector3 Vel = rb.velocity;
            Vector3 Force = m_FSpeed * (TgtVel - Vel);
            rb.AddForce(Force);
        }

    }
   
    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Role")
        {                   
            RoleMove1.dead = true;
        }
       
        
    }
}
