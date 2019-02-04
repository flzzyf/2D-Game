using System.Collections.Generic;
using UnityEngine;

public class Winch : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject prefab_rope;
    public int num;

    List<GameObject> ropes = new List<GameObject>();

    public LineRenderer line;

    void Start()
    {
        Rigidbody2D lastRb = hook;

        for (int i = 0; i < num; i++)
        {
            GameObject go = Instantiate(prefab_rope, transform);
            go.GetComponent<HingeJoint2D>().connectedBody = lastRb;

            lastRb = go.GetComponent<Rigidbody2D>();

            ropes.Add(go);
        }

        line.positionCount = num + 1;
    }

    void Update()
    {
        line.SetPosition(0, hook.transform.position);

        for (int i = 0; i < num; i++)
        {
            line.SetPosition(i + 1, ropes[i].transform.position);
        }
    }
}
