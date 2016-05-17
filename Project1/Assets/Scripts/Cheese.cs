using UnityEngine;
using System.Collections;

public class Cheese : MonoBehaviour
{
    public int cheeseLeft = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(cheeseLeft < 0)
        {
			DestroyCheese();
        }
    }

    public void Eat()
    {
        cheeseLeft--;
    }

	public void DestroyCheese()
	{
		Destroy(this.gameObject);
	}
}
