using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1030514308余智钦
public class Art1 : MonoBehaviour {

    Vector3 m_Art1Pos;
    public float m_Speed = 2;
    Vector3 m_OriPos;
    // Use this for initialization
    void Start()
    {
        m_OriPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 LocPos = transform.localPosition;
        float t = Time.realtimeSinceStartup * m_Speed;
         float x = 16 * Mathf.Sin(t) * Mathf.Sin(t) * Mathf.Sin(t);
         float y = 13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t)
             - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t);
       
        m_Art1Pos = new Vector3(x, y , 0) * 0.06f + m_OriPos;

        transform.localPosition = m_Art1Pos;
    
    }

   
}
