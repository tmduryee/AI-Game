using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    float playerSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.W)) transform.Translate(new Vector3(0, 0, playerSpeed));
        if (Input.GetKey(KeyCode.A)) transform.Translate(new Vector3(-playerSpeed, 0, 0));
        if (Input.GetKey(KeyCode.S)) transform.Translate(new Vector3(0, 0, -playerSpeed));
        if (Input.GetKey(KeyCode.D)) transform.Translate(new Vector3(playerSpeed, 0, 0));

    }
}
