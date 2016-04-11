using UnityEngine;
using System.Collections;

public class Mouse : Entity {
    AI aiComponent;
    
	void Start ()
    {
        aiComponent = this.GetComponent<AI>();
	}
	
	void Update ()
    {
        Vector3 myPos = transform.position;
		GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
		GameObject closestCat = null;
		Vector3 catPos;
		float distanceFromCat;
		
        // First find the closest cat
		foreach (GameObject c in cats)
		{
			catPos = c.transform.position;
			distanceFromCat = Vector3.Distance(transform.position, catPos);
			
			if (closestCat == null || distanceFromCat < Vector3.Distance(transform.position, closestCat.transform.position))
			{
				closestCat = c;
			}
		}

        if (Vector3.Distance(myPos, closestCat.transform.position) < aiComponent.fleeRadius)
        {
            aiComponent.Flee(closestCat);
        }
        else
        {
            GameObject[] cheese = GameObject.FindGameObjectsWithTag("Cheese");
            GameObject closestCheese = null;
            Vector3 cheesePos;
            float distance;

            foreach (GameObject c in cheese)
            {
                cheesePos = c.transform.position;
                distance = Vector3.Distance(myPos, cheesePos);

                if (distance < aiComponent.seekRadius && (closestCheese == null || distance < Vector3.Distance(myPos, closestCheese.transform.position)))
                {
                    closestCheese = c;
                }
            }

            if (!closestCheese)
            {
                aiComponent.Wander(lastFramePosition);
            }
            else
            {
                if (Vector3.Distance(myPos, closestCheese.transform.position) < 2)
                {
                    closestCheese.GetComponent<Cheese>().Eat();
                }
                else
                {
                    aiComponent.Seek(closestCheese);
                }
            }
        }

        lastFramePosition = transform.position;
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.collider.tag == "Cat")
        {
            //Destroy(gameObject);
        }
    }
}
