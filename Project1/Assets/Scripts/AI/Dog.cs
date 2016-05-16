using UnityEngine;
using System.Collections;

public class Dog : Entity
{
    public float hunger = 0.0f;
    public int maxHunger = 200;
    public bool asleep = false;
    public bool seekingCat = false;
    public bool seekingBed = false;
    public GameObject foodBowl;
    public GameObject bed;

    private Vector3 foodBowlPos;
    private Vector3 bedPos;
    private PathFollow pFollow;

    void Start ()
    {
        aiComponent = this.GetComponent<AI>();
        foodBowlPos = foodBowl.transform.position;
        bedPos = bed.transform.position;

        pFollow = new PathFollow(this.transform);
        Node first = new Node(new Vector3(-78.9f, 0.003508392f, -129.0f));
        Node second = new Node(new Vector3(-26.5f, 0.003508392f, -50.8f));
        Node third = new Node(new Vector3(6.8f, 0.003508392f, -48.8f));
        Node fourth = new Node(new Vector3(66.5f, 0.003508392f, -34.7f));
        Node fifth = new Node(new Vector3(140.9f, 0.003508392f, -39.3f));

        pFollow.AddWaypoint(first);
        pFollow.AddWaypoint(second);
        pFollow.AddWaypoint(third);
        pFollow.AddWaypoint(fourth);
        pFollow.AddWaypoint(fifth);
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
				hunger += Time.deltaTime * 15;
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
                }
                else
                {
                    pFollow.Follow(true, aiComponent.seekSpeed, seekingCat);
                    seekingCat = false;
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
						hunger += Time.deltaTime * 15;
						seekingCat = true;
						seekingBed = false;

						if (Vector3.Distance(transform.position, closestCat.transform.position) < 20.0f) {
							if (closestCat.name == "Cat") {
								Application.LoadLevel("gameOver");
							}
						}
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
                        pFollow.Follow(false, aiComponent.seekSpeed, seekingCat);
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
