using UnityEngine;
using System.Collections;

public class EnemyCat : MonoBehaviour {

	AI aiComponent;
	
	void Start ()
	{
		aiComponent = this.GetComponent<AI>();
	}
	
	void Update ()
	{
		Vector3 myPos = transform.position;
		Vector3 dogPos = GameObject.FindGameObjectWithTag("Dog").transform.position;
		
		if (Vector3.Distance(myPos, dogPos) < aiComponent.fleeRadius)
		{
			aiComponent.Flee(GameObject.FindGameObjectWithTag("Dog"));
		}
		else
		{
			GameObject[] mice = GameObject.FindGameObjectsWithTag("Mouse");
			GameObject closestMouse = null;
			Vector3 mousePos;
			float distance;
			
			foreach (GameObject m in mice)
			{
				mousePos = m.transform.position;
				distance = Vector3.Distance(myPos, mousePos);
				
				if (distance < aiComponent.seekRadius && (closestMouse == null || distance < Vector3.Distance(myPos, closestMouse.transform.position)))
				{
					closestMouse = m;
				}
			}
			
			if (!closestMouse)
			{
				aiComponent.Wander();
			}
			else
			{
				if (Vector3.Distance(myPos, closestMouse.transform.position) < 2)
				{
					//closestMouse.GetComponent<Mouse>().Eat();
				}
				else
				{
					aiComponent.Seek(closestMouse);
					Debug.Log(closestMouse.transform.position);
				}
			}
		}
	}
}
