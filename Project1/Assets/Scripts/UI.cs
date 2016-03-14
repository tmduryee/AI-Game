using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    public Texture kittyPortrait;
    public Texture dogAsleep;
    public Texture dogAngry;
    public Texture dogHungry;
    public Texture cheese;
    public Texture cheeseBar;

    public GUISkin skin;

    private Dog dog;
    private GameManager gManager;

	// Use this for initialization
	void Start () {
        dog = GameObject.FindGameObjectWithTag("Dog").GetComponent<Dog>();
        gManager = this.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnGUI()
    {
        // Cat info
        GUI.DrawTexture(new Rect(10, 10, 150, 150), kittyPortrait);

        // Cheese info
        for(int i = 0; i < gManager.currentCheese.Count; i++)
        {
            GUI.DrawTexture(new Rect(15, 200 + (40 * i), 35, 35), cheese);
            GUI.DrawTexture(new Rect(50, 200 + (40 * i), ((Cheese)gManager.currentCheese[i]).cheeseLeft, 35), cheeseBar);
        }

        // Dog info
        GUI.Box(new Rect(0, Screen.height - 90, 200, 90), "", skin.box);
        string dogStatus = "Status: ";
        if (dog.hunger >= dog.maxHunger)
        {
            GUI.TextField(new Rect(85, Screen.height - 75, 100, 30), "Status: Hungry", skin.textField);
            GUI.DrawTexture(new Rect(10, Screen.height - 80, 70, 70), dogHungry);
        }
        else if (dog.seekingCat)
        {
            GUI.TextField(new Rect(85, Screen.height - 75, 100, 30), "Status: Angry", skin.textField);
            GUI.DrawTexture(new Rect(10, Screen.height - 80, 70, 70), dogAngry);
        }
        else if (dog.seekingBed)
        {
            GUI.TextField(new Rect(85, Screen.height - 75, 100, 30), "Status: Sleepy", skin.textField);
            GUI.DrawTexture(new Rect(10, Screen.height - 80, 70, 70), dogAsleep);
        }
        else
        {
            GUI.TextField(new Rect(85, Screen.height - 75, 100, 30), "Status: Asleep", skin.textField);
            GUI.DrawTexture(new Rect(10, Screen.height - 80, 70, 70), dogAsleep);
        }

        GUI.TextField(new Rect(85, Screen.height - 75, 100, 30), dogStatus, skin.textField);
        GUI.TextField(new Rect(85, Screen.height - 40, 100, 30), "Hunger: " + dog.hunger, skin.textField);
    }
}
