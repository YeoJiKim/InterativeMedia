using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//1030514308余智钦
public class ArtThing : MonoBehaviour {

    public  GameObject m_ArtPrefab;
    private  GameObject m_ArtInstance;
    public UnityEvent m_Art;
    // Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay2D(Collider2D other)
    {
      
        if (other.name == "Role")
        {
            other.GetComponent<RoleMove1>().changeColor(GetComponent<SpriteRenderer>().color);
            RoleMove1.eatenFood++;
            m_ArtInstance = Instantiate(m_ArtPrefab, transform.position, transform.rotation);
            m_Art.Invoke();
            Destroy(gameObject);
          
           
        }
    }
}
