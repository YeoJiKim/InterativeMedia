using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1030514308余智钦
public class Art3 : MonoBehaviour {

    Vector3 m_Art1Pos;

    Vector3 m_OriPos;

    // Use this for initialization
    float u, v, l;
    float a, b, x, y;
    void Start()
    {
        m_OriPos = transform.position;

        a = 1.28f;
        b = -0.985f;
        x = 0.01f;
        y = 0.01f;
       
    }

    // Update is called once per frame
    void Update()
    {
        u = 1 - a * x * x + b * y;
        v = x - y;
        l = Mathf.Sqrt(u * u + v * v);
        u = (l < 0.01f) ? u / 0.01f : u;
        v = (l < 0.01f) ? v / 0.01f : v;
        x = x + u;
        y = y + v;
        l = Mathf.Sqrt(x * x + y * y);
        l = (l % 1.5f) / l;
        x = (l < 1) ? x * l : x;
        y = (l < 1) ? y * l : y;

        m_Art1Pos = new Vector3(x, y, 0)*2.5f + m_OriPos;
        transform.localPosition = m_Art1Pos;
    }
}
