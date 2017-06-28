using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1030514310许丹丹
public class fractal3 : MonoBehaviour {
    public Mesh mesh;
    public Material material;


    public int maxDepth;
    private int depth;
    public float childScale;

    // Use this for initialization


    void Start()
    {


        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }

    }

    IEnumerator CreateChildren()
    {

        yield return new WaitForSeconds(0.2f);
         new GameObject("Fractal Child").AddComponent<fractal3>()
              .Initialize(this, Vector3.up, Quaternion.Euler(0f, 0f, -30f));
  //    yield return new WaitForSeconds(0.5f);
  //      new GameObject("Fractal Child").AddComponent<fractal3>()
   //       .Initialize(this, -Vector3.up, Quaternion.Euler(0f, 0f, 30f));
     //   yield return new WaitForSeconds(0.5f);
   //     new GameObject("Fractal Child").AddComponent<fractal3>()
   //         .Initialize(this, Vector3.right, Quaternion.Euler(0f, 0f, -30f));
   //     yield return new WaitForSeconds(0.5f);
   //   new GameObject("Fractal Child").AddComponent<fractal3>()
   //      .Initialize(this, Vector3.left, Quaternion.Euler(0f, 0f, 30f));

    }
    void Initialize(fractal3 parent, Vector3 direction, Quaternion orientation)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = Vector3.up * (0.3f + 0.5f * childScale);
        transform.localPosition = direction * (0.3f + 0.5f * childScale);
        transform.localRotation = orientation;//设置旋转角度同父级一样。
    }

    // Update is called once per frame
    void Update () {
		
	}
}
