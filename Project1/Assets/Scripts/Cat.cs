using UnityEngine;
using System.Collections;

public class Cat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);

        if(col.gameObject.tag == "Mouse")
        {
            Destroy(col.gameObject);
        }
    }
}
