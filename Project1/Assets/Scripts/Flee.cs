using UnityEngine;
using System.Collections;

public class Flee : MonoBehaviour {

    public string toFlee = "";
    public int fleeRadius = 0;
    public int fleeSpeed = 0;
    public bool fleeing = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fleeing = false;

        Vector3 myPos = transform.position;
        Vector3 targetPos = new Vector3();
        Vector3 direction = new Vector3();
        GameObject[] targets = GameObject.FindGameObjectsWithTag(toFlee);

        for (int i = 0; i < targets.Length; i++)
        {
            targetPos = targets[i].transform.position;
            direction = myPos - targetPos;
            direction.y = 0;

            if (direction.magnitude < fleeRadius)
            {
                transform.Translate(direction.normalized * fleeSpeed / 10);
                fleeing = true;
            }
        }
    }
}
