using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PopulationManager : MonoBehaviour {

    public int populationSize = 10;
    public static float elapsed = 0;
    public GameObject personPrefab;

    private int trialTime = 10;
    private int generation = 1;
    private List<GameObject> population = new List<GameObject>();

    /**
     * This is used to display pertinent game information to the screen
     */
    GUIStyle guiStyle = new GUIStyle();
    
    void onGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
    }


	// Use this for initialization
	void Start () {
		// creating the initial population...

        for(int i = 0; i < populationSize; i++)
        {   
            // create the person...
            Vector3 pos = new Vector3(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f), 0);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);

            // generate them a random color!
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);

            // generating the random size!
            go.GetComponent<DNA>().dim = Random.Range(0.0f, 1.0f);
           
            // add them to the population list!
            population.Add(go);

        }
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        // create the person...
        Vector3 pos = new Vector3(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);

        // gathering traits of each parent!
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        // swap parent DNA
        // the offswing have a 50 percent chance of inheriting either parent's color
        // this is true for each individual color
        offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;

        // modifying the scale of each object
        offspring.GetComponent<DNA>().dim = Random.Range(0, 10) < 5 ? dna1.dim : dna2.dim;

        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        // get rid of unfit individuals
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

        population.Clear();
        // breed only the top half of the list
        for (int i = (int) (sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // removing parents from the previous generation
        for(int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }

	// Update is called once per frame
	void Update () {
        // updating the time elapsed each frame
        elapsed += Time.deltaTime;

        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
	}
}
