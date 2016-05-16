using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
    public Texture dogAsleep;
    public Texture dogAngry;
    public Texture dogHungry;
    public Texture cheese;
    public Texture cheeseBar;

    public GUISkin skin;

    private Dog dog;
	public static int score;
	public static int scoreEnemy;

	// Use this for initialization
	void Start ()
    {
        dog = GameObject.FindGameObjectWithTag("Dog").GetComponent<Dog>();
		score = 0;
		scoreEnemy = 0;
	}

    void OnGUI()
    {
        GameManager gManager = this.GetComponent<GameManager>();

        // Cat info
		GUI.TextField(new Rect(15, 20, 100, 30), "Score: " + score, skin.textField);
		GUI.TextField(new Rect(15, 50, 100, 30), "Enemy Score: " + scoreEnemy, skin.textField);

        // Cheese info
        for(int i = 0; i < gManager.currentCheese.Count; i++)
        {
            GUI.DrawTexture(new Rect(15, 100 + (40 * i), 35, 35), cheese);
            GUI.DrawTexture(new Rect(50, 100 + (40 * i), ((Cheese)gManager.currentCheese[i]).cheeseLeft, 35), cheeseBar);
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
        GUI.TextField(new Rect(85, Screen.height - 40, 100, 30), "Hunger: " + (int)dog.hunger, skin.textField);
	}
}
