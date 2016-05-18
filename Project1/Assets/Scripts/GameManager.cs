using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{

    public List<Cheese> currentCheese;

	// Genetic Algorithm content
	public List<Cheese> originCheeseScripts;
	public List<GameObject> originCheeseObjects;
	public int variations = 10; // the number of variations to test for each generation
	public int generations = 10; // the number of generations to test (iterations)
	public int eatCountTotal = 3; // the number of times the dog reaches its food bowl before ending the test for that variation
	public Vector3[] bestGenome; // holds the best array of vectors the dog follows, which will be mutated off of
	public List<Vector3[]> paths;
	GameObject[] cheeseObjs;

	int highScore = 0; // holds the highest score of current generation

	Dog dog;
	GameObject[] mouseObjs;
	Mouse[] mice;
	GameObject[] catObjs;
	EnemyCat[] cats;

	String fileName;

    // Use this for initialization
    void Start ()
    {
        cheeseObjs = GameObject.FindGameObjectsWithTag("Cheese");

		currentCheese = new List<Cheese>();

        for (int i = 0; i < cheeseObjs.Length; i++)
        {
			currentCheese.Add(cheeseObjs[i].GetComponent<Cheese>());
			originCheeseObjects.Add(cheeseObjs[i]);
        }

		// Genetic Algorithm Stuff
		originCheeseScripts = currentCheese;

		dog = GameObject.FindGameObjectWithTag("Dog").GetComponent<Dog>();

		mouseObjs = GameObject.FindGameObjectsWithTag("Mouse");
		mice = new Mouse[mouseObjs.Length];
		for (int i = 0; i < mouseObjs.Length; i++)
			mice[i] = mouseObjs[i].GetComponent<Mouse>();
		
		catObjs = GameObject.FindGameObjectsWithTag("Cat");
		cats = new EnemyCat[catObjs.Length];	
		for (int i = 0; i < catObjs.Length; i++)
			cats[i] = catObjs[i].GetComponent<EnemyCat>();

		paths = new List<Vector3[]>();

		bestGenome = new Vector3[5];

		bestGenome[0] = new Vector3(-78.9f, 0.003508392f, -129.0f);
		bestGenome[1] = new Vector3(-26.5f, 0.003508392f, -50.8f);
		bestGenome[2] = new Vector3(6.8f, 0.003508392f, -48.8f);
		bestGenome[3] = new Vector3(66.5f, 0.003508392f, -34.7f);
		bestGenome[4] = new Vector3(140.9f, 0.003508392f, -39.3f);

		fileName = "BestGenome.txt";

    }
	
	// Update is called once per frame
	void Update ()
    {
		// moved to TestGeneration() inside while loop
		List<Cheese> cheeseStillAlive = new List<Cheese>();
        foreach(Cheese c in currentCheese)
        {
            if (c.cheeseLeft > 0)
                cheeseStillAlive.Add(c);
        }

        currentCheese = cheeseStillAlive;
		//Simulation();
    }

	// Genetic Algorithm Methods
	void Simulation ()
	{
		for(int i = 0; i < generations; i++)
		{
			CreatePaths();

			TestGeneration(); // this should wait for each variation to be tested

			DestroyPaths();
		}

		// write bestGenome to file
		if (File.Exists(fileName))
		{
			Console.WriteLine(fileName + " already exists.");
			return;
		}

		var sr = File.CreateText(fileName);
		for (int i = 0; i < bestGenome.Length; i++)
		{
			sr.WriteLine(bestGenome[i]);
		}
		sr.Close();
	}

	public void CreatePaths ()
	{
		for (int i = 0; i < variations; i++)
		{
			paths.Add (Mutate());
		}
	}

	public void DestroyPaths ()
	{
		paths = new List<Vector3[]>();
	}

	public Vector3[] Mutate ()
	{
		Vector3[] vectorArray = new Vector3[bestGenome.Length];
		for (int i = 0; i < bestGenome.Length; i++)
		{
			Vector3 vector = bestGenome[i];
			vector.x += UnityEngine.Random.Range(-10, 10);
			vector.z += UnityEngine.Random.Range(-10, 10);
			vectorArray[i] = vector;
		}
		return vectorArray;
	}

	public void TestGeneration ()
	{
		int[] scores = new int[paths.Count];
		for (int i = 0; i < paths.Count; i++)
		{
			// apply path to dog
			Vector3[] path = paths[i];
			dog.resetDog(path);

			foreach(Mouse mouse in mice)
			{
				mouse.resetMouse();
			}

			foreach(EnemyCat cat in cats)
			{
				cat.resetCat();
			}

			// reset cheese
			for (int j = 0; j < currentCheese.Count; j++)
			{
				currentCheese[j].DestroyCheese();
			}
			for (int j = 0; j < originCheeseObjects.Count; j++)
			{
				Instantiate(originCheeseObjects[j], originCheeseObjects[j].transform.position, originCheeseObjects[j].transform.rotation);
			}
			currentCheese = originCheeseScripts;

			// keep track of cheese (Not part of Genetic Algorithm)
			while(dog.getEatCount() < eatCountTotal)
			{
				Console.WriteLine (dog.getEatCount());
				List<Cheese> cheeseStillAlive = new List<Cheese>();
				for (int j = 0; j < currentCheese.Count; j++)
				{
					if (currentCheese[j].cheeseLeft > 0)
						cheeseStillAlive.Add(currentCheese[j]);
				}

				currentCheese = cheeseStillAlive;
			}
			scores[i] = dog.getScore();
		}

		for (int i = 0; i < scores.Length; i++)
		{
			if (scores[i] > highScore)
			{
				highScore = scores[i];
				bestGenome = paths[i];
			}
		}

		highScore = 0;
	}
}
