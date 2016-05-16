using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public ArrayList currentCheese;
	public GameObject mousePrefab;

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

		// Check Mice
		GameObject[] miceObj = GameObject.FindGameObjectsWithTag("Mouse");
		if (miceObj.Length == 0) {
			for (int i = 0; i < 7; i++)
			{
				// x: -7 to -57
				// y: .2328
				// z: -65 to -9
				Vector3 pos = new Vector3(Random.Range(-57,-7), 0.2328f, Random.Range(-65,-9));
				Instantiate(mousePrefab, pos, Quaternion.identity);
			}
		}
    }
}
