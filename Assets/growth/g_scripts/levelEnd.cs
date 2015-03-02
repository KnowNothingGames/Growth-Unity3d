using UnityEngine;
using System.Collections;

public class levelEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D col)
    {

        //this needs t obe changed a get a value for hurt instead of being 1
        if (col.tag == "Player") 
        {
            Application.LoadLevel("growth_1");
        }
    }


}
