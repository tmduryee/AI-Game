using UnityEngine;
using System.Collections;

public class EnemyCat : Entity
{
    GameObject dog;
    GameObject target;

	// Genetic Algorithm stuff
	private Vector3 originPos;

    void Start()
    {
        aiComponent = this.GetComponent<AI>();
        dog = GameObject.FindGameObjectWithTag("Dog");
        target = null;
		lastFramePosition = this.transform.position;

		// Genetic Algorithm stuff
		originPos = this.transform.position;
    }

    void Update()
    {
        Vector3 myPos = transform.position;
        Vector3 dogPos = dog.transform.position;

        // Flee from the dog if he's too close
        if (Vector3.Distance(myPos, dogPos) < aiComponent.fleeRadius)
        {
            aiComponent.Flee(dog);
        }
        // Otherwise let's find some mice to eat
        else
        {
            // Find a target if we don't have one
            if (!target)
            {
                GameObject[] mice = GameObject.FindGameObjectsWithTag("Mouse");
                GameObject closestMouse = null;
                Vector3 mousePos;
                float distance;

                foreach (GameObject m in mice)
                {
                    mousePos = m.transform.position;
                    distance = Vector3.Distance(myPos, mousePos);

                    if (closestMouse == null || distance < Vector3.Distance(myPos, closestMouse.transform.position))
                    {
                        closestMouse = m;
                    }
                }

                target = closestMouse;
            }

            // Seek mouse
			if (target != null && Vector3.Distance(myPos, target.transform.position) < aiComponent.seekRadius) {
                aiComponent.Seek(target);
            }
			else
			{
				aiComponent.Wander(lastFramePosition);
			}
        }
    }

	// Genetic Algorithm methods
	public void resetCat()
	{
		this.transform.position = originPos;
	}
}
