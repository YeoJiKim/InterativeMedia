using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1030514308余智钦
public class Art2 : MonoBehaviour {

    Vector3 m_Art1Pos;
   // public float m_Speed = 2;
    Vector3 m_OriPos;
   // public int m_IteratorCount = 2;
    // Use this for initialization
    float f, t, a, b, x, y;
    void Start () {
        m_OriPos = transform.position;
        /* a = -0.450000f;
         b = 0.930000f;
         x = 1.0000f;
         y = 1.0000f;*/
        a = -0.491700f;
        b = 0.997400f;
        x = 1.0000f;
        y = 1.0000f;
    }

    // Update is called once per frame
    void Update () {
        f = a * x + (1 - a) * 2 * x * x / (1 + x * x);
        t = x;
        x = b * y + f;
        f = a * x + (1 - a) * 2 * x * x / (1 + x * x);
        y = -t + f;

        m_Art1Pos = new Vector3(x, y, 0) * 0.08f + m_OriPos;
        transform.localPosition = m_Art1Pos;
    }
   
}
