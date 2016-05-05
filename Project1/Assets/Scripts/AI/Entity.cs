using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    protected AI aiComponent;
    protected Vector3 lastFramePosition;
  
	void Start ()
    {	

	}
	
	void Update ()
    {
		
	}

    void LateUpdate()
    {
        lastFramePosition = transform.position;
    }
}
