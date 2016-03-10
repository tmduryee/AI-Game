using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour {

    private bool asleep = false;
    private int hunger = 0;
    private Vector3 foodBowlPos;
    private Vector3 bedPos;
    private AI aiComponent;

    public int maxHunger = 100;
    public GameObject foodBowl;
    public GameObject bed;

	// Use this for initialization
	void Start () {
        foodBowlPos = foodBowl.transform.position;
        bedPos = bed.transform.position;
        aiComponent = this.GetComponent<AI>();
	}
	
	// Update is called once per frame
	void Update () {
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
                GameObject cat = GameObject.FindGameObjectWithTag("Cat");
                Vector3 catPos = cat.transform.position;
                float distance = Vector3.Distance(transform.position, catPos);

                // If cat is in range
                if (distance < aiComponent.seekBoundaryRadius)
                    GameObject.Destroy(cat);
                else if (distance < aiComponent.seekRadius)
                {
                    aiComponent.Seek(cat);
                    hunger++;
                }
                else
                {
                    if (Vector3.Distance(transform.position, bedPos) < 10.0f)
                        asleep = true;
                    else
                        aiComponent.Seek(bed);
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
