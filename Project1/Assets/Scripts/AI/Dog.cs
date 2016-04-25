using UnityEngine;
using System.Collections;

public class Dog : Entity
{

    private Vector3 foodBowlPos;
    private Vector3 bedPos;

    public int hunger = 0;
    public int maxHunger = 100;
    public bool asleep = false;
    public bool seekingCat = false;
    public bool seekingBed = false;
    public GameObject foodBowl;
    public GameObject bed;
    
	void Start ()
    {
        aiComponent = this.GetComponent<AI>();
        foodBowlPos = foodBowl.transform.position;
        bedPos = bed.transform.position;
	}
	

	void Update ()
    {
        // If dog is asleep
        if (asleep)
        {
            // Wake him up if he's hungry
            if (hunger >= maxHunger)
                asleep = false;
            else
                hunger++;
        }
        else
        {
            // Check to see if he currently needs food
            if(hunger >= maxHunger)
            {
                // If next to bowl, reset hunger value
                if (Vector3.Distance(transform.position, foodBowlPos) < 5.0f)
                {
                    hunger = 0;
                    Debug.Log("Hit");
                }
                else
                {
                    aiComponent.Seek(foodBowl);
                }
            }
            // Otherwise lets look for the cat
            else
            {
                GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
				GameObject closestCat = null;
				Vector3 catPos;
				float distance;

				foreach (GameObject c in cats)
				{
					catPos = c.transform.position;
					distance = Vector3.Distance(transform.position, catPos);
					
					if (distance < aiComponent.seekRadius && (closestCat == null || distance < Vector3.Distance(transform.position, closestCat.transform.position)))
					{
						closestCat = c;
					}
				}

                // If cat is in range
				if (closestCat) 
				{
					if (Vector3.Distance(transform.position, closestCat.transform.position) < aiComponent.seekBoundaryRadius)
						GameObject.Destroy(closestCat);
					else if (Vector3.Distance(transform.position, closestCat.transform.position) < aiComponent.seekRadius)
					{
						aiComponent.Seek(closestCat);
						hunger++;
						seekingCat = true;
						seekingBed = false;
					}
				}                
                else
                {
                    if (Vector3.Distance(transform.position, bedPos) < 10.0f)
                    {
                        asleep = true;
                        seekingBed = false;
                    }
                    else
                    {
                        aiComponent.Seek(bed);
                        seekingBed = true;
                        seekingCat = false;
                    }
                }
            }
        }
	}

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.tag == "Cat")
        {
            //Destroy(c.gameObject);
        }
    }
}
