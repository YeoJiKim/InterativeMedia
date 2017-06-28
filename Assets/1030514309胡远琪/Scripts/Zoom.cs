using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1030514309胡远琪
public class Zoom : MonoBehaviour {
    private Vector3 oldPos;
    private float ran = 0;
    private float scale = 0;
    // Use this for initialization
    void Start()
    {
        ran = Random.Range(0.0f,3.14f);
        oldPos = transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(Random.Range(0.0f, 0.2f), Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.0f));
        //sr.color = new Color(255,255,0);
    }

    // Update is called once per frame
    void Update()
    {
        float 当前时刻_秒 = Time.realtimeSinceStartup;
        scale = 0.2f+0.1f*Mathf.Sin(当前时刻_秒 + ran);
        transform.localScale =
            new Vector2(scale, scale);
        transform.position = new Vector3(oldPos.x, oldPos.y, -9);
        //旋转
        transform.rotation =
            Quaternion.AngleAxis(-当前时刻_秒 * 200.0f, Vector3.forward);
    }
}
