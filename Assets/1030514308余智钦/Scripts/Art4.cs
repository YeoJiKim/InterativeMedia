using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1030514308余智钦
public class Art4 : MonoBehaviour {

    Vector3 m_Art1Pos;

    Vector3 m_OriPos;

    // Use this for initialization
    float a, r, x, y;
    float b, c, t;
    void Start()
    {
        m_OriPos = transform.position;

        a = 3.647602f;
        r = 0.001f;
        x = 1.0f;
        y = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        b = Mathf.Sin(a);
        c = Mathf.Cos(a);
        t = x * c - y * b + x * x * b;
        y = x * b + y * c - x * x * c;
        x = t % 4 + Random.Range(-r, r);
        y = y % 4 + Random.Range(-r, r);

        m_Art1Pos = new Vector3(x, y, 0) * 0.9f + m_OriPos;
        transform.localPosition = m_Art1Pos;
    }
}
