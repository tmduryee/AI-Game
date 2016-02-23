using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour {

    public string toSeek  = "";
    public int seekRadius = 0;
    public int seekSpeed  = 0;
    public bool seeking = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        seeking = false;

        Vector3 myPos = transform.position;
        Vector3 targetPos = new Vector3();
        Vector3 direction = new Vector3();
        GameObject[] targets = GameObject.FindGameObjectsWithTag(toSeek);

        for(int i = 0; i < targets.Length; i++)
        {
            targetPos = targets[i].transform.position;
            direction = targetPos - myPos;

            if (direction.magnitude < seekRadius)
            {
                transform.Translate(direction.normalized * seekSpeed / 10);
                seeking = true;
            }
        }
	}
}
