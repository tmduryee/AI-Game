using UnityEngine;
using System;
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

	// Genetic Algorithm stuff
	private int seekCount = 0; // Records number of times a cat is sought after
	private int catCount = 0; // Records number of times a cat is eaten/attacked (this dog is fucking vicious if it actually eats the cat!)
	private int eatCount = 0; // Records number of times dog eats out of food bowl
	private Vector3 originPos; // Stores the dog's original position which is used for resetting the dog

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

		originPos = this.transform.position;
    }


    void Update ()
    {
        // If dog is asleep
        if (asleep)
        {
			Console.WriteLine ("Dog is sleeping");
			// Wake him up if he's hungry
            if (hunger >= maxHunger)
                asleep = false;
            else
			{
				hunger += Time.deltaTime * 15;
				Console.WriteLine("Dog is getting hungry");
			}
        }
        else
        {
			Console.WriteLine ("Dog is awake");
            // Check to see if he currently needs food
            if(hunger >= maxHunger)
            {
                // If next to bowl, reset hunger value
                if (Vector3.Distance(transform.position, foodBowlPos) < 5.0f)
                {
					Console.WriteLine ("Dog ate");
                    hunger = 0;

					// Genetic Algorithm content
					eatCount++;
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
						Console.WriteLine ("Dog is angry and chasing cat");
						aiComponent.Seek(closestCat);
						hunger += Time.deltaTime * 15;

						// Genetic Algorithm content
						// adds 1 to seekCount ONLY ONCE PER CAT SEEK
						if (!seekingCat)
							seekCount++;
						
						seekingCat = true;
						seekingBed = false;

						if (Vector3.Distance(transform.position, closestCat.transform.position) < 20.0f)
                        {
							if (closestCat.GetComponent<EnemyCat>() == null)
                            {
                                Application.LoadLevel("GameOver");
                                Debug.Log("Game Over!!");
							}
                            else
                            {
                                GameObject.Destroy(closestCat);
                                Debug.Log("Cat Destroyed.");
                            }
						}
					}
				}                
                else
                {
                    if (Vector3.Distance(transform.position, bedPos) < 10.0f)
                    {
						Console.WriteLine ("Dog is asleep");
                        asleep = true;
                        seekingBed = false;
                    }
                    else
                    {
						Console.WriteLine ("Dog is seeking bed");
                        pFollow.Follow(false, aiComponent.seekSpeed, seekingCat);
                        seekingBed = true;
                        seekingCat = false;
                    }
                }
            }
        }
	}

	// Genetic Algorithm Methods

	// sets a new path for the dog to follow
	public void setPath(Vector3[] path)
	{
		pFollow = new PathFollow(this.transform);

		for (int i = 0; i < path.Length; i++)
		{
			Node n = new Node(path[i]);

			pFollow.AddWaypoint(n);
		}
	}

	public int getScore()
	{
		return seekCount + (catCount * 3);
	}

	public int getEatCount()
	{
		return eatCount;
	}

	// this gets called when the current path variation is done being tested
	public void resetDog(Vector3[] path)
	{
		this.transform.position = originPos; // reset position

		// reset counts
		seekCount = 0;
		catCount = 0;
		eatCount = 0;
		hunger = 0.0f;

		// set new path
		setPath(path);
	}

}
