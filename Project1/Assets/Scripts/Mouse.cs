using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
    AI aiComponent;
    
	void Start ()
    {
        aiComponent = this.GetComponent<AI>();
	}
	
	void Update ()
    {
        Vector3 myPos = transform.position;
        Vector3 catPos = GameObject.FindGameObjectWithTag("Cat").transform.position;

        if (Vector3.Distance(myPos, catPos) < aiComponent.fleeRadius)
        {
            aiComponent.Flee(GameObject.FindGameObjectWithTag("Cat"));
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
                aiComponent.Wander();
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
	}

    void OnCollisionEnter(Collision c)
    {
        if(c.collider.tag == "Cat")
        {
            Destroy(gameObject);
        }
    }
}
