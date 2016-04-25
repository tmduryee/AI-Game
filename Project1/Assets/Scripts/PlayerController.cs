using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 1.0f;
	public float rotateSpeed = 4.0f;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if (Input.GetKey(KeyCode.W)) transform.Translate(new Vector3(0, 0, playerSpeed));
        if (Input.GetKey(KeyCode.A)) transform.Translate(new Vector3(-playerSpeed/2, 0, 0));
        if (Input.GetKey(KeyCode.S)) transform.Translate(new Vector3(0, 0, -playerSpeed/2));
        if (Input.GetKey(KeyCode.D)) transform.Translate(new Vector3(playerSpeed/2, 0, 0));

		// Character rotation using mouse
		float forward = Input.GetAxis("Vertical");
		float side = Input.GetAxis("Horizontal");
		float rotY = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0))
        {
            gameObject.transform.Rotate(0, rotY * rotateSpeed, 0);
        }
    }
}
