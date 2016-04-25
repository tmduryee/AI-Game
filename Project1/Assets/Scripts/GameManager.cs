using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public ArrayList currentCheese;

    // Use this for initialization
    void Start ()
    {
        currentCheese = new ArrayList();

        GameObject[] cheeseObjs = GameObject.FindGameObjectsWithTag("Cheese");
        for (int i = 0; i < cheeseObjs.Length; i++)
        {
            currentCheese.Add(cheeseObjs[i].GetComponent<Cheese>());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        ArrayList cheeseStillAlive = new ArrayList();
        foreach(Cheese c in currentCheese)
        {
            if (c.cheeseLeft > 0)
                cheeseStillAlive.Add(c);
        }

        currentCheese = cheeseStillAlive;
    }
}
