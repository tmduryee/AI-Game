using UnityEngine;
using System.Collections;

public class Mouse : Entity
{
    private GameObject[] cats;
	private Vector3 lastFramePosition;

	// Genetic Algorithm stuff
	private Vector3 originPos;

	void Start ()
    {
        aiComponent = this.GetComponent<AI>();
        cats = GameObject.FindGameObjectsWithTag("Cat");

		lastFramePosition = this.transform.position;

		// Genetic Algorithm stuff
		originPos = this.transform.position;
	}
	
	void Update ()
    {
        Vector3 myPos = transform.position;
        GameObject closestCat = null;
        Vector3 currentCatPos = new Vector3();
		float distanceFromCat = 0.0f;
		
        // First find the closest cat
		foreach (GameObject c in cats)
		{
			currentCatPos = c.transform.position;
			distanceFromCat = Vector3.Distance(transform.position, currentCatPos);
			
			if (closestCat == null || distanceFromCat < Vector3.Distance(myPos, closestCat.transform.position))
			{
				closestCat = c;
			}
		}

        // If the closest cat is too close flee it
        if (Vector3.Distance(myPos, closestCat.transform.position) < aiComponent.fleeRadius)
        {
            aiComponent.Flee(closestCat);

			// Mouse was caught!
			if (Vector3.Distance(transform.position, closestCat.transform.position) < 10.0f) {
				if (closestCat.name == "Cat") {
					UI.score++;
					Destroy (this.gameObject);
				}
				else if (closestCat.name == "EnemyCat") {
					UI.scoreEnemy++;
					Destroy (this.gameObject);
				}
			}
        }
        // Otherwise let's find some cheese
        else
        {
            GameObject[] cheese = GameObject.FindGameObjectsWithTag("Cheese");
            GameObject closestCheese = null;
            Vector3 currentCheesePos = new Vector3();
            float distanceFromCheese = 0.0f;

            foreach (GameObject c in cheese)
            {
                currentCheesePos = c.transform.position;
                distanceFromCheese = Vector3.Distance(myPos, currentCheesePos);

                if (distanceFromCheese < aiComponent.seekRadius && (closestCheese == null || distanceFromCheese < Vector3.Distance(myPos, closestCheese.transform.position)))
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
		lastFramePosition = myPos;
    }

	// What is the point of this?
    /*void OnCollisionEnter(Collision c)
    {
        if(c.collider.tag == "Cat")
        {
            //Destroy(gameObject);
        }
    }*/

	// Genetic Algortithm methods
	public void resetMouse()
	{
		this.transform.position = originPos;
	}
}
