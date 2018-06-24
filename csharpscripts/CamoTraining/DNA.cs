using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    // gene for color
    public float r;
    public float g;
    public float b;

    // dimensions for the scale of the object
    public float dim;
    // keeps track of how long they were on the screen
    public float timeToDie = 0;

    bool dead = false;
    SpriteRenderer sRenderer;
    Collider2D sCollider;


    void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        Debug.Log("Dead at: " + timeToDie);
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }
	// Use this for initialization
	void Start () {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();

        // changing the scale of the person!
        this.transform.localScale = new Vector3(dim, dim, dim);

        sRenderer.color = new Color(r, g, b);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
