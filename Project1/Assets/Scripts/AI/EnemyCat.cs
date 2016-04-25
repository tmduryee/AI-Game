using UnityEngine;
using System.Collections;

public class EnemyCat : Entity
{
    GameObject dog;
    GameObject target;

    void Start()
    {
        aiComponent = this.GetComponent<AI>();
        dog = GameObject.FindGameObjectWithTag("Dog");
        target = null;
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

            // If the mouse is close enough eat it
            if (Vector3.Distance(myPos, target.transform.position) < 9)
            {
                //GameObject.Destroy(target);
            }
            else
            {
                aiComponent.Seek(target);
            }
        }
    }
}
