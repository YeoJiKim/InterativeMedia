using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    SpriteRenderer sr;
    float alpha;
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        int i = Random.Range(0, 3);
        if (i == 0)
        {
            sr.sprite = s1;
        }
        else if (i == 1)
        {
            sr.sprite = s2;
        }
        else if (i == 2)
        {
            sr.sprite = s3;
        }
	}
	
	// Update is called once per frame
	void Update () {
       alpha = sr.color.a;
       if (alpha <= 0)
       {
           Destroy(gameObject);
       }
       else {
           sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha-0.002f);
       }
	}
}
