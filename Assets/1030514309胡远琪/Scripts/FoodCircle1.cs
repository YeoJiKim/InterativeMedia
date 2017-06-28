using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//1030514309胡远琪
public class FoodCircle1 : MonoBehaviour {

    private Rigidbody2D food;
    public GameObject _Prefab;
    public UnityEvent m_Art;
    // Use this for initialization
    void Start () {
        food = GetComponent<Rigidbody2D>();
       // food.position = new Vector2(Random.Range(-8f,6.5f), Random.Range(1.80f, 10.5f));
       
    }

    // Update is called once per frame
    void Update() {
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name=="Role")
        {
            other.GetComponent<RoleMove1>().changeColor(GetComponent<SpriteRenderer>().color);
            RoleMove1.eatenFood++;
            Vector3 pos = food.transform.position;
            GameObject newObj = Instantiate(_Prefab) as GameObject;
            newObj.transform.position = pos;
            m_Art.Invoke();
            Destroy(gameObject);
        }
        
    }
}
