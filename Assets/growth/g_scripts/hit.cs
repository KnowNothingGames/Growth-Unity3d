using UnityEngine;
using System.Collections;

public class hit : MonoBehaviour {
    public int damage = 1;

    public float upframes;
    
    // Use this for initialization
	void Start () {

        Destroy(gameObject, upframes);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (col.tag == "Enemy")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<Enemy>().Hurt(1);

            // Call the explosion instantiation.
            

            // Destroy the rocket.
            Destroy(gameObject);
        }
        // Otherwise if it hits a bomb crate...
        else if (col.tag == "BombPickup")
        {
            // ... find the Bomb script and call the Explode function.
            col.gameObject.GetComponent<Bomb>().Explode();

            // Destroy the bomb crate.
            Destroy(col.transform.root.gameObject);

            // Destroy the rocket.
            Destroy(gameObject);
        }
        // Otherwise if the player manages to shoot himself...
        else if (col.gameObject.tag != "Player")
        {
            // Instantiate the explosion and destroy the rocket.
            
            Destroy(gameObject);
        }
    }



}
