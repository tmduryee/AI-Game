using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    Seek sComponent;
    Flee fComponent;
    Wander wComponent;
    
	void Start ()
    {
        sComponent = this.GetComponent<Seek>();
        fComponent = this.GetComponent<Flee>();
        wComponent = this.GetComponent<Wander>();
	}
	
	void Update ()
    {
        Vector3 myPos = transform.position;
        Vector3 catPos = GameObject.FindGameObjectWithTag("Cat").transform.position;

        if(Vector3.Distance(myPos, catPos) < fComponent.fleeRadius)
        {
            fComponent.Execute();
        }
        else
        {
            GameObject[] cheese = GameObject.FindGameObjectsWithTag("Cheese");
            GameObject closestCheese = null;
            Vector3 cheesePos;
            float distance;

            foreach(GameObject c in cheese)
            {
                cheesePos = c.transform.position;
                distance = Vector3.Distance(myPos, cheesePos);

                if (distance < sComponent.seekRadius &&  (closestCheese == null || distance < Vector3.Distance(myPos, closestCheese.transform.position)))
                {
                    closestCheese = c;
                }
            }

            if(!closestCheese)
            {
                wComponent.Execute();
            }
            else
            {
                sComponent.Execute(closestCheese);
            }
        }

	}
}
